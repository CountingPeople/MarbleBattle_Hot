using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : IMarbleEntity
{
    public float _Velocity = 0.5f;
    
    private Vector3 mVelocityDir;
    private Vector3 mInitPosition;
    public Vector3 InitPosition { get { return mInitPosition; } }

    private Animator mAnimator;
    public AnimationCurve _ReviveCurve;

    public void Launch(Vector3 launchDir)
    {
        mVelocityDir = launchDir;
    }

    private Vector3 Collide(Vector3 point)
    {
        var normal = (transform.position - point).normalized;
        mVelocityDir = Vector3.Reflect(mVelocityDir, normal);
        return normal;
    }

    protected override void Start()
    {
        base.Start();

        mInitPosition = transform.localPosition;

        mVelocityDir = Vector2.zero;

        mAnimator = GetComponent<Animator>();

        MarbleEventManager.RequestMarbleStartRevive.AddListener(() => StartRevive());
    }

    public override void Tick()
    {
        Vector3 deltaMove = mVelocityDir * _Velocity * Time.deltaTime;
        transform.localPosition += deltaMove;
    }

    void StartRevive()
    {
        mVelocityDir = Vector2.zero;
        mAnimator.SetBool("Revive", true);
    }

    public void Revive()
    {
        MarbleEventManager.OnMarbleRevive.Invoke();
    }

    // TODO: use CCD
    // TODO: use ColliderMask
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: use enum type
        BrickController brickController = collision.gameObject.GetComponent<BrickController>();
        WallController wallController = collision.gameObject.GetComponent<WallController>();
        
        if (!brickController && !wallController)
            return;

        Vector3 collidePoint = collision.contacts[0].point;
        collidePoint.y = transform.position.y;
        var normal = Collide(collidePoint);

        if (brickController)
            brickController.Hit(collidePoint, normal);
        else if (wallController)
            MarbleEventManager.OnMarbleHitBorder.Invoke();
    }
}
