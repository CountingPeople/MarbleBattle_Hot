using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PerfabConfig", menuName = "CreatPerfabConfig", order = 0)]
public class PerfabConfig : ScriptableObject
{
    public List<PerfabItemData> allPerfab = new List<PerfabItemData>();
    public void AddPerfab(PerfabItemData perfabItem)
    {
        var perfabData = allPerfab.Find(item => item.name == perfabItem.name);
        if (perfabData != null)
        {
            int idx = allPerfab.IndexOf(perfabData);
            allPerfab[idx] = perfabItem;
        }
        else
        {
            allPerfab.Add(perfabItem);
        }
    }

    public List<PerfabClass> perfabList = new List<PerfabClass>();

}

[System.Serializable]
public class PerfabClass {
    public string name;
    public List<GameObject> perfabList = new List<GameObject>();
}

[System.Serializable]
public class PerfabItemData
{
    public PerfabItemData(string guild)
    {
        allPath = AssetDatabase.GUIDToAssetPath(guild);
        string[] strArr = allPath.Split('/');
        name = strArr[strArr.Length-1].Replace(".prefab", "");
        perfabPath = allPath.Replace(GameConfig.resourcesPath, "").Replace(".prefab", "");
        perfab = AssetDatabase.LoadAssetAtPath<GameObject>(allPath);
    }

    public string name;
    public string allPath;
    public string perfabPath;
    public GameObject perfab;
}

