using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CameraManager
{
    private Camera mBattleCamera;
    public Camera BattleCamera
    {
        get { return mBattleCamera; }
        private set { mBattleCamera = value; }
    }
    const string mBattleCameraName = "BattleCamera";

    private Camera mMarbleCamera;
    public Camera MarbleCamera
    {
        get { return mMarbleCamera; }
        private set { mMarbleCamera = value; }
    }
    const string mMarbleCameraName = "MarbleCamera";

    static public CameraManager Instance = null;

    CameraManager()
    {
        var cameras = Camera.allCameras;
        foreach (var curCam in cameras)
        {
            if (curCam.name == mBattleCameraName)
                mBattleCamera = curCam;

            if (curCam.name == mMarbleCameraName)
                mMarbleCamera = curCam;
        }

        //Check
#if UNITY_EDITOR
        Debug.Assert(mBattleCamera != null && mMarbleCamera != null);
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