using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static volatile T instance;
    private static object syncRoot = new Object();
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        T[] instances = FindObjectsOfType<T>();
                        //if (instances != null)
                        //{
                        //    for (var i = 0; i < instances.Length; i++)
                        //    {
                        //        Destroy(instances[i].gameObject);
                        //    }
                        //}
                        GameObject go = new GameObject();
                        go.name = typeof(T).Name;
                        instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return instance;
        }
    }

    public virtual void Init()
    { 
    }
}

public abstract class GetInstance<T> where T : class, new()
{
    protected static T _instance = null;
    public static T Instance
    {
        get{
            if (null == _instance)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
    protected GetInstance()
    {
        if (null != _instance)
        {
            Debug.Log("_instance不是null !!!");
            Init();
        }
    }
    public virtual void Init()
    {

    }
}
