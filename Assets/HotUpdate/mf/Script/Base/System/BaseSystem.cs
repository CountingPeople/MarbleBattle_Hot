using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSystem<T> : ISystem where T : BaseSystem<T>, new()
{

    private static T _instance = null;

    public static T Instance => _instance;

    private GameObject _gameObject;
    public GameObject gameObject => _gameObject;

    public Transform transform => _gameObject?.transform;

    private Dictionary<int, Coroutine> _coroutineDic;

    public BaseSystem()
    {
        _coroutineDic = new Dictionary<int, Coroutine>();
        _gameObject = new UnityEngine.GameObject(this.GetType().Name);
        Debug.Log(this.GetType().Name);
        _gameObject.transform.SetParent(GameApp.Instance.transform);
        _instance = this as T;
        if (_instance == null)
        {
            Debug.LogError($"ModuleType:{this.GetType().Name},=== T Type:{typeof(T).Name}");
        }
    }
    public virtual void InitGame()
    {

    }

    public virtual void EnterGame()
    {
       
    }

    public virtual void UpdateGame()
    {

    }

    public virtual void LateUpdateGame()
    {

    }


    public virtual void LeaveGame()
    {
       
    }

    /// <summary>
    /// 启动协程
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    protected Coroutine StartCoroutine(IEnumerator routine)
    {
        Coroutine coroutine = GameApp.Instance.StartCoroutine(routine);
        _coroutineDic.Add(coroutine.GetHashCode(), coroutine);
        return coroutine;
    }
    /// <summary>
    /// 停止协程
    /// </summary>
    /// <param name="coroutine"></param>
    public void StopCoroutine(Coroutine coroutine)
    {
        GameApp.Instance.StopCoroutine(coroutine);
        int hashCode = coroutine.GetHashCode();
        if (_coroutineDic.ContainsKey(hashCode))
        {
            _coroutineDic.Remove(hashCode);
        }
    }

    /// <summary>
    /// 停止协程
    /// </summary>
    public void StopAllCoroutines()
    {
        foreach (var item in _coroutineDic)
        {
            GameApp.Instance.StopCoroutine(item.Value);
        }
        _coroutineDic.Clear();
    }
}
