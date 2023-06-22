using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 预制体数据单元
/// </summary>
public class PerfabDto
{
    private int instId;
    private bool isFree;
    public float genTime;
    public GameObject obj;

    public PerfabDto(GameObject perfab)
    {
        obj = GameObject.Instantiate(perfab);
        //obj.name = $"{genTime.ToString()}  gen";
    }

    public void UseObj()
    {
        genTime= Time.time;
        isFree = false;
        obj.gameObject.SetActive(true);
        //obj.name = $"{genTime.ToString()}  use  0";
    }
    public void FreeObj()
    {
        isFree = true;
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(PerfabTool.Instance.transform);
        //obj.name = $"{genTime.ToString()}  free  1";
    }

    public bool GetState()
    {
        return isFree;
    }

    public int GetStateValue()
    {
        return isFree ? 1 : 0;
    }

    public void SetInstId(int _instId)
    {
        instId = _instId;
    }

    public int GetInstId()
    {
        return instId;
    }
}

/// <summary>
/// 对象池数据管理
/// </summary>
public partial class PerfabPoolData
{
    Dictionary<int, GameObject> perfabDic = new Dictionary<int, GameObject>();
    Dictionary<int, List<PerfabDto>> poolDic = new Dictionary<int, List<PerfabDto>>();

    public void Init()
    {
        for (int i = 0; i < perfabPathArr.Length; i++)
        {
            var perfab = ResourcesModule.Instance.Load<GameObject>(perfabPathArr[i],true,GameContant.LocalBundles);
            perfabDic.Add((int)i, perfab);
        }
    }

    public PerfabDto GetNormalObj(int _temp)
    {
        if (!perfabDic.ContainsKey(_temp))
        {
            return null;
        }
        var perfab = perfabDic[_temp];
        PerfabDto perfabDto = new PerfabDto(perfab);
        return perfabDto;
    }

    public PerfabDto CreateObj(int _temp)
    {
        if (!perfabDic.ContainsKey(_temp))
        {
            return null;
        }
        var perfab = perfabDic[_temp];
        if (perfab == null)
        {
            Debug.LogError(_temp);
        }
        PerfabDto perfabDto = new PerfabDto(perfab);
        if (!poolDic.ContainsKey(_temp))
        {
            poolDic.Add(_temp, new List<PerfabDto>());
        }
        poolDic[_temp].Add(perfabDto);
        return perfabDto;
    }

    public PerfabDto GetObj(int _temp)
    {
        PerfabDto per = null;
        if (poolDic.ContainsKey(_temp) && poolDic[_temp].Count > 0)
        {
            per = poolDic[_temp].Find(item => item.GetState());
        }
        if(per==null||per.obj==null)
        {
            per = CreateObj(_temp);
        }
        if (per != null)
        {
            per.UseObj();
        }
        else
        {
            Debug.LogError($"没有取到{_temp}类型的预制体");
        }
        return per;
    }

    public void FreeByInstId(int _temp,int instId)
    {
        if (poolDic.ContainsKey(_temp) && poolDic[_temp].Count > 0)
        {
           var per= poolDic[_temp].Find(item => item.GetInstId() == instId);
           if (per != null)
           {
              per.FreeObj();
           }
        }
    }

    public void FreeObj(PerfabDto perfabDto) 
    {
        perfabDto.FreeObj();
    }

    public void FreeObjFirst(int _temp)
    {
        if (poolDic.ContainsKey(_temp) && poolDic[_temp].Count > 0)
        {
            poolDic[_temp].Sort((x,y)=> 
            {
                if (x.GetStateValue() != y.GetStateValue())
                {
                    return x.GetStateValue().CompareTo(y.GetStateValue());
                }
                else
                {
                    if (x.GetStateValue() == 0)
                    {
                        return x.genTime.CompareTo(y.genTime);
                    }
                    else 
                    {
                        return -x.genTime.CompareTo(y.genTime);
                    }
                }
            });

            poolDic[_temp][0].FreeObj();
        }
    }

    public void FreeAll()
    {
        foreach (var item in poolDic)
        {
            foreach (var item2 in item.Value)
            {
                item2.FreeObj();
            }
        }
    }

}


/// <summary>
/// 对象池外部接口调用
/// </summary>
public class PerfabTool : MonoSingleton<PerfabTool>
{
    public PerfabPoolData playerPoolData;

    /// <summary>
    /// 初始化对象池
    /// </summary>
    public override void Init()
    {
        playerPoolData = new PerfabPoolData();
        playerPoolData.Init();
    }

    /// <summary>
    /// 获取一个对象
    /// </summary>
    /// <param name="_temp"></param>
    /// <returns></returns>
    public PerfabDto GetObj(object _temp)
    {
        return playerPoolData.GetObj((int)_temp);
    }
    /// <summary>
    /// 释放一个对象
    /// </summary>
    /// <param name="_temp"></param>
    public void FreeObj(PerfabDto _temp)
    {
        playerPoolData.FreeObj(_temp);
    }
    /// <summary>
    /// 释放第一个对象
    /// </summary>
    /// <param name="_temp"></param>
    public void FreeFirst(object _temp)
    {
        playerPoolData.FreeObjFirst((int)_temp);
    }
    /// <summary>
    /// 根据实例id释放对象
    /// </summary>
    /// <param name="_temp"></param>
    /// <param name="instId"></param>
    public void FreeByInstId(object _temp, int instId)
    {
        playerPoolData.FreeByInstId((int)_temp, instId);
    }

    /// <summary>
    /// 获取一个普通的obj
    /// </summary>
    /// <param name="_temp"></param>
    /// <returns></returns>
    public PerfabDto GetNormalObj(object _temp)
    {
        return playerPoolData.GetNormalObj((int)_temp);
    }

    public void FreeAll()
    {
        playerPoolData.FreeAll();
    }

}
