using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{

    private float mDirectorFactor = 1.0f;
    public float DirectorFactor
    {
        get { return mDirectorFactor; }
        set { mDirectorFactor = value; }
    }

    void Start()
    {
        Lean.Touch.LeanTouch.OnFingerSwipe += OnFingerSwipe;
    }

    private void OnFingerSwipe(Lean.Touch.LeanFinger obj)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
