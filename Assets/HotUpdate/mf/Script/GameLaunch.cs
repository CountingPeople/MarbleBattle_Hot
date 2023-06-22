using Framework;
using UnityEngine;
using UnityEngine.UI;


internal sealed class GameLaunch : MonoBehaviour
{
    [SerializeField]
    //private bool IsWan = true;
    //[SerializeField]
    //private bool ShowFps = true;



    private void Start()
    {
        //        if (ShowFps)
        //        {
        //            GameObject obj = new GameObject("GameFPS");

        //            obj.AddComponent<GameFPS>();
        //            DontDestroyOnLoad(obj);
        //        }
        //#if UNITY_EDITOR
        //        //GameObject obj1 = new GameObject("GamePool");

        //        //obj1.AddComponent<ShowObjectPool>();
        //        //DontDestroyOnLoad(obj1);
        //#endif
        GameApp.Instance
            .SetFrameRate(60)
            .SetDeltaTimeType(GameApp.DeltaTimeType.RealDeltaTime)
            .UseGC(true)
            .SetGCTime(90)
            .Init();


        AppContext.InitAppContext();

#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#endif
        PerfabTool.Instance.Init();
        //UIModule.Instance.ShowPanel<LoginPanelView>();
        //AdSdkTool.Instance.InitAd();

        Debug.Log("开始初始化系统");
        SystemModule.Instance.InitGame();
        SystemModule.Instance.EnterGame();
        Debug.Log("开始切换场景");
        var state = FSMModule.Instance.GetState<LoadState>();
        state.SetNextEvent<LoginLoadEvent>();
        FSMModule.Instance.TransitionState<LoadState>();

    }

    private void Update()
    {

    }
}
