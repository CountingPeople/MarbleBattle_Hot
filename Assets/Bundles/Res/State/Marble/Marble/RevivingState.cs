using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivingState : StateMachineBehaviour
{
    float mTime = 0;
    AnimationCurve mReviveCurve;
    Transform mMarbleTransform;
    Vector3 mStartPosition;
    Vector3 mDstPosition;
    const float _TotalTime = 0.2f;

    MarbleController mController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMarbleTransform = animator.gameObject.transform;
        mController = animator.gameObject.GetComponent<MarbleController>();
        mReviveCurve = mController._ReviveCurve;
        mStartPosition = mMarbleTransform.localPosition;
        mDstPosition = mController.InitPosition;

        mTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float factor = Mathf.Clamp01(mTime / _TotalTime);
        mMarbleTransform.localPosition = Vector3.Lerp(mStartPosition, mDstPosition, mReviveCurve.Evaluate(factor));
        mTime += Time.deltaTime;
        if (mTime > _TotalTime)
            animator.SetBool("Revive", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mController.Revive();
    }
}
