using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;

namespace WorkTools
{
    public class UtilEditor
    {
        /// <summary>
        /// 获取鼠标选取资源
        /// </summary>
        /// <param name="containType">包含类型</param>
        /// <returns></returns>
        public static List<string> GetSelectedAssets(List<string> containType = null)
        {
            var assets = new List<string>();
            foreach (var obj in Selection.objects)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                if (Directory.Exists(path))
                {
                    var folder = new string[] { path };
                    var guids = AssetDatabase.FindAssets(null, folder);
                    foreach (var guid in guids)
                    {
                        var temp = AssetDatabase.GUIDToAssetPath(guid);
                        if (containType != null)
                        {
                            foreach (var type in containType)
                            {
                                if (temp.ToLower().EndsWith(type.ToLower()) && !Directory.Exists(temp))
                                    assets.Add(AssetDatabase.GUIDToAssetPath(guid));
                            }
                        }
                        else
                        {
                            if (!Directory.Exists(temp))
                                assets.Add(AssetDatabase.GUIDToAssetPath(guid));
                        }
                    }
                }
                else
                {
                    if (containType != null)
                    {
                        foreach (var type in containType)
                        {
                            if (path.ToLower().EndsWith(type.ToLower()))
                                assets.Add(path);
                        }
                    }
                    else
                    { assets.Add(path); }
                }
            }
            return assets;
        }

        /// <summary>
        /// 搜索所有包含的的文件
        /// </summary>
        /// <param name="path">原路径</param>
        /// <param name="fileList">文件列表</param>
        /// <param name="include">包含的后缀名</param>
        public static void SearchAllFiles(string path, List<string> fileList, List<string> include = null)
        {
            string[] subPaths = Directory.GetDirectories(path);
            // 对每一个字目录做与根目录相同的操作
            foreach (string item in subPaths) { SearchAllFiles(item, fileList, include); }
            // 过滤文件
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (include != null)
                {
                    bool isInclude = false;
                    for (int i = 0; i < include.Count; i++)
                    {
                        if (file.EndsWith(include[i]))
                        { isInclude = true; break; }
                    }
                    if (!isInclude) { continue; }
                }
                fileList.Add(file.Replace("\\", "/"));
            }
        }


        static MethodInfo clearMethod = null;
        /// <summary>
        /// 清理控制台
        /// </summary>
        public static void ClearConsole()
        {
            if (clearMethod == null)
            {
                System.Type log = typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries");
                clearMethod = log.GetMethod("Clear");
            }
            clearMethod.Invoke(null, null);
        }
    }
}