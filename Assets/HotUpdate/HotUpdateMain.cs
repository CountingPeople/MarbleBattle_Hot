using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotUpdateMain : MonoBehaviour
{

    public string text;

    void Start()
    {
        Debug.Log("这个热更新脚本挂载在prefab上，打包成ab。通过从ab中实例化prefab成功还原");
        Debug.LogFormat("hello, {0}.", text);

        gameObject.AddComponent<CreateByCode>();

        Debug.Log("=======看到此条日志代表你成功运行了示例项目的热更新代码=======");

        gameObject.AddComponent<GameLaunch>();
    }
}
