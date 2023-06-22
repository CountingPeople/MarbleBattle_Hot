using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    [CreateAssetMenu(menuName = "--- 美术工具 ---/新建预览信息", order = 1000)]
    public class ResObject : ScriptableObject
    {
        [SerializeField] public string lastPath;
        [SerializeField] public string path;
        [SerializeField] public List<ResData> Assets = new List<ResData>();


        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddData(ResData data)
        {
            if (Assets.Count > 0)
            {
                bool is_add = true;
                foreach (var asset in Assets)
                {
                    if (asset.path == data.path) { is_add = false; }
                }
                if (is_add)
                {
                    Assets.Add(data);
                    AssetDatabase.AddObjectToAsset(data, this);
                }
            }
            else
            {
                Assets.Add(data);
                AssetDatabase.AddObjectToAsset(data, this);
            }
        }

        /// <summary>
        /// 清理空数据
        /// </summary>
        public void ClearNull()
        {
            if (path != lastPath)
            {
                Assets.Clear();
            }
            var deletelist = new List<ResData>();
            foreach (var data in Assets)
            {
                if (data.obj == null) { deletelist.Add(data); }
            }
            foreach (var delete in deletelist)
            {
                Assets.Remove(delete);
            }
            lastPath = path;
        }
    }
}