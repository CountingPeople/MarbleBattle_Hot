using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IDbservice { }
    public abstract class BaseDbService<T> : IDbservice where T : BaseDbService<T>, new()
    {
        private static T _instance = null;

        public static T Instance => _instance;


        public GameSaveScpObjTool _connection;

        public BaseDbService()
        {
            _instance = this as T;
            if (_instance == null)
            {
                Debug.LogError($"BaseDbService:{this.GetType().Name},=== T Type:{typeof(T).Name}");
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init(GameSaveScpObjTool connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Freed()
        {

        }

        private ILogger _logger = null;
        protected ILogger Log
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogModule.Instance.GetLogger(_instance.GetType().Name);
                }
                return _logger;
            }
        }



    }
}
