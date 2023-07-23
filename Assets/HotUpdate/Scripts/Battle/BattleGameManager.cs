using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all base Entity
public abstract class IBattleEntity : MonoBehaviour
{
    // use for identify enemy or player
    public enum ECamp
    {
        Unknow,
        Player,
        Enemy
    }

    private ECamp mCamp = ECamp.Unknow;
    public ECamp Camp
    {
        get {
            Debug.Assert(mCamp != ECamp.Unknow);
            return mCamp; 
        }
        set { mCamp = value; }
    }

    public virtual float GetHit() { return 0; }

    public abstract void Tick();

    protected virtual void Start()
    {
        BattleGameManager.RegisterBattleEntity(this);
    }

    protected virtual void OnDestroy()
    {
        BattleGameManager.UnregisterBattleEntity(this);
    }
}

public abstract class MissileEntity : IBattleEntity
{
    public GameObject DeadEffect = null;
}

public enum BattleGameState
{
    Gaming,
    Pause,
    Finish
}

public static class BattleGameManager
{
    private static List<IBattleEntity> mTickItem = null;
    private static bool mNeedDestory = false;

    // Game State
    private static BattleGameState mGameState;
    public static BattleGameState GameState
    {
        private set { mGameState = value; }
        get { return mGameState; }
    }

    // Player
    private static Battle.Player mPlayer = null;
    public static Battle.Player Player
    {
        get { return mPlayer; }
    }

    // Init
    public static void Init(Transform enemyParent)
    {
        mTickItem = new List<IBattleEntity>();
        mNeedDestory = false;

        CameraManager.Init();
        MonsterManager.Init(enemyParent);

        mPlayer = new Battle.Player();

        BattleEventManager.OnGroundAttacked.AddListener(OnGroundAttacked);
        MarbleEventManager.OnHPBrickDestory.AddListener(OnHPBrickDestory);
        GlobalEventManager.OnLevelFinished.AddListener(OnLevelFinished);
        BattleEventManager.OnPlayerDead.AddListener(OnPlayerDead);

        GameState = BattleGameState.Pause;
    }

    // Destory
    public static void Destory()
    {
        mNeedDestory = true;

        if(mTickItem.Count == 0)
            mTickItem = null;

        CameraManager.Destory();
        MonsterManager.Destory();
    }

    public static void Tick(float deltaTime)
    {
        if (GameState != BattleGameState.Gaming)
            return;

        MonsterManager.Instance.Tick(deltaTime);

        // tick every entity
        for(int i = 0; i < mTickItem.Count; ++i)
        {
            mTickItem[i].Tick();
        }

    }

    public static void LateTick()
    {
        PlayerCarnon.ChangeCarnon();
    }

    public static void RegisterBattleEntity(IBattleEntity entity)
    {
#if UNITY_EDITOR
        Debug.Assert(entity != null, "Battle Entity is null");
#endif

        mTickItem.Add(entity);
    }

    public static void UnregisterBattleEntity(IBattleEntity entity)
    {
        mTickItem.Remove(entity);

        if (mNeedDestory && mTickItem.Count == 0)
            mTickItem = null;

    }

    static void OnGroundAttacked(IBattleEntity attacker)
    {
        Player.HP -= attacker.GetHit();
    }

    static void OnHPBrickDestory(float hpRecover)
    {
        Player.HP += hpRecover;
    }

    static void OnPlayerDead()
    {
        GameState = BattleGameState.Finish;

        GlobalEventManager.OnGameOver.Invoke();
    }

    static void OnLevelFinished()
    {
        GameState = BattleGameState.Finish;
    }

    // Game State Control
    public static void Pause()
    {
        GameState = BattleGameState.Pause;
    }

    public static void Start()
    {
        GameState = BattleGameState.Gaming;
    }
    
}

// TODO: refactor this
class CameraManager
{
    private Camera mBattleCamera;
    public Camera BattleCamera
    {
        get { return mBattleCamera; }
        set { mBattleCamera = value; }
    }
    const string mBattleCameraName = "BattleCamera";

    static public CameraManager Instance = null;

    CameraManager()
    {
        var cameras = Camera.allCameras;
        foreach(var curCam in cameras)
        {
            if (curCam.name == mBattleCameraName)
            {
                mBattleCamera = curCam;
                break;
            }
        }

        //Check
#if UNITY_EDITOR
        Debug.Assert(mBattleCamera != null);
#endif
    }

    public static void Init()
    {
#if UNITY_EDITOR
        Debug.Assert(Instance == null);
#endif
        Instance = new CameraManager();
    }

    public static void Destory()
    {
        Instance = null;
    }
}