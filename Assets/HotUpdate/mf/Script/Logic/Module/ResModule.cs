using Framework;
using UnityEngine;

public class ResModule : BaseModule<ResModule>
{

    public override void Init()
    {
        base.Init();


    }

    public GameObject GetRoleObj(int id) 
    {
        var prefab = Resources.Load($"role/{id}");
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        obj.transform.position = GameObject.Find("spawn").transform.position;
        return obj;
    }


    public GameObject GetObj(object type, Transform parent, Vector3 pos,Vector3 angle)
    {
        var obj = PerfabTool.Instance.GetNormalObj(type);
        obj.obj.transform.SetParent(parent);
        obj.obj.transform.localPosition = pos;
        obj.obj.transform.localEulerAngles = angle;
        return obj.obj;
    }


}
