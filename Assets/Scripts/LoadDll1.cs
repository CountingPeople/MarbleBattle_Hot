using BM;
using ET;
using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDll1 : MonoBehaviour
{


    public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
    {
        "mscorlib.dll",
        "System.dll",
        "System.Core.dll",
        "BundleMaster.dll",
        //"HotCode.dll",
    };

    void Start()
    {
        InitializePackage().Coroutine();
    }

    private async ETTask InitializePackage()
    {
        await InitCode();
    }

    private static Dictionary<string, byte[]> s_assetDatas = new Dictionary<string, byte[]>();

    public static byte[] GetAssetData(string dllName)
    {
        return s_assetDatas[dllName];
    }

    private async ETTask InitCode()
    {
        var assets = new List<string>
        {
            "Assembly-CSharp.dll",
        }.Concat(AOTMetaAssemblyNames);

        foreach (var asset in assets)
        {
            TextAsset assetData = await AssetComponent.LoadAsync<TextAsset>(out LoadHandler loginUIHandler, "Assets/Bundles/Dll/" + asset + ".bytes");
            s_assetDatas[asset] = assetData.bytes;
            Debug.Log($"加载程序集==============={asset}");
        }

        LoadMetadataForAOTAssemblies();

#if !UNITY_EDITOR
        System.Reflection.Assembly.Load(GetAssetData("Assembly-CSharp.dll"));
#endif
        //AssetBundle prefabAb = AssetBundle.LoadFromMemory(GetAssetData("prefabs"));
        //GameObject testPrefab = Instantiate(prefabAb.LoadAsset<GameObject>("HotUpdatePrefab.prefab"));

        //GameObject loginUIAsset = await AssetComponent.LoadAsync<GameObject>(out LoadHandler loginUIHandler2, "Assets/Bundles/Res/Prefab/HotUpdatePrefab.prefab");
        //GameObject loginUIObj = UnityEngine.Object.Instantiate(loginUIAsset);


        GameObject loginUIAsset = await AssetComponent.LoadAsync<GameObject>(out LoadHandler loginUIHandler2, "Assets/Bundles/Prefab/Lanuch.prefab");
        GameObject loginUIObj = UnityEngine.Object.Instantiate(loginUIAsset);

    }


    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    private static void LoadMetadataForAOTAssemblies()
    {
        /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        /// 
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (var aotDllName in AOTMetaAssemblyNames)
        {
            byte[] dllBytes = GetAssetData(aotDllName);
            // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
        }
    }
}
