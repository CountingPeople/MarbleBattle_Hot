using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;

public class BrickManager : MonoBehaviour
{
    /* Grid config */
    public Vector3 _GridSize;
    public float _CellSize;
    [Range(0.0f, 0.5f)]
    public float _Padding;

    /* Gizmo */
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

        private float mCellHeightWithPadding = 0;
        public float CellHeightWithPadding
        {
            get { return mCellHeightWithPadding; }
        }

        public Grid(uint width, uint height, float cellSize, float cellSizeWithPadding, float cellHeight)
        {
            mCells = new Cell[width * height];
            for(int y = 0; y < height; ++y)
                for(int x = 0; x < width; ++x)
                {
                    mCells[y * width + x] = new Cell(new Vector2Int(x, y));
                }
            mEmptyEnd = mCells.Length;

            mCellSize = cellSize;
            mCellSizeWithPadding = cellSizeWithPadding;
            mCellHeightWithPadding = cellHeight;

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
            gridElementStartPosition.y = gridCenter.y;
            gridElementStartPosition.x = gridCenter.x - mInnerGridSize.x / 2.0f + mCellSize / 2.0f;
            gridElementStartPosition.z = gridCenter.z + mInnerGridSize.y / 2.0f - mCellSize / 2.0f;

            position.x = gridElementStartPosition.x + cell.Coordinate.x * mCellSize;
            position.y = gridElementStartPosition.y;
            position.z = gridElementStartPosition.z - cell.Coordinate.y * mCellSize;

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
    private const string _BrickTemplatePath = "Assets/Bundles/Res/Prefabs/Marble/Brick3D.prefab";
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
        mBrickRewardResourceTemplate = AssetDatabase.LoadAssetAtPath<GameObject>(_BrickTemplatePath);
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
            brickObject.transform.GetChild(0).gameObject.SetActive(false);
        else
            brickObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Bundles/Res/Texture/Marble/BrickIcon/" + resourceConfig.Icon+".png");

        // number and type
        var brickController = brickObject.GetComponent<BrickController>();
        brickController.Life = Random.Range(brick.HPMin, brick.HPMax + 1);
        brickController.BrickType = resourceConfig.BrickRewardType;
        brickController.BrickID = resourceConfig.BrickRewardId;

        // position
        Grid.Cell cell = mBrickGrid.TakeOverRandom();
        brickObject.transform.position = mBrickGrid.GetPositionByCell(cell, transform.position);
        
        // scale
        var renderer = brickObject.transform.GetComponent<Renderer>();
        
        Vector3 curSize = renderer.bounds.size;
        brickObject.transform.localScale = new Vector3(mBrickGrid.CellSizeWithPadding / curSize.x, mBrickGrid.CellHeightWithPadding / curSize.y, mBrickGrid.CellSizeWithPadding / curSize.z);

        brickController.BrickCell = cell;
    }

    void GenerateBrickGrid()
    {
        var cellCount = GetCellCount();
        mBrickGrid = new Grid((uint)cellCount.x, (uint)cellCount.y, _CellSize, GetCellSizeWithPadding(), _GridSize.y);
    }

    Vector2Int GetCellCount()
    {
        Vector2 cellCountFloat = new Vector2(_GridSize.x / _CellSize, _GridSize.z / _CellSize);
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

        // outter bouding box
        Gizmos.color = Color.yellow;
        Vector3 size = _GridSize;
        Gizmos.DrawWireCube(transform.position, size);

        // inner bounding box
        Gizmos.color = Color.red;
        var innerSize = cellCount * CellSize;
        size.x = innerSize.x;
        size.z = innerSize.y;
        Gizmos.DrawWireCube(transform.position, size);

        // grid
        if (_ShowGrid)
        {
            Vector3 lineFrom = Vector3.zero;
            Vector3 lineTo = Vector3.zero;

            lineFrom.x = transform.position.x - innerGridSize.x / 2.0f;
            lineFrom.y = transform.position.y;
            lineFrom.z = transform.position.z + innerGridSize.y / 2.0f;
            lineTo.x = transform.position.x - innerGridSize.x / 2.0f;
            lineTo.y = transform.position.y;
            lineTo.z = transform.position.z - innerGridSize.y / 2.0f;

            Gizmos.color = Color.gray;
            Vector3 step = new Vector3(CellSize.x, 0.0f, 0.0f);
            for (int x = 1; x < cellCount.x; ++x)
            {
                Gizmos.DrawLine(lineFrom + x * step, lineTo + x * step);
            }

            lineFrom.x = transform.position.x - innerGridSize.x / 2.0f;
            lineFrom.z = transform.position.z + innerGridSize.y / 2.0f;
            lineTo.x = transform.position.x + innerGridSize.x / 2.0f;
            lineTo.z = transform.position.z + innerGridSize.y / 2.0f;
            step = new Vector3(0.0f, 0.0f, -CellSize.y);
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
        gridElementStartPosition.y = transform.position.y;
        gridElementStartPosition.z = transform.position.z + innerGridSize.y / 2.0f - _CellSize / 2.0f;

        Vector3 gridElementPosition = Vector3.zero;
        gridElementPosition.y = transform.position.y;
        Vector3 gridElementSizeV3 = Vector3.one;
        gridElementSizeV3.x = gridElementSize;
        gridElementSizeV3.y = _GridSize.y;
        gridElementSizeV3.z = gridElementSize;
        for (int x = 0; x < cellCount.x; ++x)
        {
            for (int z = 0; z < cellCount.y; ++z)
            {
                gridElementPosition.x = gridElementStartPosition.x + x * _CellSize;
                gridElementPosition.z = gridElementStartPosition.z - z * _CellSize;
                Gizmos.DrawWireCube(gridElementPosition, gridElementSizeV3);
            }
        }
    }
}
