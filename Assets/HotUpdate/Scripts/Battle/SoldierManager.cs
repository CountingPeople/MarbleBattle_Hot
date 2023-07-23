using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoldierManager : MonoBehaviour
{
    public Vector2 _GridSize;
    public float _CellSize;
    [Range(0.0f, 0.5f)]
    public float _Padding;

    public Vector2 _CellLevelOffset;

    public bool _ShowGizmo = false;
    [Range(1, 5)]
    public int _GizmoLevels = 1;

    private Dictionary<string, GameObject> mSoldierTemplat = new Dictionary<string, GameObject>();

    // Soldier Grid
    class Grid
    {
        public class Cell
        {
            // from top-left
            public Vector2Int Coordinate = Vector2Int.zero;
            public bool IsEmpty = true;

            public Cell(Vector2Int coord)
            {
                Coordinate = coord;
            }
        }

        private List<Cell[]> mCells = null;

        uint mCellWidth, mCellHeight;
        private Vector2 mInnerGridSize;
        private float mCellSize = 0;
        public float CellSize
        {
            get { return mCellSize; }
        }

        private Vector2 mCellLevelOffset;

        private float mCellSizeWithPadding = 0;
        public float CellSizeWithPadding
        {
            get { return mCellSizeWithPadding; }
        }

        public Grid(uint width, uint height, float cellSize, float cellSizeWithPadding, Vector2 cellOffset)
        {
            mCellLevelOffset = cellOffset;
            mCellWidth = width;
            mCellHeight = height;

            mCells = new List<Cell[]>();
            mCells.Add(null);
            InitCells(0);

            mCellSize = cellSize;
            mCellSizeWithPadding = cellSizeWithPadding;

            mInnerGridSize = Vector2.zero;
            mInnerGridSize.x = width * mCellSize;
            mInnerGridSize.y = height * mCellSize;
        }

        private void InitCells(int index)
        {
            mCells[index] = new Cell[mCellWidth * mCellHeight];
            for (int y = 0; y < mCellHeight; ++y)
                for (int x = 0; x < mCellWidth; ++x)
                {
                    mCells[index][y * mCellHeight + x] = new Cell(new Vector2Int(x, y));
                }
        }

        private Cell _TakeOverFromCenterImp(int index)
        {
            Cell[] cells = mCells[index];
            int N = cells.Length;
            int left = N / 2 - 1;
            int right = N / 2;
            int targetIndex = -1;
            while (left >= 0 && right < N)
            {
                if (cells[left].IsEmpty)
                {
                    targetIndex = left;
                    break;
                }
                if (cells[right].IsEmpty)
                {
                    targetIndex = right;
                    break;
                }
                left--;
                right++;
            }

            if (targetIndex == -1)
                return null;

            cells[targetIndex].IsEmpty = false;
            return cells[targetIndex];
        }

        public (Cell Cell, int Level) TakeOverFromCenter()
        {
            Cell targetCell = null;
            int index = -1;
            for (int i = 0; i < mCells.Count; ++i)
            {
                targetCell = _TakeOverFromCenterImp(i);
                if (targetCell != null)
                {
                    index = i;
                    break;
                }
            }
            if (targetCell == null)
            {
                mCells.Add(null);
                InitCells(mCells.Count - 1);
                targetCell = _TakeOverFromCenterImp(mCells.Count - 1);
                index = mCells.Count - 1;


            }

            Debug.Assert(targetCell != null);
            return (targetCell, index);
        }

        public Vector3 GetPositionByCell(Grid.Cell cell, Vector3 gridCenter, int level)
        {
            Vector3 position = Vector3.zero;

            Vector3 gridElementStartPosition = Vector3.zero;
            gridElementStartPosition.x = gridCenter.x - mInnerGridSize.x / 2.0f + mCellSize / 2.0f;
            gridElementStartPosition.y = gridCenter.y + mInnerGridSize.y / 2.0f - mCellSize / 2.0f;

            position.x = gridElementStartPosition.x + cell.Coordinate.x * mCellSize;
            position.y = gridElementStartPosition.y - cell.Coordinate.y * mCellSize;

            var levelFactor = (level % 2);
            position.x += levelFactor * mCellLevelOffset.x;
            position.y -= level * mCellLevelOffset.y;
            position.z -= 0.01f * level;

            return position;
        }
    }

    private Grid mSoldierGrid = null;

    // SoldierQueue
    public SoldierQueue _Queue = null;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.Assert(_Queue != null);
#endif
    }

    private void Start()
    {
        string path = "Assets/Bundles/Res/Prefabs/Battle/";

        var soldierConfigTable = DataManager.DataTable.TbSoldierConfig;

        foreach(var item in soldierConfigTable.DataList)
        {
            mSoldierTemplat.Add(item.Id, AssetDatabase.LoadAssetAtPath<GameObject>(path + item.Resource+".prefab"));
        }

        MarbleEventManager.OnBrickDestory.AddListener(OnBrickDestory);
        BattleEventManager.OnSoldierDrop.AddListener(OnSoldierDrop);

        GenerateBrickGrid();
    }

    void GenerateBrickGrid()
    {
        var cellCount = GetCellCount();
        mSoldierGrid = new Grid((uint)cellCount.x, (uint)cellCount.y, _CellSize, GetCellSizeWithPadding(), _CellLevelOffset);
    }

    Vector2Int GetCellCount()
    {
#if UNITY_EDITOR
        // check cell size
        _CellSize = Mathf.Min(_GridSize.y, _CellSize);
#endif

        Vector2 CellSize = new Vector2(_CellSize, _CellSize);
        Vector2 cellCountFloat = _GridSize / CellSize;
        Vector2Int cellCount = new Vector2Int((int)cellCountFloat.x, 1);
        return cellCount;
    }

    float GetCellSizeWithPadding()
    {
        return _CellSize * (1 - 2 * _Padding);
    }

    void SpawnSoldier(GameObject soldierObject)
    {
        // assemble brick object
        // position and scale
        //Grid.Cell targetCell = null;
        var targetCell = mSoldierGrid.TakeOverFromCenter();

        soldierObject.transform.position = mSoldierGrid.GetPositionByCell(targetCell.Cell, transform.position, targetCell.Level);
        var bodyRender = soldierObject.GetComponent<PlayerGuards>().BodyRenderer;

        Vector3 prefabSize = soldierObject.transform.localScale;
        Vector3 curSize = bodyRender.bounds.size;
        curSize.x /= prefabSize.x;
        curSize.y /= prefabSize.y;
        float factorX = mSoldierGrid.CellSizeWithPadding / curSize.x;
        float factorY = mSoldierGrid.CellSizeWithPadding / curSize.y;
        float factor = Mathf.Max(factorX, factorY);
        soldierObject.transform.localScale = new Vector3(factor, factor, 1);
    }

    private void OnDrawGizmos()
    {
        if (!_ShowGizmo)
            return;

        Vector2Int cellCount = GetCellCount();

        Vector2 CellSize = new Vector2(_CellSize, _CellSize);
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

        for (int l = 0; l < _GizmoLevels; ++l)
        {
            for (int x = 0; x < cellCount.x; ++x)
            {
                for (int y = 0; y < cellCount.y; ++y)
                {
                    gridElementPosition.x = gridElementStartPosition.x + x * _CellSize;
                    gridElementPosition.y = gridElementStartPosition.y - y * _CellSize;

                    gridElementPosition.x += (l % 2) * _CellLevelOffset.x;
                    gridElementPosition.y -= l * _CellLevelOffset.y;

                    Gizmos.DrawWireCube(gridElementPosition, gridElementSizeV3);
                }
            }
        }
    }

    void OnBrickDestory(BrickController bc)
    {
        GameObject soldierTemplat = null;
        mSoldierTemplat.TryGetValue(bc.BrickID, out soldierTemplat);
        Debug.Assert(soldierTemplat != null, string.Format("tager ID: {0}, is not a Template", bc.BrickID));

        GameObject soldierInstance = GameObject.Instantiate<GameObject>(soldierTemplat);

        _Queue.Enqueue(soldierInstance);
    }

    void OnSoldierDrop(GameObject instance)
    {
        _Queue.Extract(instance);

        instance.transform.SetParent(transform.parent);
    }
}
