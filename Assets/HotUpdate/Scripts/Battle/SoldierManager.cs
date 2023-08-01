using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    private Dictionary<string, GameObject> mSoldierTemplat = new Dictionary<string, GameObject>();

    // SoldierQueue
    public SoldierQueue _Queue = null;

    static public SoldierManager Instance = null;

    // soldier's size
    public Vector2 _SoldierSize = Vector2.one;

    private void Awake()
    {
        Instance = this;

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
            mSoldierTemplat.Add(item.Id, ResourcesModule.LoadAssetAtPath<GameObject>(path + item.Resource+".prefab"));
        }

        MarbleEventManager.OnBrickDestory.AddListener(OnBrickDestory);

    }

    void OnBrickDestory(BrickController bc)
    {
        GameObject soldierTemplat = null;
        mSoldierTemplat.TryGetValue(bc.BrickID, out soldierTemplat);
        Debug.Assert(soldierTemplat != null, string.Format("tager ID: {0}, is not a Template", bc.BrickID));

        GameObject soldierInstance = GameObject.Instantiate<GameObject>(soldierTemplat);

        _Queue.Enqueue(soldierInstance);
    }

    public bool TryDropSoldier(GameObject instance)
    {
        bool canDrop = false;
        Vector3 screenPosition = CameraManager.Instance.MarbleCamera.WorldToScreenPoint(instance.transform.position);
        screenPosition.z = transform.position.z - CameraManager.Instance.BattleCamera.transform.position.z;
        
        var instancePositionInBattle = CameraManager.Instance.BattleCamera.ScreenToWorldPoint(screenPosition);
        canDrop = Physics2D.Raycast(instancePositionInBattle, Vector2.down, float.MaxValue, LayerMask.GetMask("SoldierGroud"));

        if (!canDrop)
            return canDrop;

        // can drop
        //
        _Queue.Extract(instance);

        instance.transform.SetParent(transform, false);

        var positionInMarble = transform.transform.position;

        instance.transform.position = instancePositionInBattle;
        instance.transform.localRotation = Quaternion.Euler(Vector3.zero);

        // scale
        // i think this code could be more elegant
        var renderer = instance.GetComponent<PlayerGuards>().BodyRenderer;
        var renderBoundsSize = renderer.bounds.size;

        var sizeFactorX = _SoldierSize.x / renderBoundsSize.x;
        var sizeFactorY = _SoldierSize.y / renderBoundsSize.y;

        var localScale = instance.transform.localScale;
        localScale.x *= sizeFactorX;
        localScale.y *= sizeFactorY;
        instance.transform.localScale = localScale;

        return true;
    }

    private void OnDrawGizmos()
    {
        Vector3 size = _SoldierSize;
        size.z = 0.1f;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
