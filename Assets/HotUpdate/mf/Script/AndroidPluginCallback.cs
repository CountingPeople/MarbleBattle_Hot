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
                    Debug.Log("callback onSuccess: ����ʼ�����");
                    //Demo.demo.ShowToast("����ʼ�����");
                }
                break;
            case "RemoveView":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: �ͷŹ����Դ");
                    //Demo.demo.ShowToast("�ͷŹ����Դ");
                }
                break;
            case "SplashAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: �������չ�ֳɹ����ر�");
                    //Demo.demo.ShowToast("�������չ�ֳɹ����ر�");
                }
                break;
            case "InteractionAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: �������ر�");
                    //Demo.demo.ShowToast("�������ر�");
                }
                else if(1 == code)
                {
                    Debug.Log("callback onSuccess: �������չ��");
                    //Demo.demo.ShowToast("�������չ��");
                }
                else if (2 == code)
                {
                    Debug.Log("callback onSuccess: ���������");
                    //Demo.demo.ShowToast("���������");
                }
                break;
            case "RewardVideoAd":
                if (0 == code)
                {
                    Debug.Log("callback onSuccess: ������Ƶ���ر�");
                    //Demo.demo.ShowToast("������Ƶ���ر�");
                }
                else if (1 == code)
                {
                    Debug.Log("callback onSuccess: ������Ƶ���չ��");
                    //Demo.demo.ShowToast("������Ƶ���չ��");
                }
                else if (2 == code)
                {
                    Debug.Log("callback onSuccess: ������Ƶ�����");
                    //Demo.demo.ShowToast("������Ƶ�����");
                }
                else if (3 == code)
                {
                    Debug.Log("callback onSuccess: ������Ƶ��潱�����");
                    //Demo.demo.ShowToast("������Ƶ��潱�����");

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
                Debug.Log("callback onError: �����������ʧ��: " + errorMessage);
                //Demo.demo.ShowToast("�����������ʧ��: " + errorMessage);
                break;
            case "InteractionAd":
                Debug.Log("callback onError: �����������ʧ��: " + errorMessage);
                //Demo.demo.ShowToast("�����������ʧ��: " + errorMessage);
                break;
            case "RewardVideoAd":
                Debug.Log("callback onError: ������Ƶ�������ʧ��: " + errorMessage);
                //Demo.demo.ShowToast("������Ƶ�������ʧ��: " + errorMessage);
                break;
        }
    }
}
