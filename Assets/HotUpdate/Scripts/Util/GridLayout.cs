using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout
{
   
    // basic element in Grid
    public class Cell
    {
        // from top-left
        public Vector2Int Coordinate = Vector2Int.zero;
        public bool IsEmpty = true;
        public GameObject mItem = null;

        public Cell(Vector2Int coord, Transform layouter, GameObject cellBackTemplate)
        {
            Coordinate = coord;

            mItem = GameObject.Instantiate<GameObject>(cellBackTemplate);
            mItem.transform.SetParent(layouter, false);

            mItem.transform.localPosition = Vector3.zero;
            mItem.transform.localScale = Vector3.one;
        }

        public void Reset()
        {
            IsEmpty = true;
        }
    }

    private List<Cell[]> mCells = null;

    //
    Transform mLayouter = null;
    GameObject mCellBackground = null;

    //
    uint mCellCountX, mCellCountY;       // in logic space
    private Vector2 mInnerGridSize;
    private float mCellSizeInWS = 0;    // in world space
    public float CellSize
    {
        get { return mCellSizeInWS; }
    }

    private Vector2 mCellLevelOffset;

    private float mCellSizeWithPaddingInWS = 0;
    public float CellSizeWithPadding
    {
        get { return mCellSizeWithPaddingInWS; }
    }

    int mCellCount = 0;

    // overlaped Grid, so multiple Grid can be exist with @cellOffset
    public GridLayout(Transform layouter, uint width, uint height, float cellSize, float cellSizeWithPadding, Vector2 cellOffset, GameObject gridCellBackgroundTemplate)
    {
        mLayouter = layouter;
        mCellBackground = gridCellBackgroundTemplate;

        mCellLevelOffset = cellOffset;
        mCellCountX = width;
        mCellCountY = height;

        mCellSizeInWS = cellSize;
        mCellSizeWithPaddingInWS = cellSizeWithPadding;

        mInnerGridSize = Vector2.zero;
        mInnerGridSize.x = width * mCellSizeWithPaddingInWS;
        mInnerGridSize.y = height * mCellSizeWithPaddingInWS;

        mCells = new List<Cell[]>();
        mCells.Add(null);
        InitCells(0);
    }

    // @index means Grid index
    private void InitCells(int index)
    {
        mCells[index] = new Cell[mCellCountX * mCellCountY];
        for (int y = 0; y < mCellCountY; ++y)
            for (int x = 0; x < mCellCountX; ++x)
            {
                Cell curCell = new Cell(new Vector2Int(x, y), mLayouter, mCellBackground);

                // adopt GameObject's localscale to match Cell size
                curCell.mItem.transform.position = GetPositionByCell(curCell, mLayouter.position, index);
                curCell.mItem.transform.localScale = GetScaleByCell(Vector3.one);

                mCells[index][y * mCellCountY + x] = curCell;
            }

        mCellCount += mCells[index].Length;
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

    private Cell _TakeOverByOrderImp(int index)
    {
        Cell[] cells = mCells[index];
        int targetIndex = -1;
        for (int i = 0; i < cells.Length; ++i)
            if (cells[i].IsEmpty)
            {
                targetIndex = i;
                break;
            }

        if (targetIndex == -1)
            return null;

        cells[targetIndex].IsEmpty = false;
        return cells[targetIndex];
    }
    public (Cell Cell, int Level) TakeOverByOrder(bool alloc = false)
    {
        Cell targetCell = null;
        int gridLevel = -1;
        for (int i = 0; i < mCells.Count; ++i)
        {
            targetCell = _TakeOverByOrderImp(i);
            if (targetCell != null)
            {
                gridLevel = i;
                break;
            }
        }

        if (targetCell == null && alloc)
        {
            mCells.Add(null);
            InitCells(mCells.Count - 1);
            targetCell = _TakeOverByOrderImp(mCells.Count - 1);
            gridLevel = mCells.Count - 1;


        }

        return (targetCell, gridLevel);
    }

    public Vector3 GetPositionByCell(Cell cell, Vector3 gridCenter, int level)
    {
        Vector3 position = Vector3.zero;

        Vector3 gridElementStartPosition = Vector3.zero;
        gridElementStartPosition.x = gridCenter.x - mInnerGridSize.x / 2.0f;
        gridElementStartPosition.y = gridCenter.y + mInnerGridSize.y / 2.0f - mCellSizeWithPaddingInWS / 2.0f;

        position.x = gridElementStartPosition.x + cell.Coordinate.x * mCellSizeWithPaddingInWS + mCellSizeWithPaddingInWS / 2.0f;
        position.y = gridElementStartPosition.y - cell.Coordinate.y * mCellSizeWithPaddingInWS;
        position.z = gridCenter.z;

        // for interleaved offset
        var levelFactor = (level % 2);
        position.x += levelFactor * mCellLevelOffset.x;
        position.y -= level * mCellLevelOffset.y;
        position.z -= 0.01f * level;

        return position;
    }

    public Vector3 GetScaleByCell(Vector3 contentSize)
    {
        float factorX = (CellSize / contentSize.x);
        float factorY = (CellSize / contentSize.y);
        float factor = Mathf.Max(factorX, factorY);
        return new Vector3(factor, factor, 1);
    }

    public void ResetCells()
    {
        for (int i = 0; i < mCells.Count; ++i)
        {
            Cell[] curCell = mCells[i];
            for (int j = 0; j < curCell.Length; ++j)
                curCell[j].Reset();
        }
    }

    // request fresh
    public void RefreshView(List<GameObject> toBeLayoutInstance)
    {
        ResetCells();

        int refreshCount = Mathf.Min(mCellCount, toBeLayoutInstance.Count);

        for(int i = 0; i < refreshCount; ++i)
        {
            // get a cell
            var targetCell = TakeOverByOrder();
            Debug.Assert(targetCell.Cell != null);

            // init this instance
            GameObject curInstance = toBeLayoutInstance[i];
            curInstance.transform.SetParent(null, false);
            
            var bodyRender = curInstance.GetComponent<PlayerGuards>().BodyRenderer;
            targetCell.Cell.mItem.transform.localScale = GetScaleByCell(bodyRender.bounds.size);

            // place in cell
            curInstance.transform.SetParent(targetCell.Cell.mItem.transform, false);
        }
        
    }
}
