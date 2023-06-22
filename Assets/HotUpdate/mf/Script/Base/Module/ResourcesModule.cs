using BM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    public sealed class ResourcesModule : BaseModule<ResourcesModule>
    {
        private Dictionary<string, ResourcesDto> _resourcesCacheDic;

        private List<string> _destroyTempList;
        /// <summary>
        /// 每次检测释放没用资源的时间（秒）
        /// </summary>
        public int UnloadTime { get; set; }
        /// <summary>
        /// 释放资源临时计时变量
        /// </summary>
        private float _unloadTempTime = 0;
        /// <summary>
        /// 删除Resources的时间（秒）
        /// </summary>
        public int DestroyTime { get; set; }
        public override void Init()
        {
            _resourcesCacheDic = new Dictionary<string, ResourcesDto>();
            _destroyTempList = new List<string>();
            UnloadTime = 180;
            DestroyTime = 180;
        }
        /// <summary>
        /// 读取并创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadOrCreate<T>(string path, bool autoRelease = true) where T : Object
        {
            T resObj = Load<T>(path, autoRelease);
            if (resObj != null)
            {
                T obj = Object.Instantiate(resObj);
                resObj = null;
                return obj;
            }
            return null;
        }
        /// <summary>
        /// 读取并创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public Object LoadOrCreate(string path, bool autoRelease = true)
        {
            Object resObj = Load(path, autoRelease);
            if (resObj != null)
            {
                Object obj = Object.Instantiate(resObj);
                resObj = null;
                return obj;
            }
            return null;
        }
        /// <summary>
        /// 读取不创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Load<T>(string path, bool autoRelease = true, string bundleName = "AllBundle") where T : Object
        {
#if UNITY_EDITOR
      
            return AssetDatabase.LoadAssetAtPath<T>(path);
#else
            return AssetComponent.Load<T>(path, bundleName);
#endif
            return null;
        }

        /// <summary>
        /// 读取不创建
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Object Load(string path, bool autoRelease = true)
        {
            return  AssetComponent.Load(path, "AllBundle");
            //return Resources.Load(path);
        }
        public override void Update(float deltaTime)
        {
            if (_unloadTempTime > UnloadTime)
            {
                //间隔一段时间调用
                Resources.UnloadUnusedAssets();
                _unloadTempTime = 0;
            }
            _unloadTempTime += deltaTime;
        }

        public override void Freed()
        {
            foreach (KeyValuePair<string, ResourcesDto> item in _resourcesCacheDic)
            {
                ResourcesDto resourcesDto = item.Value;
                Object.Destroy(resourcesDto.ResourceObj);
                resourcesDto.ResourceObj = null;
            }
            _resourcesCacheDic.Clear();
            _destroyTempList.Clear();
            Resources.UnloadUnusedAssets();
            Object.Destroy(this.gameObject);
        }

        private class ResourcesDto
        {
            public Object ResourceObj = null;
            public float LastUseTime = 0;
            public bool AutoRelease = true;

            public bool IsCanDestroy => AutoRelease ? false : (GameApp.Instance.gameTime - this.LastUseTime) > Instance.DestroyTime;
        }
    }
}
