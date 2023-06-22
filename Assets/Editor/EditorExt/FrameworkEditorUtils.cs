using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//using static TSFrame.Editor.Properties.Resources;

namespace Framework.Editor
{
    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class UIExportAttribute : Attribute
    {

    }
    [System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class MVVMTypeAttribute : Attribute
    {

    }
    internal static class FrameworkEditorUtils
    {
        //internal static string CONFIG_FILE_PATH = Application.dataPath + "/../config";
        internal static string CONFIG_FILE_PATH = null;
        internal static FrameworkConfig FRAMEWORK_CONFIG = null;
        internal const string TAG_NAME = "Export";


        /// <summary>
        /// 检测配置是否完整
        /// </summary>
        /// <returns></returns>
        internal static bool CheckConfig()
        {
            if (string.IsNullOrWhiteSpace(CONFIG_FILE_PATH))
            {
                string[] guids = AssetDatabase.FindAssets(typeof(FrameworkConfig).Name);
                if (guids.Length != 1)
                {
                    Debug.LogError("guids存在多个");
                }
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                path = Path.GetDirectoryName(path);
                CONFIG_FILE_PATH = path + "/Config.asset";
                if (!File.Exists(CONFIG_FILE_PATH))
                {
                    FRAMEWORK_CONFIG = ScriptableObject.CreateInstance<FrameworkConfig>();
                    AssetDatabase.CreateAsset(FRAMEWORK_CONFIG, CONFIG_FILE_PATH);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            if (FRAMEWORK_CONFIG == null)
            {
                FRAMEWORK_CONFIG = AssetDatabase.LoadAssetAtPath<FrameworkConfig>(CONFIG_FILE_PATH);
            }

            Assembly assembly = typeof(FrameworkEditorUtils).Assembly;
            Type[] types = assembly.GetTypes();
            Type dicType = typeof(Dictionary<string, string>);
            foreach (var item in types)
            {
                FieldInfo[] fieldInfos = item.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                foreach (var field in fieldInfos)
                {
                    var exportAttr = field.GetCustomAttribute<UIExportAttribute>();
                    if (exportAttr != null)
                    {
                        if (field.FieldType == dicType)
                        {
                            Dictionary<string, string> dic = field.GetValue(null) as Dictionary<string, string>;
                            foreach (var value in dic)
                            {
                                if (FRAMEWORK_CONFIG.UIExportDic.ContainsKey(value.Key))
                                {
                                    FRAMEWORK_CONFIG.UIExportDic[value.Key] = value.Value;
                                }
                                else
                                {
                                    FRAMEWORK_CONFIG.UIExportDic.Add(value.Key, value.Value);
                                }
                            }
                        }
                    }
                }
            }

            return FRAMEWORK_CONFIG.CheckPath();
        }
        internal static string[] GetExportType(string name)
        {
            int index = name.IndexOf("_");
            string str = name;
            if (index > 0)
            {
                str = name.Substring(0, index);
            }
            else
            {
                Debug.LogError("没有对应的前缀:" + name);
            }
            string[] strs = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] typeNames = new string[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                string exportName = strs[i] + "_";
                if (FRAMEWORK_CONFIG.UIExportDic.ContainsKey(exportName))
                {
                    typeNames[i] = FRAMEWORK_CONFIG.UIExportDic[exportName];
                }
                if (string.IsNullOrWhiteSpace(typeNames[i]))
                {
                    typeNames[i] = typeof(GameObject).Name;
                }
            }
            return typeNames;
        }

        /// <summary>
        /// 获取所有需要生成的Tran
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="parentPath"></param>
        /// <param name="trans"></param>
        internal static void GetTrans(Transform transform, string parentPath, List<TranDto> trans)
        {
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform tran = transform.GetChild(i);
                    string path = parentPath;
                    if (transform.parent != null)
                    {
                        path += (transform.name + "/");
                    }
                    GetTrans(tran, path, trans);
                }

            }
            if (transform.tag == TAG_NAME)
            {
                TranDto tranDto = new TranDto();
                tranDto.Tran = transform;
                int startNum = transform.name.IndexOf('_');
                string str = transform.name.Substring(startNum + 1, transform.name.Length - startNum - 1);
                tranDto.ComOriginalName = str;
                tranDto.ParentPath = parentPath;
                trans.Add(tranDto);
            }
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        static void CheckConfigFile()
        {
            CheckConfig();
            InitTag();

        }

        private static void InitTag()
        {
            // Open tag manager
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            // Tags Property
            SerializedProperty tagsProp = tagManager.FindProperty("tags");

            //Debug.Log("TagsPorp Size:" + tagsProp.arraySize);
            List<string> tags = new List<string>();
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                tags.Add(tagsProp.GetArrayElementAtIndex(i).stringValue);
            }

            if (!tags.Contains(TAG_NAME))
            {
                tags.Add(TAG_NAME);
                tagsProp.ClearArray();

                tagManager.ApplyModifiedProperties();


                for (int i = 0; i < tags.Count; i++)
                {
                    // Insert new array element
                    tagsProp.InsertArrayElementAtIndex(i);
                    SerializedProperty sp = tagsProp.GetArrayElementAtIndex(i);
                    // Set array element to tagName
                    sp.stringValue = tags[i];

                    tagManager.ApplyModifiedProperties();
                }
            }
        }

    }
}
