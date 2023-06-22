//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using UnityEngine;
//using Newtonsoft.Json;

//public interface ITable { }

//public abstract class TableBase<T> : ITable where T : TableBase<T>, new()
//{
//    public static T Instance { get; private set; }
//    public static void Initialize()
//    {
//        Instance = Activator.CreateInstance<T>();
//        string tableName = (typeof(T)).ToString();//.Replace("Table","");
//        string jsonStr = File.ReadAllText($"{Application.streamingAssetsPath}/table/{tableName}.json", Encoding.UTF8);
//        var dataList = JsonConvert.DeserializeObject<List<T>>(jsonStr);

//        Instance.Init(dataList);
//    }

//    public virtual void Init(List<T> dataList)
//    {

//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Framework;

public interface ITable {
}

public abstract class TableBase<T> : ITable where T : TableBase<T>, new()
{
    public static T Instance { get; private set; }
    public static void Initialize()
    {
        Instance = Activator.CreateInstance<T>();
        string tableName = (typeof(T)).ToString();//.Replace("Table","");
        Debug.Log($"开始加载数据{tableName}");
        string jsonStr = ResourcesModule.Instance.Load<TextAsset>($"Assets/Bundles/table/{tableName}.json").text;//ReadFileFromStreamingAsset($"{tableName}.json");
        Debug.Log("成功加载数据");
        var dataList = JsonConvert.DeserializeObject<List<T>>(jsonStr);
        Instance.Init(dataList);
    }

    public static string ReadFileFromStreamingAsset(string file_name)
    {
        string from_path=null;

#if UNITY_EDITOR
        from_path = Application.streamingAssetsPath + "/table/" + file_name;
#else
        if (Application.platform == RuntimePlatform.Android)
        {
            from_path = "jar:file://" + Application.dataPath + "!/assets/table/" + file_name;
        }
#endif
        WWW www = new WWW(from_path);
        while (!www.isDone) { }
        if (www.error == null) return www.text;
        else return www.error;
    }
    public virtual void Init(List<T> dataList)
    {
    }
}