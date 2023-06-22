using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BrickManager : MonoBehaviour
{
    public Vector2 _GridSize;
    public float _CellSize;
    [Range(0.0f, 0.5f)]
    public float _Padding;

    public bool _ShowGizmo = false;
    public bool _ShowGrid = false;

    // Brick Grid
    public class Grid
    {
        public class Cell
        {
            // from top-left
            public Vector2Int Coordinate = Vector2Int.zero;

            public Cell(Vector2Int coord)
            {
                Coordinate = coord;
            }
        }

        private Cell[] mCells = null;
        private int mEmptyEnd = 0;

        private Vector2 mInnerGridSize;
        private float mCellSize = 0;
        public float CellSize
        {
            get { return mCellSize; }
        }

        private float mCellSizeWithPadding = 0;
        public float CellSizeWithPadding
        {
            get { return mCellSizeWithPadding; }
        }

        public Grid(uint width, uint height, float cellSize, float cellSizeWithPadding)
        {
            mCells = new Cell[width * height];
            for(int y = 0; y < height; ++y)
                for(int x = 0; x < width; ++x)
                {
                    mCells[y * height + x] = new Cell(new Vector2Int(x, y));
                }
            mEmptyEnd = mCells.Length;

            mCellSize = cellSize;
            mCellSizeWithPadding = cellSizeWithPadding;

            mInnerGridSize = Vector2.zero;
            mInnerGridSize.x = width * mCellSize;
            mInnerGridSize.y = height * mCellSize;
        }

        public bool IsFull()
        {
            return mEmptyEnd == 0;
        }

        public Cell TakeOverRandom()
        {
            Debug.Assert(mEmptyEnd != 0, "Grid if full!");

            var index = Random.Range(0, mEmptyEnd);
            Cell takeOverCell = mCells[index];

            var swap = mCells[index];
            mCells[index] = mCells[mEmptyEnd - 1];
            mCells[mEmptyEnd - 1] = swap;

            --mEmptyEnd;

            return takeOverCell;
        }

        public void ReturnCell(Cell cell)
        {
            Debug.Assert(mEmptyEnd < mCells.Length);

            mCells[mEmptyEnd] = cell;
            ++mEmptyEnd;
        }

        public Vector3 GetPositionByCell(Grid.Cell cell, Vector3 gridCenter)
        {
            Vector3 position = Vector3.zero;

            Vector3 gridElementStartPosition = Vector3.zero;
            gridElementStartPosition.x = gridCenter.x - mInnerGridSize.x / 2.0f + mCellSize / 2.0f;
            gridElementStartPosition.y = gridCenter.y + mInnerGridSize.y / 2.0f - mCellSize / 2.0f;

            position.x = gridElementStartPosition.x + cell.Coordinate.x * mCellSize;
            position.y = gridElementStartPosition.y - cell.Coordinate.y * mCellSize;

            return position;
        }
    }

    private Grid mBrickGrid = null;

    float mSpawnCD = 0;
    float mSpawnTime = 0;
    float mSpawnCount = 0;

    // brick spawner
    class BrickSpawner
    {
        class Probability
        {
            public int StartWeight = 0;
            public int EndWeight = 0;
            public cfg.Brick Brick = null;
        }

        private List<Probability> mProbability;
        private int mTotalProbability = 0;

        public BrickSpawner(List<cfg.Brick> probability)
        {
            Debug.Assert(probability != null && probability.Count != 0, "Probability Error");

            mProbability = new List<Probability>(probability.Count);

            int start = 0;
            int end = 0;
            for(int i = 0; i < probability.Count; ++i)
            {
                end += probability[i].Weight;

                Probability p = new Probability();
                p.StartWeight = start;
                p.EndWeight = end;
                p.Brick = probability[i];
                mProbability.Add(p);

                start += probability[i].Weight;

                mTotalProbability += probability[i].Weight;
            }
        }

        public cfg.Brick Spawn()
        {
            int p = Random.Range(0, mTotalProbability);

            cfg.Brick brick = null;
            for (int i = 0; i < mProbability.Count; ++i)
                if (p >= mProbability[i].StartWeight && p < mProbability[i].EndWeight)
                    brick = mProbability[i].Brick;

            return brick;
        }
    }
    private BrickSpawner mBrickSpawner = null;

    // brick resource
    private cfg.TbBrickConfig mBrickRewardResource = null;
    GameObject mBrickRewardResourceTemplate = null;

    private static BrickManager mInstance = null;
    public static BrickManager Instance
    {
        get { return mInstance; }
    }

    private void Awake()
    {
        mInstance = this;
    }

    void Start()
    {
        mBrickRewardResourceTemplate = ResourcesModule.Instance.Load<GameObject>("Assets/Bundles/Res/Prefabs/Marble/BrickTemplate.prefab");
        mBrickRewardResource = DataManager.DataTable.TbBrickConfig;

        GenerateBrickGrid();

        OnLevelAdvanced();

        for (int i = 0; i < mSpawnCount; ++i)
        {
            SpawnBrick();
        }

        mSpawnTime = int.MaxValue;

        GlobalEventManager.OnLevelAdvanced.AddListener(OnLevelAdvanced);
        MarbleEventManager.OnBrickDestory.AddListener(OnBrickDestory);
    }

    private void OnDestroy()
    {
        mInstance = null;
    }

    public void Tick()
    {
        // brick refresh
        mSpawnCD += Time.deltaTime;
        if(mSpawnCD > mSpawnTime)
        {
            SpawnBrick();

            mSpawnCD = 0;
        }
    }

    void OnBrickDestory(BrickController bc)
    {
        mBrickGrid.ReturnCell(bc.BrickCell);
    }

    void OnLevelAdvanced()
    {
        var curLevel = LevelManager.Instance.CurrentLevel;
        cfg.LevelConfig levelInfo = DataManager.DataTable.TbLevelConfig.Get(curLevel.LevelID, curLevel.SubLevelID);

        mSpawnTime = levelInfo.BrickFreshSpace;
        mSpawnCount = levelInfo.BrickFreshNum;
        mSpawnCD = 0;

        var curLevelBrickPoolItem = DataManager.DataTable.TbBrickPool.Get(levelInfo.Brick);

        mBrickSpawner = new BrickSpawner(curLevelBrickPoolItem.BrickConfig);
    }

    void SpawnBrick()
    {
        if (mBrickGrid.IsFull())
            return;

        var brick = mBrickSpawner.Spawn();

        GameObject brickObject = GameObject.Instantiate<GameObject>(mBrickRewardResourceTemplate);
        brickObject.transform.SetParent(transform.parent);

        // assemble brick object
        var resourceConfig = mBrickRewardResource.Get(brick.BrickID);
        
        // icon
        if(resourceConfig.Icon == "none")
            brickObject.transform.GetChild(1).gameObject.SetActive(false);
        else
            brickObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ResourcesModule.Instance.Load<Sprite>("Assets/Bundles/Res/Texture/Marble/BrickIcon/" + resourceConfig.Icon+".png");

        // number and type
        var brickController = brickObject.GetComponent<BrickController>();
        brickController.Life = Random.Range(brick.HPMin, brick.HPMax + 1);
        brickController.BrickType = resourceConfig.BrickRewardType;
        brickController.BrickID = resourceConfig.BrickRewardId;

        // position and scale
        Grid.Cell cell = mBrickGrid.TakeOverRandom();
        brickObject.transform.position = mBrickGrid.GetPositionByCell(cell, transform.position);
        var sr = brickObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        
        Vector3 curSize = sr.sprite.bounds.size;
        brickObject.transform.localScale = new Vector3(mBrickGrid.CellSizeWithPadding / curSize.x, mBrickGrid.CellSizeWithPadding / curSize.y, 1);

        brickController.BrickCell = cell;
    }

    void GenerateBrickGrid()
    {
        var cellCount = GetCellCount();
        mBrickGrid = new Grid((uint)cellCount.x, (uint)cellCount.y, _CellSize, GetCellSizeWithPadding());
    }

    Vector2Int GetCellCount()
    {
        Vector2 CellSize = new Vector2(_CellSize, _CellSize);
        Vector2 cellCountFloat = _GridSize / CellSize;
        Vector2Int cellCount = new Vector2Int((int)cellCountFloat.x, (int)cellCountFloat.y);
        return cellCount;
    }

    float GetCellSizeWithPadding()
    {
        return _CellSize * (1 - 2 * _Padding);
    }

    private void OnDrawGizmos()
    {
        if (!_ShowGizmo)
            return;

        Vector2 CellSize = new Vector2(_CellSize, _CellSize);
        Vector2Int cellCount = GetCellCount();

        Vector2 innerGridSize = (cellCount * CellSize);

        // outter
        Gizmos.color = Color.yellow;
        Vector3 size = _GridSize;
        size.z = 1;
        Gizmos.DrawWireCube(transform.position, size);

        // inner
        Gizmos.color = Color.red;
        size = cellCount * CellSize;
        size.z = 1;
        Gizmos.DrawWireCube(transform.position, size);

        // grid
        if (_ShowGrid)
        {
            Vector3 lineFrom = Vector3.zero;
            Vector3 lineTo = Vector3.zero;

            lineFrom.x = transform.position.x - innerGridSize.x / 2.0f;
            lineFrom.y = transform.position.y + innerGridSize.y / 2.0f;
            lineTo.x = transform.position.x - innerGridSize.x / 2.0f;
            lineTo.y = transform.position.y - innerGridSize.y / 2.0f;

            Gizmos.color = Color.gray;
            Vector3 step = new Vector3(CellSize.x, 0.0f, 0.0f);
            for (int x = 1; x < cellCount.x; ++x)
            {
                Gizmos.DrawLine(lineFrom + x * step, lineTo + x * step);
            }

            lineFrom.x = transform.position.x - innerGridSize.x / 2.0f;
            lineFrom.y = transform.position.y + innerGridSize.y / 2.0f;
            lineTo.x = transform.position.x + innerGridSize.x / 2.0f;
            lineTo.y = transform.position.y + innerGridSize.y / 2.0f;
            step = new Vector3(0.0f, -CellSize.y, 0.0f);
            for (int y = 1; y < cellCount.y; ++y)
            {
                Gizmos.DrawLine(lineFrom + y * step, lineTo + y * step);
            }
        }

        // grid element
        Gizmos.color = Color.white;
        float gridElementSize = GetCellSizeWithPadding();
        Vector3 gridElementStartPosition = Vector3.zero;
        gridElementStartPosition.x = transform.position.x - innerGridSize.x / 2.0f + _CellSize / 2.0f;
        gridElementStartPosition.y = transform.position.y + innerGridSize.y / 2.0f - _CellSize / 2.0f;
        Vector3 gridElementPosition = Vector3.zero;
        Vector3 gridElementSizeV3 = Vector3.one;
        gridElementSizeV3.x = gridElementSize;
        gridElementSizeV3.y = gridElementSize;
        for (int x = 0; x < cellCount.x; ++x)
        {
            for (int y = 0; y < cellCount.y; ++y)
            {
                gridElementPosition.x = gridElementStartPosition.x + x * _CellSize;
                gridElementPosition.y = gridElementStartPosition.y - y * _CellSize;
                Gizmos.DrawWireCube(gridElementPosition, gridElementSizeV3);
            }
        }
    }
}
