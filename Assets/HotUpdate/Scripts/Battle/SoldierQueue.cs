using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoldierQueue : MonoBehaviour
{
    /*
     *  Gizmo
     */
    public Vector2 _GridSize;
    public float _CellSize;
    [Range(0.0f, 0.5f)]
    public float _Padding;
    public bool _ShowGizmo = false;

    /*
     *  Logic
     */
    List<GameObject> mQueue = new List<GameObject>();

    /*
     * Grid View
     */

    GridLayout mGridLayout = null;
    private GameObject mGridElementBackground = null;

    private void Awake()
    {
        int cellCountX = GetCellCount();
        mGridElementBackground = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Bundles/Res/Prefabs/SoldierQueue/SoldierCell.prefab");
        mGridLayout = new GridLayout(transform, (uint)cellCountX, 1, _CellSize, GetCellSizeWithPadding(), Vector2.zero, mGridElementBackground);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // spwan a instance
    public void Enqueue(GameObject soilderInstance)
    {
        mQueue.Add(soilderInstance);
        soilderInstance.GetComponent<PlayerGuards>().SoilderState = PlayerGuards.State.WaitPlace;
        soilderInstance.AddComponent<PlayerGuardDragable>();

        mGridLayout.RefreshView(mQueue);

        // notify
        MarbleEventManager.OnSoldierQueueChanged.Invoke(mQueue.Count);
    }

    public void Extract(GameObject soilderInstance)
    {
        mQueue.Remove(soilderInstance);

        mGridLayout.RefreshView(mQueue);

        // notify
        MarbleEventManager.OnSoldierQueueChanged.Invoke(mQueue.Count);
    }

    /*
     *  Layout
     */
    float GetCellSizeWithPadding()
    {
#if UNITY_EDITOR
        float realSize = _CellSize + 2 * _Padding;

        if (realSize > _GridSize.x || realSize > _GridSize.y)
        {
            Debug.LogError("realSize excess _GridSize");
            int index = realSize > _GridSize.x ? 0 : 1;
            _Padding = (_GridSize[index] - _CellSize) / 2.0f;
        }
#endif

        return _CellSize + 2 * _Padding;
    }

    int GetCellCount()
    {
        float cellSize = GetCellSizeWithPadding();
        return (int)(_GridSize.x / cellSize);
    }

    private void OnDrawGizmos()
    {
        if (!_ShowGizmo)
            return;

        float cellCount = GetCellCount();

        float cellSizeWithPadding = GetCellSizeWithPadding();
        Vector2 CellSizeWithPadding = new Vector2(cellSizeWithPadding, cellSizeWithPadding);

        // for center aligned
        Vector2 innerGridSize = (cellCount * CellSizeWithPadding);

        const float _SizeZ = 0.1f;

        // outter
        Gizmos.color = Color.red;
        Vector3 size = _GridSize;
        size.z = _SizeZ;
        Gizmos.DrawWireCube(transform.position, size);

        // grid element
        Gizmos.color = Color.white;
        Vector3 gridElementStartPosition = Vector3.zero;
        gridElementStartPosition.x = transform.position.x - innerGridSize.x / 2.0f;
        gridElementStartPosition.y = transform.position.y;
        gridElementStartPosition.z = transform.position.z;
        Vector3 gridElementPosition = Vector3.zero;
        Vector3 gridElementSizeV3 = Vector3.one;
        gridElementSizeV3.x = _CellSize;
        gridElementSizeV3.y = _CellSize;
        gridElementSizeV3.z = _SizeZ;


        for (int x = 0; x < cellCount; ++x)
        {
            gridElementPosition.x = gridElementStartPosition.x + x * cellSizeWithPadding + cellSizeWithPadding / 2.0f;
            gridElementPosition.y = gridElementStartPosition.y;
            gridElementPosition.z = gridElementStartPosition.z;

            Gizmos.DrawWireCube(gridElementPosition, gridElementSizeV3);
        }
        
    }
}
