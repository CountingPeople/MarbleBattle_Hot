using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarbleGameState
{
    Gaming,
    Pause,
    Finish
}

// all base Entity
public abstract class IMarbleEntity : MonoBehaviour
{
    public abstract void Tick();

    protected virtual void Start()
    {
        MarbleGameManager.Instance.RegisterBattleEntity(this);
    }

    protected virtual void OnDestroy()
    {
        // TODO: manager
        if(MarbleGameManager.Instance != null)
            MarbleGameManager.Instance.UnregisterBattleEntity(this);
    }
}

public class MarbleGameManager
{
    private Camera mCameraMarbleGame;
    public Camera CameraMarbleGame
    {
        get { return mCameraMarbleGame; }
    }

    private static MarbleGameManager mInstance = null;
    public static MarbleGameManager Instance
    {
        get { return mInstance; }
    }

    private MarbleGameManager()
    {
        mTickItem = new List<IMarbleEntity>();

        GlobalEventManager.OnLevelFinished.AddListener(OnLevelFinished);
        BattleEventManager.OnPlayerDead.AddListener(OnLevelFinished);
    }

    public static void Init(Camera camera)
    {
        if (mInstance != null)
            return;

        mInstance = new MarbleGameManager();
        mInstance.mCameraMarbleGame = camera;

        GameState = MarbleGameState.Gaming;
    }

    public static void Destory()
    {
        mInstance = null;
    }

    // Game State
    private static MarbleGameState mGameState;
    public static MarbleGameState GameState
    {
        private set { mGameState = value; }
        get { return mGameState; }
    }

    private static List<IMarbleEntity> mTickItem = null;

    // Update is called once per frame
    public void Tick(float deltaTime)
    {
        if (GameState != MarbleGameState.Gaming)
            return;

        BrickManager.Instance.Tick();

        // tick every entity
        for (int i = 0; i < mTickItem.Count; ++i)
        {
            mTickItem[i].Tick();
        }
    }

    public void RegisterBattleEntity(IMarbleEntity entity)
    {
#if UNITY_EDITOR
        Debug.Assert(entity != null, "Battle Entity is null");
#endif

        mTickItem.Add(entity);
    }

    public void UnregisterBattleEntity(IMarbleEntity entity)
    {
        mTickItem.Remove(entity);
    }

    void OnLevelFinished()
    {
        GameState = MarbleGameState.Finish;
    }
}

public static class MarbleUtility
{
    public static bool isHit(ref Vector2 hitPosition, Collider2D collider)
    {
        RaycastHit2D[] result = null;
        int hitIndex = -1;

        Ray ray = MarbleGameManager.Instance.CameraMarbleGame.ScreenPointToRay(Input.mousePosition);
        result = Physics2D.RaycastAll(ray.origin, ray.direction);
        if (result.Length == 0)
            return false;

        for (hitIndex = 0; hitIndex < result.Length; ++hitIndex)
        {
            if (result[hitIndex].collider == collider)
                break;
        }

        if (hitIndex == result.Length)
            return false;

        hitPosition = result[hitIndex].point;
        return true;
    }
}