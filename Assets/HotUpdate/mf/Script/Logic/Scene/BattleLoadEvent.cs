using BM;
using ET;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoadEvent : LoadEventBase
{
    private bool isCanContinue=true;
    public override bool IsCanContinue()
    {
        return isCanContinue;
    }

    private async ETTask LoadNewScene()
    {
        LoadSceneHandler loadSceneHandler = await AssetComponent.LoadSceneAsync("Assets/Bundles/Scence/battle.unity");
        //如果需要获取场景加载进度, 用这种加载方式 loadSceneHandler2.GetProgress() , 注意进度不是线性的
        // ETTask loadSceneHandlerTask = AssetComponent.LoadSceneAsync(out LoadSceneHandler loadSceneHandler2, "Assets/Scenes/Game.unity");
        // await loadSceneHandlerTask;
        AsyncOperation operation = SceneManager.LoadSceneAsync("battle");
        operation.completed += asyncOperation =>
        {
            isCanContinue = true;
        };
    }


    public override void OnProgress(float value)
    {
        base.OnProgress(value);



        if (value == 8)
        {
            isCanContinue = false;            //GameApp.Instance.StartCoroutine(LoadScene());
            LoadNewScene().Coroutine();
        }

        if (value == 95)
        {
            //BattleSystem.Instance.StartGame();
        }

    }

    public override void OnComplete()
    {
        base.OnComplete();
        FSMModule.Instance.TransitionState<BattleState>();
    }
}
