using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : IMarbleEntity
{
    public float _Velocity = 0.5f;
    private float mInitVelocity = 0;
    
    private Vector2 mVelocityDir;
    private Vector3 mInitPosition;
    public Vector3 InitPosition { get { return mInitPosition; } }

    private Animator mAnimator;
    public AnimationCurve _ReviveCurve;

    public void Launch(Vector2 launchDir)
    {
        _Velocity = mInitVelocity;
        mVelocityDir = launchDir;
    }

    private Vector2 Collide(Vector2 point)
    {
        var normal = ((Vector2)transform.position - point).normalized;
        mVelocityDir = Vector2.Reflect(mVelocityDir, normal);
        return normal;
    }

    protected override void Start()
    {
        base.Start();

        mInitPosition = transform.localPosition;
        mInitVelocity = _Velocity;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: use enum type
        BrickController brickController = collision.gameObject.GetComponent<BrickController>();
        WallController wallController = collision.gameObject.GetComponent<WallController>();
        
        if (!brickController && !wallController)
            return;

        Vector2 collidePoint = collision.contacts[0].point;
        var normal = Collide(collidePoint);

        if (brickController)
            brickController.Hit(collidePoint, normal);
        else if (wallController)
            MarbleEventManager.OnMarbleHitBorder.Invoke();
    }
}
