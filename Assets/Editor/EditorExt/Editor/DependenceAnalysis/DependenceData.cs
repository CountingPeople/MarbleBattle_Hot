using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace WorkTools
{
    public class DependenceData
    {
        //资产状态
        public enum AssetState { NORMAL, CHANGED, MISSING, NODATA, }
        //资产类
        public class AssetDescription
        {
            public string name = "";
            public string path = "";
            public Hash128 assetDependencyHash;
            public List<string> dependencies = new List<string>();
            public List<string> references = new List<string>();
            public AssetState state = AssetState.NORMAL;
        }

        //缓存路径
        private const string CACHE_PATH = "Library/FinderCache";
        //资源引用信息字典
        public Dictionary<string, AssetDescription> assetDict = new Dictionary<string, AssetDescription>();

        /// <summary>
        /// 收集资源引用信息并更新缓存
        /// </summary>
        public void CollectDependenciesInfo()
        {
            try
            {

                ReadFromCache();    //读取数据
                var allAssets = AssetDatabase.GetAllAssetPaths();
                int totalCount = allAssets.Length;
                for (int i = 0; i < allAssets.Length; i++)
                {
                    //每遍历100个Asset，更新一下进度条，同时对进度条的取消操作进行处理
                    if ((i % 100 == 0) && EditorUtility.DisplayCancelableProgressBar("跟新", string.Format("收集 {0} 资产", i), (float)i / totalCount))
                    {
                        EditorUtility.ClearProgressBar();
                        return;
                    }
                    // if (File.Exists(allAssets[i]))
                    ImportAsset(allAssets[i]);
                    if (i % 2000 == 0)
                        GC.Collect();
                }
                //将信息写入缓存
                EditorUtility.DisplayCancelableProgressBar("跟新", "写入缓存", 1f);
                WriteToChache();
                //生成引用数据
                EditorUtility.DisplayCancelableProgressBar("跟新", "生成资产参考信息", 1f);
                UpdateReferenceInfo();
                ReadFromCache();    //读取数据
                EditorUtility.ClearProgressBar();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                EditorUtility.ClearProgressBar();
            }
        }

        public void Remove()
        {
            if (File.Exists(CACHE_PATH))
                File.Delete(CACHE_PATH);
        }

        //通过依赖信息更新引用信息
        private void UpdateReferenceInfo()
        {
            foreach (var asset in assetDict)
            {
                foreach (var assetGuid in asset.Value.dependencies)
                {
                    if (assetDict[assetGuid].references.Contains(asset.Key))
                        return;
                    assetDict[assetGuid].references.Add(asset.Key);
                }
            }
        }

        //生成并加入引用信息
        private void ImportAsset(string path)
        {
            //通过path获取guid进行储存
            string guid = AssetDatabase.AssetPathToGUID(path);
            //获取该资源的最后修改时间，用于之后的修改判断
            Hash128 assetDependencyHash = AssetDatabase.GetAssetDependencyHash(path);
            //如果assetDict没包含该guid或包含了修改时间不一样则需要更新
            if (!assetDict.ContainsKey(guid) || assetDict[guid].assetDependencyHash != assetDependencyHash)
            {
                //将每个资源的直接依赖资源转化为guid进行储存
                var guids = AssetDatabase.GetDependencies(path, false).
                    Select(p => AssetDatabase.AssetPathToGUID(p)).
                    ToList();

                //生成asset依赖信息，被引用需要在所有的asset依赖信息生成完后才能生成
                AssetDescription ad = new AssetDescription();
                ad.name = Path.GetFileNameWithoutExtension(path);
                ad.path = path;
                ad.assetDependencyHash = assetDependencyHash;
                ad.dependencies = guids;

                if (assetDict.ContainsKey(guid))
                    assetDict[guid] = ad;
                else
                    assetDict.Add(guid, ad);
            }
        }

        //读取缓存信息
        public bool ReadFromCache()
        {
            assetDict.Clear();
            if (File.Exists(CACHE_PATH))
            {
                var serializedGuid = new List<string>();
                var serializedDependencyHash = new List<Hash128>();
                var serializedDenpendencies = new List<int[]>();
                //反序列化数据
                using (FileStream fs = File.OpenRead(CACHE_PATH))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    EditorUtility.DisplayCancelableProgressBar("Import Cache", "Reading Cache", 0);
                    serializedGuid = (List<string>)bf.Deserialize(fs);
                    serializedDependencyHash = (List<Hash128>)bf.Deserialize(fs);
                    serializedDenpendencies = (List<int[]>)bf.Deserialize(fs);
                    EditorUtility.ClearProgressBar();
                }

                for (int i = 0; i < serializedGuid.Count; ++i)
                {
                    string path = AssetDatabase.GUIDToAssetPath(serializedGuid[i]);
                    if (!string.IsNullOrEmpty(path))
                    {
                        var ad = new AssetDescription();
                        ad.name = Path.GetFileNameWithoutExtension(path);
                        ad.path = path;
                        ad.assetDependencyHash = serializedDependencyHash[i];
                        assetDict.Add(serializedGuid[i], ad);
                    }
                }

                for (int i = 0; i < serializedGuid.Count; ++i)
                {
                    string guid = serializedGuid[i];
                    if (assetDict.ContainsKey(guid))
                    {
                        var guids = serializedDenpendencies[i].
                            Select(index => serializedGuid[index]).
                            Where(g => assetDict.ContainsKey(g)).
                            ToList();
                        assetDict[guid].dependencies = guids;
                    }
                }
                UpdateReferenceInfo();
                return true;
            }
            return false;
        }

        //写入缓存
        private void WriteToChache()
        {
            if (File.Exists(CACHE_PATH))
                File.Delete(CACHE_PATH);

            var serializedGuid = new List<string>();
            var serializedDependencyHash = new List<Hash128>();
            var serializedDenpendencies = new List<int[]>();
            //辅助映射字典
            var guidIndex = new Dictionary<string, int>();
            //序列化
            using (FileStream fs = File.OpenWrite(CACHE_PATH))
            {
                foreach (var pair in assetDict)
                {
                    guidIndex.Add(pair.Key, guidIndex.Count);
                    serializedGuid.Add(pair.Key);
                    serializedDependencyHash.Add(pair.Value.assetDependencyHash);
                }

                foreach (var guid in serializedGuid)
                {
                    int[] indexes = assetDict[guid].dependencies.Select(s => guidIndex[s]).ToArray();
                    serializedDenpendencies.Add(indexes);
                }
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, serializedGuid);
                bf.Serialize(fs, serializedDependencyHash);
                bf.Serialize(fs, serializedDenpendencies);
            }
        }

        //更新引用信息状态
        public void UpdateAssetState(string guid)
        {
            AssetDescription ad;
            if (assetDict.TryGetValue(guid, out ad) && ad.state != AssetState.NODATA)
            {
                if (File.Exists(ad.path))
                {
                    //修改时间与记录的不同为修改过的资源
                    if (ad.assetDependencyHash != AssetDatabase.GetAssetDependencyHash(ad.path))
                    {
                        ad.state = AssetState.CHANGED;
                    }
                    else
                    {
                        //默认为普通资源
                        ad.state = AssetState.NORMAL;
                    }
                }
                //不存在为丢失
                else
                {
                    ad.state = AssetState.MISSING;
                }
            }

            //字典中没有该数据
            else if (!assetDict.TryGetValue(guid, out ad))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ad = new AssetDescription();
                ad.name = Path.GetFileNameWithoutExtension(path);
                ad.path = path;
                ad.state = AssetState.NODATA;
                assetDict.Add(guid, ad);
            }
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns>根据引用信息状态获取状态描述</returns>
        public static string GetInfoByState(AssetState state)
        {
            if (state == AssetState.CHANGED)
            {
                return "<color=#FFCC00>更改</color>";
            }
            else if (state == AssetState.MISSING)
            {
                return "<color=#F74C31>丢失</color>";
            }
            else if (state == AssetState.NODATA)
            {
                return "<color=#F74C31>null</color>";
            }
            return "<color=#5e8a35>正常</color>";
        }
    }
}