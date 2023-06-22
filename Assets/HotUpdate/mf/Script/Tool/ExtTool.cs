using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class ExtTool : GetInstance<ExtTool>
{
    public void SaveDataJson(Dictionary<string, List<string>> player)
    {
        Dictionary<string, List<string>> jsonRoleDto = player;

        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
                Application.persistentDataPath + "/localPlayerData.json";
#elif UNITY_IPHONE && !UNITY_EDITOR
               Application.persistentDataPath + "/localPlayerData.json";
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
               Application.dataPath + "/localPlayerData.json";
#else
               string.Empty;
#endif
        var content = JsonConvert.SerializeObject(jsonRoleDto);
        File.WriteAllText(path, content);
    }

    public Dictionary<string, List<string>> LoadDataJson()
    {
        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
               Application.persistentDataPath + "/localPlayerData.json";
#elif UNITY_IPHONE && !UNITY_EDITOR
               Application.persistentDataPath + "/localPlayerData.json";
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
                Application.dataPath + "/localPlayerData.json";
#else
                string.Empty;
#endif
        if (File.Exists(path))
        {
            var content = File.ReadAllText(path);
            var playerData = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(content);
            return playerData;
        }
        else
        {
            Debug.Log("Save file not found in  " + path);
            return null;
        }
    }

//    public void SavePlayerJson(GameDataDto player)
//    {
//        GameDataDtoJson jsonRoleDto = player;

//        string path =
//#if UNITY_ANDROID && !UNITY_EDITOR
//                Application.persistentDataPath + "/localPlayerData.json";
//#elif UNITY_IPHONE && !UNITY_EDITOR
//               Application.persistentDataPath + "/localPlayerData.json";
//#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
//               Application.dataPath + "/localPlayerData.json";
//#else
//               string.Empty;
//#endif
//        var content = JsonConvert.SerializeObject(jsonRoleDto);
//        File.WriteAllText(path, content);
//    }

//    public GameDataDto LoadPlayerJson()
//    {
//        string path =
//#if UNITY_ANDROID && !UNITY_EDITOR
//               "file://"+Application.persistentDataPath + "/localPlayerData.json";
//#elif UNITY_IPHONE && !UNITY_EDITOR
//               Application.persistentDataPath + "/localPlayerData.json";
//#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
//                Application.dataPath + "/localPlayerData.json";
//#else
//                string.Empty;
//#endif
//        if (File.Exists(path))
//        {
//            var content = File.ReadAllText(path);
//            var playerData = JsonConvert.DeserializeObject<GameDataDtoJson>(content);
//            return playerData;
//        }
//        else
//        {
//            Debug.Log("Save file not found in  " + path);
//            return null;
//        }
//    }

    /// <summary>
    /// 获取当前时间戳
    /// </summary>
    public Int64 GetTimeSpanNow()
    {
        TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);//获取时间戳
        return Convert.ToInt64(st.TotalSeconds);

    }

    /// <summary>
    /// 获取转换后的时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public Int64 GetTimeSpanFormat(DateTime dateTime)
    {
        TimeSpan st = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);//获取时间戳
        return Convert.ToInt64(st.TotalSeconds);
    }

    /// <summary>
    /// 获取经过多少时间
    /// </summary>
    /// <param name="lastTimeSpan"></param>
    /// <returns></returns>
    public Int64 GetTimeSpanPass(Int64 lastTimeSpan)
    {
        var now = GetTimeSpanNow();
        var dis = now - lastTimeSpan;
        return dis;
    }

    /// <summary>
    /// 获取日期
    /// </summary>
    /// <returns></returns>
    public DateTime GetTimeDate(Int64 timeSpan)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));//获取时间戳
        DateTime dt = startTime.AddSeconds(timeSpan);
        //string t = dt.ToString("yyyy/MM/dd HH:mm:ss");//转化为日期时间
        //Debug.Log(t);
        return dt;
    }


    private const int FALSE = 0;
    private const int TRUE = 1;
    private static int _valueLock = 0;
    private static int _instanceId = 0;

    private static Queue<int> _instanceCacheQueue = null;
    static ExtTool()
    {
        _instanceId = int.MinValue;
        _instanceCacheQueue = new Queue<int>();
    }

    /// <summary>
    /// 获取实例ID
    /// </summary>
    /// <returns></returns>
    internal  int GetInstanceId()
    {
    Begin: if (Interlocked.CompareExchange(ref _valueLock, TRUE, FALSE) == FALSE)
        {

            int result = int.MinValue;
            if (_instanceCacheQueue.Count < 1)
            {
                result = _instanceId;
                _instanceId++;
            }
            else
            {
                result = _instanceCacheQueue.Dequeue();
            }
            Interlocked.Exchange(ref _valueLock, FALSE);
            return result;
        }
        else
        {
            Thread.Sleep(10);
            goto Begin;
        }
    }

    /// <summary>
    /// 回收实例ID
    /// </summary>
    /// <returns></returns>
    internal  void RecoverInstanceId(int instanceId)
    {
    Begin: if (Interlocked.CompareExchange(ref _valueLock, TRUE, FALSE) == FALSE)
        {
            if (!_instanceCacheQueue.Contains(instanceId))
                _instanceCacheQueue.Enqueue(instanceId);
            Interlocked.Exchange(ref _valueLock, FALSE);
        }
        else
        {
            Thread.Sleep(10);
            goto Begin;
        }
    }
}
