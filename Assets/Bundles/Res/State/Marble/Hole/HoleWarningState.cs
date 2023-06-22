using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleWarningState : StateMachineBehaviour
{
    HoleController mHoleController;
    SpriteRenderer mRender;
    Color mInitColor;
    Color mWarningColor = Color.red;
    float mTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mRender = animator.gameObject.GetComponent<SpriteRenderer>();
        mHoleController = animator.gameObject.GetComponent<HoleController>();
        mInitColor = mRender.color;
        mTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mTime += Time.deltaTime;
        if(mTime > 0.3f)
        {
            animator.SetBool("Hit", false);
        }
        
        mRender.color = Color.Lerp(mInitColor, mWarningColor, Mathf.Sin(40 * Time.realtimeSinceStartup) * 0.5f + 0.5f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MarbleEventManager.RequestMarbleStartRevive.Invoke();
        mRender.color = mInitColor;
    }
}
