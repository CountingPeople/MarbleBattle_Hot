using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : IMarbleEntity
{
    SpriteRenderer mRender;
    Animator mAnimator;
    MarbleController mMarbleController;
    float mOldVelocity = 0;

    Collider mCollider = null;

    private void Awake()
    {
        mRender = GetComponent<SpriteRenderer>();
        mAnimator = GetComponent<Animator>();
        mCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    public override void Tick()
    {
        // TODO: use InputSystem @zhangrufu
        if (!Input.GetMouseButtonDown(0))
            return;

        Vector3 hitPosition = Vector2.zero;
        var hitResult = MarbleUtility.isHit(ref hitPosition, mCollider);
        if (!hitResult)
            return;

        MarbleEventManager.RequestMarbleStartRevive.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Enter Warning
        mMarbleController = collision.gameObject.GetComponent<MarbleController>();
        mOldVelocity = mMarbleController._Velocity;
        mMarbleController._Velocity = 0;

        mAnimator.SetBool("Hit", true);
    }
}
