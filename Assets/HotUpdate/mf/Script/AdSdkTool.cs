using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSdkTool : GetInstance<AdSdkTool>
{

    private AndroidJavaClass jc;
    private AndroidJavaObject jo;


    public void InitAd()
    {
#if !UNITY_EDITOR
        Debug.Log("Awake()");
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("InitAd", "1660866543862431843-1", new AndroidPluginCallback());
#endif
    }


    public void RemoveView()
    {
        jo.Call("RemoveView", "all", new AndroidPluginCallback());//�ͷ����й����Դ
        //jo.Call("RemoveView", "InteractionAd", new AndroidPluginCallback());//ֻ�ͷŲ��������Դ
        //jo.Call("RemoveView", "BannerAd", new AndroidPluginCallback());//ֻ�ͷ�Banner�����Դ
        //jo.Call("RemoveView", "RewardVideoAd", new AndroidPluginCallback());//ֻ�ͷż�����Ƶ�����Դ
    }


    //public void LoadSplashAd()
    //{
    //    jo.Call("LoadSplashAd", "����ƽ̨������Ŀ������ID", new AndroidPluginCallback());
    //}

    //public void LoadInteractionAd()
    //{
    //    jo.Call("LoadInteractionAd", "����ƽ̨������Ĳ������ID", new AndroidPluginCallback());
    //}

    private string[] adIdArr = { "1660868353629110351", "1660868217310035996", "1660866927167291485" };

    //
    public void LoadPlayRewardVideoAd(AdEnum adEnum, Action callBack)
    {
        string guid = Guid.NewGuid().ToString().Substring(0, 25);//SystemInfo.deviceUniqueIdentifier;
        string saveGuid = PlayerPrefs.GetString("GameRoleGuid");
        if (string.IsNullOrEmpty(saveGuid))
        {
            PlayerPrefs.SetString("GameRoleGuid", guid);
        }
        else {
            guid = saveGuid;
        }

#if UNITY_EDITOR
        if (callBack != null)
        {
            callBack();
        }
#else
        jo.Call("LoadPlayRewardVideoAd", adIdArr[(int)adEnum], guid, "true", new AndroidPluginCallback(callBack));
#endif
    }
}
