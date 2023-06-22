using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{

    private static LevelManager mInstance = null;
    public static LevelManager Instance
    {
        get { return mInstance; }
    }

    public static void Init()
    {
        if (mInstance != null)
            return;

        mInstance = new LevelManager();
    }

    public static void Destory()
    {
        mInstance = null;
    }

    public class Level
    {
        public int LevelID = 0;
        public int SubLevelID = 0;
    }

    Level mCurLevel = null;
    public Level CurrentLevel
    {
        get { return mCurLevel; }
    }
    float mCurLevelElapsedTime = 0;
    float mCurLevelTime = 0;
    public float RemainTime
    {
        get { return Mathf.Max(mCurLevelTime - mCurLevelElapsedTime, 0.0f); }
    }

    // Start is called before the first frame update
    LevelManager()
    {
        mCurLevel = new Level();

        // default 1 - 1
        SetLevel(1, 1);

        
    }

    // Update is called once per frame
    public void Tick(float deltaTime)
    {
        if (BattleGameManager.GameState != BattleGameState.Gaming)
            return;

        // level update
        mCurLevelElapsedTime += deltaTime;
        if (mCurLevelElapsedTime > mCurLevelTime)
        {
            AdvanceLevel();

            mCurLevelElapsedTime = 0;
        }
    }

    public void SetLevel(int level, int stage)
    {
        mCurLevel.LevelID = level;
        mCurLevel.SubLevelID = stage;

        mCurLevelTime = 0;

        // get first level
        cfg.LevelConfig levelInfo = DataManager.DataTable.TbLevelConfig.Get(mCurLevel.LevelID, mCurLevel.SubLevelID);
        mCurLevelTime = levelInfo.StageTime;
    }

    void AdvanceLevel()
    {
        // enter next stage
        ++mCurLevel.SubLevelID;

        cfg.LevelConfig levelInfo = DataManager.DataTable.TbLevelConfig.Get(mCurLevel.LevelID, mCurLevel.SubLevelID);

        if (levelInfo != null)
        {
            mCurLevelTime = levelInfo.StageTime;
            mCurLevelElapsedTime = 0.0f;

            GlobalEventManager.OnLevelAdvanced.Invoke();
        }
        else
        {
            mCurLevelTime = 0.0f;
            mCurLevelElapsedTime = 0.0f;

            GlobalEventManager.OnLevelFinished.Invoke();
        }
    }
}
