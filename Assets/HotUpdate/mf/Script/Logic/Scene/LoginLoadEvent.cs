using BM;
using ET;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginLoadEvent : LoadEventBase
{
    private bool isCanContinue = true;
    public override bool IsCanContinue()
    {
        return isCanContinue;
    }

    private async ETTask LoadNewScene()
    {
        LoadSceneHandler loadSceneHandler = await AssetComponent.LoadSceneAsync("Assets/Bundles/Scence/Start.unity");
        //如果需要获取场景加载进度, 用这种加载方式 loadSceneHandler2.GetProgress() , 注意进度不是线性的
        // ETTask loadSceneHandlerTask = AssetComponent.LoadSceneAsync(out LoadSceneHandler loadSceneHandler2, "Assets/Scenes/Game.unity");
        // await loadSceneHandlerTask;

        AsyncOperation operation = SceneManager.LoadSceneAsync("Start");
        operation.completed += asyncOperation =>
        {
            isCanContinue = true;
        };
    }

    public override void OnProgress(float value)
    {
        base.OnProgress(value);

        //Debug.Log($"qs {value}");

        if (value == 10)
        {
            isCanContinue = false;
            //GameApp.Instance.StartCoroutine(LoadScene());
            LoadNewScene().Coroutine();
            
        }

        if (value == 40)
        {
            isCanContinue = false;
            //Camera.main.transform.localPosition = new Vector3(120.8f, 52.4f, 17);
            //Camera.main.transform.localEulerAngles = new Vector3(13, 180, 0);
            //PlayerLogic.Instance.GetRole().ResetProp();

            //var tran = GameObject.Find("tran_role").transform;
            //var obj = UIUtil.Instance.LoadObj($"Assets/{GameContant.LocalBundles}/Role/Knight Male 05.prefab", tran, new Vector3(0, 0, 0));

            //PlayerLogic.Instance.SetModel(obj);
            //PlayerLogic.Instance.ChangeModel();
            isCanContinue = true;
        }
    }

    public override void OnComplete()
    {
        base.OnComplete();
        FSMModule.Instance.TransitionState<LoginState>();
    }
}
