using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Lean.Touch;
using Lean.Common;

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

        mDirector.time = 0;
        mDirector.Evaluate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected virtual void LateUpdate()
	{
		// Get the fingers we want to use
		var fingers = Use.UpdateAndGetFingers();

        if (fingers.Count != 1)
            return;

        var finger = fingers[0];
        if (finger.IsOverGui)
            return;

        foreach (var select in LeanSelect.Instances)
        {
            if (select.Selectables.Count > 0)
                return;
        }

        float screenDelta = LeanGesture.GetScreenDelta(fingers).y * _Sensitivity * 0.001f;
        mDirectorFactor += -screenDelta;
        mDirectorFactor = Mathf.Clamp01(mDirectorFactor);

        mDirector.time = mDirectorFactor * mTimelineDuration;
        mDirector.Evaluate();
    }
}
