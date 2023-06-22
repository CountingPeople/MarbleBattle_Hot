using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidPluginCallback : AndroidJavaProxy
{
    private Action tempCall;
    public AndroidPluginCallback(Action callBack=null) : base("com.qubian.ad_sdk_unity.PluginCallback") {
        tempCall = callBack;
    }

    public void onSuccess(string type, int code)
    {
        Debug.Log("ENTER callback onSuccess: " + type + ": " + code);
        switch(type)
        {
            case "InitAd":
                if(0 == code)
                {
                    Debug.Log("callback onSuccess: 广告初始化完成");
                    //Demo.demo.ShowToast("广告初始化完成");
                }
                break;
            case "RemoveView":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: 释放广告资源");
                    //Demo.demo.ShowToast("释放广告资源");
                }
                break;
            case "SplashAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: 开屏广告展现成功并关闭");
                    //Demo.demo.ShowToast("开屏广告展现成功并关闭");
                }
                break;
            case "InteractionAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: 插屏广告关闭");
                    //Demo.demo.ShowToast("插屏广告关闭");
                }
                else if(1 == code)
                {
                    Debug.Log("callback onSuccess: 插屏广告展现");
                    //Demo.demo.ShowToast("插屏广告展现");
                }
                else if (2 == code)
                {
                    Debug.Log("callback onSuccess: 插屏广告点击");
                    //Demo.demo.ShowToast("插屏广告点击");
                }
                break;
            case "RewardVideoAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: 激励视频广告关闭");
                    //Demo.demo.ShowToast("激励视频广告关闭");
                }
                else if (1 == code)
                {
                    Debug.Log("callback onSuccess: 激励视频广告展现");
                    //Demo.demo.ShowToast("激励视频广告展现");
                }
                else if (2 == code)
                {
                    Debug.Log("callback onSuccess: 激励视频广告点击");
                    //Demo.demo.ShowToast("激励视频广告点击");
                }
                else if (3 == code)
                {
                    Debug.Log("callback onSuccess: 激励视频广告奖励达成");
                    //Demo.demo.ShowToast("激励视频广告奖励达成");

                    if (tempCall != null)
                    {
                        tempCall();
                    }

                }
                break;
        }
    }
    public void onError(string type, string errorMessage)
    {
        Debug.Log("ENTER callback onError: " + type + ": " + errorMessage);
        switch (type)
        {
            case "SplashAd":
                Debug.Log("callback onError: 开屏广告请求失败: " + errorMessage);
                //Demo.demo.ShowToast("开屏广告请求失败: " + errorMessage);
                break;
            case "InteractionAd":
                Debug.Log("callback onError: 插屏广告请求失败: " + errorMessage);
                //Demo.demo.ShowToast("插屏广告请求失败: " + errorMessage);
                break;
            case "RewardVideoAd":
                Debug.Log("callback onError: 激励视频广告请求失败: " + errorMessage);
                //Demo.demo.ShowToast("激励视频广告请求失败: " + errorMessage);
                break;
        }
    }
}
