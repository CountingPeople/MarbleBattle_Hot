using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Lean.Touch;

public class CameraDirector : MonoBehaviour
{
    // LeanTouch
    public LeanFingerFilter Use = new LeanFingerFilter(true);

    [Range(0.1f, 10f)]
	public float _Sensitivity = 1.0f;

	private float mDirectorFactor = 0.0f;
    private float mTimelineDuration = 0.0f;
    private PlayableDirector mDirector = null;
    

    void Start()
    {
        mDirector = GetComponent<PlayableDirector>();
        mTimelineDuration = (float)mDirector.duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected virtual void LateUpdate()
	{
		// Get the fingers we want to use
		var fingers = Use.UpdateAndGetFingers();

		float screenDelta = LeanGesture.GetScreenDelta(fingers).y * _Sensitivity * 0.001f;
        mDirectorFactor += -screenDelta;
        mDirectorFactor = Mathf.Clamp01(mDirectorFactor);

        mDirector.time = mDirectorFactor * mTimelineDuration;
        mDirector.Evaluate();
    }
}
