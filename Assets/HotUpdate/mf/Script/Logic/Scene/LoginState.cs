using DG.Tweening;
using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginState : StateBase
{
    public override void OnEnter()
    {
        base.OnEnter();


        GameApp.Instance.StartCoroutine(Create());
 

    }

    IEnumerator Create()
    {

        UIModule.Instance.CloseAllPanel();
        yield return new WaitForEndOfFrame();


        //UIModule.Instance.ShowPanel<HomeSceneView>();

        //AudioModule.Instance.PlayAudio("Assets/LocalBundles/Sound/BGM04town2.wav", true,true);

    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
