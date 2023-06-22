using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace WorkTools
{
    public static class GUIDReplace
    {

        private static List<Object> m_replacePrefabs = new List<Object>();

        /// <summary>
        ///  查找
        /// </summary>
        /// <param name="old">检索的object</param>
        /// <param name="replacePrefabs">返回替换预设</param>
        public static void Find(Object old)
        {
            //防报错
            if (old == null) { return; }
            if (EditorSettings.serializationMode != SerializationMode.ForceText)
                Debug.LogError("需将Edit-->Editor-->Asset Serialization模式设置为:ForceText");

            //获取旧的GUID
            var oldGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(old));

            //获取所有预设
            var prefabs = new List<string>();
            UtilEditor.SearchAllFiles("Assets", prefabs, new List<string> { ".prefab" });

            for (int i = 0; i < prefabs.Count; i++)
            {
                var prefab = prefabs[i];
                var content = File.ReadAllText(prefab);
                if (content.Contains(oldGUID))
                {
                    var go = AssetDatabase.LoadAssetAtPath(prefab, typeof(Object));
                    m_replacePrefabs.Add(go);
                    Debug.LogWarning("引用:" + go.name, go);
                }
                EditorUtility.DisplayProgressBar("查找预设", prefab, i * 1.0f / prefabs.Count);
            }
            EditorUtility.ClearProgressBar();
        }

        public static void Replace(Object old, Object replace)
        {
            if (old != null && replace != null)
            {
                if (m_replacePrefabs.Count > 0)
                {
                    //获取旧的GUID
                    var oldGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(old));
                    //获取新的GUID
                    var newGUID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(replace));

                    for (int i = 0; i < m_replacePrefabs.Count; i++)
                    {
                        var prefab = m_replacePrefabs[i];
                        //读取信息替换
                        var content = File.ReadAllText(AssetDatabase.GetAssetPath(prefab));
                        content = content.Replace(oldGUID, newGUID);
                        File.WriteAllText(AssetDatabase.GetAssetPath(prefab), content);
                        AssetDatabase.SaveAssets();
                    }
                    AssetDatabase.Refresh();
                    m_replacePrefabs.Clear();
                }
            }
        }
    }
}