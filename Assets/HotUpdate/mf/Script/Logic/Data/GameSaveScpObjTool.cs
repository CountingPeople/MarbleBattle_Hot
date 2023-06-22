using Framework;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSaveScpObjTool
{

    private Dictionary<string, object> achiveDbCaches = new Dictionary<string, object>();
    private Dictionary<string, List<string>> cacheStrDic = new Dictionary<string, List<string>>();

    public void InitData()
    {
        var tempCache  = ExtTool.Instance.LoadDataJson();
        if (tempCache!=null)
        {
            cacheStrDic = tempCache;
        }
    }

    private void SaveDataList<T>(List<string> strList)
    {
        List<T> tempList = new List<T>();
        for (int i = 0; i < strList.Count; i++)
        {
            T item = JsonConvert.DeserializeObject<T>(strList[i]);
            tempList.Add(item);
        }
        achiveDbCaches[typeof(T).Name] = tempList;
    }

    public void CreateTable<T>()
    {
        List<string> tempStrList = null;
        if (!cacheStrDic.ContainsKey(typeof(T).Name))
        {
            tempStrList= new List<string>();
            cacheStrDic[typeof(T).Name] = tempStrList;
        }
        else {
            tempStrList = cacheStrDic[typeof(T).Name];
        }

        SaveDataList<T>(tempStrList);
    }

    public List<T> Table<T>()
    {
        if (achiveDbCaches.TryGetValue(typeof(T).Name, out object list))
        {
            return (List<T>)list;
        }
        else
        {
            throw new ArgumentException($"The type {typeof(T)} was not found in the cache.");
        }
    }

    public List<string> GetStrList<T>()
    {
        if (cacheStrDic.TryGetValue(typeof(T).Name, out List<string> list))
        {
            return list;
        }
        else
        {
            throw new ArgumentException($"The type {typeof(T)} was not found in the cache.");
        }
    }


    public T Find<T>(Func<T, bool> predicate)
    {
        List<T> tempList = Table<T>();
        if (tempList.Count > 0)
        {
            var temp = tempList.Where(predicate).ToList();

            if (temp.Count > 0)
            {
                var result = temp.First();
                return result;
            }

        }
        return default(T);
    }

    public void Insert<T>(T dto)
    {
        List<string> tempStrList = GetStrList<T>();
        List<T> tempDataList = Table<T>();
        var content = JsonConvert.SerializeObject(dto);
        tempDataList.Add(dto);
        tempStrList.Add(content);
        SaveData();
    }

    public void Updata<T>(T dto)
    {
        List<string> tempStrList = GetStrList<T>();
        List<T> tempDataList = Table<T>();
        var content = JsonConvert.SerializeObject(dto);
        int idx = tempDataList.IndexOf(dto);
        tempDataList[idx] = dto;
        tempStrList[idx] = content;
        SaveData();
    }

    public void Delete<T>(T dto)
    {
        List<string> tempStrList = GetStrList<T>();
        List<T> tempDataList = Table<T>();
        int idx = tempDataList.IndexOf(dto);
        tempDataList.RemoveAt(idx);
        tempStrList.RemoveAt(idx);
        SaveData();
    }

    public void SaveData()
    {
        ExtTool.Instance.SaveDataJson(cacheStrDic);
    }

}
