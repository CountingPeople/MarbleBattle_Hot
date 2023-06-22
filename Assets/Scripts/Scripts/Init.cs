using System.Collections.Generic;
using UnityEngine;
using ET;
using BM;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using System.Linq;
using HybridCLR;
public class Init : MonoBehaviour
{
    private Transform _uiManagerTf;
    private UpdateBundleDataInfo _updateBundleDataInfo;
    
    private void Awake()
    {
        System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            AssetLogHelper.LogError(e.ExceptionObject.ToString());
        };
        ETTask.ExceptionHandler += AssetLogHelper.LogError;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Initialization();
    }
    
    void Update()
    {
        AssetComponent.Update();
    }
    
    void OnLowMemory()
    {
        AssetComponent.ForceUnLoadAll();
    }
    
    void OnDestroy()
    {
        _updateBundleDataInfo?.CancelUpdate();
        LMTD.ThreadFactory.Destroy();
    }

    private void Initialization()
    {
        //重新配置热更路径(开发方便用, 打包移动端需要注释注释)
#if UNITY_EDITOR_WIN
        AssetComponentConfig.HotfixPath = Application.dataPath + "/../HotfixBundles/";
#endif
        _uiManagerTf = gameObject.transform.Find("UIManager");
        AssetComponentConfig.DefaultBundlePackageName = "AllBundle";
        //创建下载UI
        GameObject downLoadUI = GameObject.Instantiate(Resources.Load<GameObject>("DownLoadUI"), _uiManagerTf);
        DownLoad(downLoadUI).Coroutine();
        
    }
    
    /// <summary>
    /// 下载资源
    /// </summary>
    private async ETTask DownLoad(GameObject downLoadUI)
    {
        downLoadUI.SetActive(false);
        Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
        {
            {AssetComponentConfig.DefaultBundlePackageName, false},
            {"LocalBundles", false},
            //{"Main", false},
            //{"APK", false},
        };
        _updateBundleDataInfo = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);
        if (!_updateBundleDataInfo.NeedUpdate)
        {
            GameObject.Destroy(downLoadUI);
            InitializePackage().Coroutine();
            return;
        }
        downLoadUI.SetActive(true);
        Debug.LogError("需要更新, 大小: " + _updateBundleDataInfo.NeedUpdateSize);
        Slider progressSlider = downLoadUI.transform.Find("ProgressSlider").GetComponent<Slider>();
        Text progressText = downLoadUI.transform.Find("ProgressValue/Text").GetComponent<Text>();
        Text speedText = downLoadUI.transform.Find("SpeedValue/Text").GetComponent<Text>();
        Button cancelDownLoad = downLoadUI.transform.Find("Cancel").GetComponent<Button>();
        Button reDownLoad = downLoadUI.transform.Find("ReDown").GetComponent<Button>();
        _updateBundleDataInfo.DownLoadFinishCallback += () =>
        {
            GameObject.Destroy(downLoadUI);
            InitializePackage().Coroutine();
        };
        _updateBundleDataInfo.ProgressCallback += p =>
        {
            progressSlider.value = p / 100.0f;
            progressText.text = p.ToString("#0.00") + "%";
        };
        _updateBundleDataInfo.DownLoadSpeedCallback += s =>
        {
            speedText.text = (s / 1024.0f).ToString("#0.00") + " kb/s";
        };
        _updateBundleDataInfo.ErrorCancelCallback += () =>
        {
            Debug.LogError("下载取消");
        };
        cancelDownLoad.onClick.RemoveAllListeners();
        cancelDownLoad.onClick.AddListener(_updateBundleDataInfo.CancelUpdate);
        reDownLoad.onClick.RemoveAllListeners();
        reDownLoad.onClick.AddListener(() =>
        {
            DownLoad(downLoadUI).Coroutine();
        });
        AssetComponent.DownLoadUpdate(_updateBundleDataInfo).Coroutine();
    }
    
    private async ETTask InitializePackage()
    {
        await AssetComponent.Initialize(AssetComponentConfig.DefaultBundlePackageName);
        await AssetComponent.Initialize("LocalBundles");
        await InitUI();
        //await InitCode();
    }

    private async ETTask InitUI()
    {
        ////加载图集
        //await AssetComponent.LoadAsync(out LoadHandler atlasHandler2, BPath.Assets_Bundles_Atlas_UISpriteAtlas__spriteatlas);
        ////异步加载资源
        //GameObject loginUIAsset = await AssetComponent.LoadAsync<GameObject>(out LoadHandler loginUIHandler, BPath.Assets_Bundles_LoginUI__prefab);
        //GameObject loginUIObj = UnityEngine.Object.Instantiate(loginUIAsset, _uiManagerTf, false);

        //// GameObject subUI = await AssetComponent.LoadAsync<GameObject>(out LoadHandler usbUIHandler, "Assets/Bundles/SubBundleAssets/SubUI_Copy.prefab");
        //// GameObject subUIObj = UnityEngine.Object.Instantiate(subUI, loginUIObj.transform, false);

        //loginUIObj.transform.Find("Login").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    //卸载资源
        //    GameObject.Destroy(loginUIObj);
        //    loginUIHandler.UnLoad();
        //    atlasHandler2.UnLoad();
        //    LoadNewScene().Coroutine();
        //});

        LoadNewScene().Coroutine();
    }

    private async ETTask LoadNewScene()
    {

        LoadSceneHandler loadSceneHandler = await AssetComponent.LoadSceneAsync("Assets/Bundles/Scence/lanuch.unity");
        //如果需要获取场景加载进度, 用这种加载方式 loadSceneHandler2.GetProgress() , 注意进度不是线性的
        // ETTask loadSceneHandlerTask = AssetComponent.LoadSceneAsync(out LoadSceneHandler loadSceneHandler2, "Assets/Scenes/Game.unity");
        // await loadSceneHandlerTask;
        AsyncOperation operation = SceneManager.LoadSceneAsync("lanuch");
        operation.completed += asyncOperation =>
        {
            //同步加载资源(加载分包内的资源)
            //GameObject gameObjectAsset = AssetComponent.Load<GameObject>(BPath.Assets_Bundles_SubBundleAssets_mister91jiao__prefab, "SubBundle");
            
            // BundleRuntimeInfo bundleRuntimeInfo = AssetComponent.GetBundleRuntimeInfo("SubBundle");
            // GameObject gameObjectAsset = bundleRuntimeInfo.Load<GameObject>(BPath.Assets_Bundles_SubBundleAssets_mister91jiao__prefab);
            // GameObject obj = UnityEngine.Object.Instantiate(gameObjectAsset);
            
            // GameObject gameObjectAsset1 = AssetComponent.Load<GameObject>(BPath.Assets_Bundles_SubBundleAssets_mister91jiao__prefab);
            // GameObject obj1 = UnityEngine.Object.Instantiate(gameObjectAsset1);

            //AssetComponent.LoadAsync<GameObject>(out LoadHandler handler, BPath.Assets_Bundles_SubBundleAssets_mister91jiao__prefab, "SubBundle").Coroutine();
            //handler.Completed += loadHandler =>
            //{
            //    UnityEngine.Object.Instantiate(loadHandler.Asset);
            //    ResetUI().Coroutine();
            //};

            //LoadGroupTest().Coroutine();
        };
    } 

    //private async ETTask LoadGroupTest()
    //{
    //    Texture zfnp = await AssetComponent.LoadAsync<Texture>(out LoadHandler handler, BPath.Assets_Bundles_GroupBundle_bds__png);
    //    //Debug.LogError(zfnp.height);
    //    handler.UnLoad();
    //}
    
    //private async ETTask ResetUI()
    //{
    //    //异步加载资源
    //    UnityEngine.Object resetUIAsset = await AssetComponent.LoadAsync(BPath.Assets_Bundles_ResetUI__prefab);
    //    GameObject resetUIObj = UnityEngine.Object.Instantiate(resetUIAsset as GameObject, _uiManagerTf, false);
    //    resetUIObj.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() =>
    //    {
    //        GameObject.Destroy(resetUIObj);
    //        AssetComponent.UnInitializeAll();
            
    //        AsyncOperation operation = SceneManager.LoadSceneAsync("Init_2");
    //        operation.completed += asyncOperation =>
    //        {
    //            //重新加载资源
    //            Initialization();
    //        };
    //    });
        
    //    resetUIObj.transform.Find("Install").GetComponent<Button>().onClick.AddListener(() =>
    //    {
    //        InstallHelper.InstallApk().Coroutine();
    //    });
    //}
    
}
