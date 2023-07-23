using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

class MonsterManager
{
    static public MonsterManager Instance = null;

    private List<GameObject> mEnemyList = new List<GameObject>();
    public float _EnemySpawnSize = 0.5f;    // TODO: read from config @zhangrufu

    public List<GameObject> Enemies
    {
        get { return mEnemyList; }
    }

    public int EnemyCount
    {
        get { return Enemies.Count; }
    }
    
    Transform mEnemyParent;

    // monster spawner
    class MonsterSpawner
    {
        class Probability
        {
            public int StartWeight = 0;
            public int EndWeight = 0;
            public string MonsterID = "";
        }

        private List<Probability> mProbability;
        private int mTotalProbability = 0;

        public MonsterSpawner(List<cfg.MonsterRefreshConfig> probability)
        {
            Debug.Assert(probability != null && probability.Count != 0, "Probability Error");

            mProbability = new List<Probability>(probability.Count);

            int start = 0;
            int end = 0;
            for (int i = 0; i < probability.Count; ++i)
            {
                end += probability[i].Weight;

                Probability p = new Probability();
                p.StartWeight = start;
                p.EndWeight = end;
                p.MonsterID = probability[i].ID;
                mProbability.Add(p);

                start += probability[i].Weight;

                mTotalProbability += probability[i].Weight;
            }
        }

        public string Spawn()
        {
            int p = Random.Range(0, mTotalProbability);

            string brick = null;
            for (int i = 0; i < mProbability.Count; ++i)
                if (p >= mProbability[i].StartWeight && p < mProbability[i].EndWeight)
                    brick = mProbability[i].MonsterID;

            return brick;
        }
    }
    private MonsterSpawner mMonsterSpawner = null;

    // monster resource
    private cfg.TbMonsterConfig mMonsterConfig = null;
    private Dictionary<string, GameObject> mMonsterTemplate = new Dictionary<string, GameObject>();

    // CD
    float mSpawnCD = 0;
    float mSpawnTime = 0;
    float mSpawnCount = 0;

    MonsterManager(Transform enemyParent)
    {
        // load enemy resource
        mMonsterConfig = DataManager.DataTable.TbMonsterConfig;
        var dataList = mMonsterConfig.DataList;
        string resPathBase = "Assets/Bundles/Res/Prefabs/Battle/";
        foreach(var data in dataList)
        {
            string resPath = resPathBase + data.Res+".prefab";
            GameObject template = AssetDatabase.LoadAssetAtPath<GameObject>(resPath);
            Debug.Assert(template != null, "Monster prefab is null");

            mMonsterTemplate.Add(data.ID, template);
        }

        mEnemyParent = enemyParent;

        // level monter logic
        OnLevelAdvanced();
        SpawnEnemy();

        mSpawnTime = int.MaxValue;

        GlobalEventManager.OnLevelAdvanced.AddListener(OnLevelAdvanced);
    }

    void OnLevelAdvanced()
    {
        var curLevel = LevelManager.Instance.CurrentLevel;
        cfg.LevelConfig levelInfo = DataManager.DataTable.TbLevelConfig.Get(curLevel.LevelID, curLevel.SubLevelID);

        mSpawnTime = levelInfo.MonsterFreshSpace;
        mSpawnCount = levelInfo.MonsterFreshNum;
        mSpawnCD = 0;

        var curLevelMonsterPoolItem = DataManager.DataTable.TbMonsterPool.Get(levelInfo.Brick);

        mMonsterSpawner = new MonsterSpawner(curLevelMonsterPoolItem.Monster);
    }

    public static void Init(Transform enemyParent)
    {
#if UNITY_EDITOR
        Debug.Assert(Instance == null);
        Debug.Assert(CameraManager.Instance != null);
#endif
        Instance = new MonsterManager(enemyParent);
    }

    public static void Destory()
    {
        Instance = null;
    }

    public void Tick(float deltaTime)
    {
        mSpawnCD += deltaTime;
        if (mSpawnCD >= mSpawnTime)
        {
            SpawnEnemy();
            mSpawnCD = 0;
        }
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < mSpawnCount; ++i)
        {
            var monsterID = mMonsterSpawner.Spawn();
            GameObject template = mMonsterTemplate[monsterID];
            GameObject monsterObject = GameObject.Instantiate<GameObject>(template);

            // TODO: config area
            // position
            Vector2 targetPosition = Vector3.zero;
            var rendererComp = monsterObject.GetComponent<SpriteRenderer>();
            var enemySize = rendererComp.bounds.size;
            var enemyExtent = rendererComp.bounds.extents;

            // TODO: cache viewport size @zhangrufu
            var battleCamera = CameraManager.Instance.BattleCamera;
            Vector2 viewport = new Vector2(0, battleCamera.orthographicSize * 2);
            viewport.x = viewport.y * battleCamera.aspect;
            viewport.y *= _EnemySpawnSize;

            Vector2 anchor = new Vector2(-viewport.x / 2.0f, battleCamera.orthographicSize - viewport.y);

            Vector2 div = viewport / enemySize;
            Vector2Int grid = new Vector2Int(Mathf.FloorToInt(div.x), Mathf.FloorToInt(div.y));
            Vector2 gridPos = new Vector2((int)(Random.value * grid.x), (int)(Random.value * grid.y));
            targetPosition = ((Vector2)enemyExtent + gridPos * enemySize) + anchor;
            targetPosition.y -= 0.3f;

            // create
            monsterObject.transform.SetParent(mEnemyParent, false);
            monsterObject.transform.localPosition = targetPosition;

            var monsterConfig = mMonsterConfig.Get(monsterID);
            var script = monsterObject.GetComponent<EnemySpikeShooter>();
            script.AttackCD = monsterConfig.AttackCD;
            script.MoveSpeed = monsterConfig.Speed;
            script.HP = monsterConfig.HP;

            mEnemyList.Add(monsterObject);
        }
    }

    public GameObject GetNearestEnemy(Vector3 position)
    {
        if (mEnemyList.Count == 0)
            return null;

        int minIndex = 0;
        float minDistance = (position - mEnemyList[0].transform.position).sqrMagnitude;
        for (int i = 1; i < mEnemyList.Count; ++i)
        {
            float curDistance = (position - mEnemyList[0].transform.position).sqrMagnitude;
            if (curDistance < minDistance)
            {
                minIndex = i;
                minDistance = curDistance;
            }
        }

        return mEnemyList[minIndex];
    }

    public void RemoveEnemy(GameObject enemy)
    {
        for (int i = 0; i < mEnemyList.Count; ++i)
            if (mEnemyList[i] == enemy)
            {
                mEnemyList.RemoveAt(i);
                return;
            }

    }
}
