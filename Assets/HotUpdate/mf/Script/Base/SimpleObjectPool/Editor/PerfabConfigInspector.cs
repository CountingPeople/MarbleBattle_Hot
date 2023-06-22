using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

[CustomEditor(typeof(PerfabConfig))]
public class PerfabConfigInspector : Editor
{
    public SerializedProperty perfabList;
    string selectPath;

    private void OnEnable()
    {
        perfabList = serializedObject.FindProperty("perfabList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.indentLevel = 1;

        EditorGUILayout.TextField("配置文件路径", GameConfig.perfabConfigPath);
        EditorGUILayout.TextField("当前选择路径", selectPath);

        EditorGUILayout.PropertyField(perfabList, new GUIContent("预制体分组"));
        GUILayout.Space(5);

        if (GUILayout.Button("初始化路径"))
        {
            GameConfig.perfabConfigPath = GetPath("PerfabConfig.asset");
            selectPath = GetSelectedPathOrFallback();
        }
        if (GUILayout.Button("生成配置文件"))
        {
            GetAll();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string GetPath(string conStr)
    {
        string path=null;
        if (Directory.Exists(Application.dataPath))
        {
            DirectoryInfo direction = new DirectoryInfo(Application.dataPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                if (files[i].FullName.Contains("Art"))
                {
                    continue;
                }
                if (files[i].Name.Contains(conStr))
                {
                    string tempPath = files[i].FullName;
                    var idx = tempPath.IndexOf("Asset");
                    StringBuilder temp = new StringBuilder();
                    for (int m = idx; m < tempPath.Length; m++)
                    {
                        temp.Append(tempPath[m]);
                    }
                    path = temp.ToString();
                    break;
                }
            }
        }
        return path;
    }


    private void GetAll()
    {
        PerfabConfig perfabConfig = AssetDatabase.LoadAssetAtPath<PerfabConfig>(GameConfig.perfabConfigPath);
        if (perfabConfig == null)
        {
            Debug.LogError($"预制体配置文件的路径不对  请从新配置{GameConfig.perfabConfigPath}");
            return ;
        }
        StringBuilder content = new StringBuilder();

        int enumIdx = 0;
        List<string> perfabPaths = new List<string>();
        for (int i = 0; i < perfabConfig.perfabList.Count; ++i)
        {
            var temp = perfabConfig.perfabList[i];
            var className = $"{temp.name}Enum";
            var actionStr = GetList(temp.perfabList);
            var enumClassStr = GenEnumStr(actionStr, className,ref enumIdx);
            content.Append(enumClassStr);
            content.Append("\n");

            for (int j = 0; j < temp.perfabList.Count; j++)
            {
                string findPath = GetPath(temp.perfabList[j].name);
                string perPath = findPath.Replace("\\", "/");//.Replace("Assets/Resources/","").Replace(".prefab","");
                perfabPaths.Add(perPath);
            }
        }

        content.AppendLine("public partial class PerfabPoolData");
        content.AppendLine("{");
        content.AppendLine("    private string[] perfabPathArr = new string[]");
        content.AppendLine("    {");
        foreach (var item in perfabPaths)
        {
            content.AppendLine("        " + '"' + item + '"' + ",");
        }
        content.AppendLine("    };");
        content.AppendLine("}");


        File.WriteAllText(GetSelectedPathOrFallback() + "/PerfabEnum.cs", content.ToString(), new UTF8Encoding());
        AssetDatabase.Refresh();

    }

    private string GenEnumStr(string str,string className,ref int idxFirst)
    {
        string[] actions = str.Split('|');
        StringBuilder text = new StringBuilder();
        text.AppendLine($"public enum {className}");
        text.AppendLine("{");

        string[] enumNameArr = null;
        string enumName = null;
        foreach (var item in actions)
        {
            enumNameArr = item.Split('/');
            enumName = enumNameArr[enumNameArr.Length - 1];
            text.AppendLine("    " + enumName.Replace(" ", "").Trim() + $"={idxFirst}" + ",");
            idxFirst++;
        }
        text.AppendLine("}");

        return text.ToString();
    }


    private string GetList(List<GameObject> objList)
    {
        List<string> perfabNameList = new List<string>();
        for (int i = 0; i < objList.Count; i++)
        {
            perfabNameList.Add(objList[i].name);
        }
        string str = string.Join("|", perfabNameList);
        return str.Trim();
    }

    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}
