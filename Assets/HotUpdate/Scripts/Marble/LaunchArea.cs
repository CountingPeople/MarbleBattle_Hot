using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class LaunchArea : IMarbleEntity
{
    public MarbleController _MarbleController;
    public SpriteRenderer _SpriteNarrow;
    public SpriteRenderer _SpriteMarble;

    private CircleCollider2D _ColliderLaunchArea;
    private float mNarrowDistanceToArea = 0;
    private Vector2 mLaunchDir = Vector2.zero;
    private float mNarrowOriScale = 1.0f;
    private float mNarrowDstScale = 1.0f;

    // GuideLine
    private LineRenderer mGuideLineRender;

    enum State
    {
        Ready, 
        Launching,
        Launched, // already launched
    }
    State mState = State.Ready; // TODO: use StateMachine

    protected override void Start()
    {
        base.Start();

        _ColliderLaunchArea = GetComponent<CircleCollider2D>();
        mGuideLineRender = GetComponent<LineRenderer>();
        mNarrowDistanceToArea = _SpriteNarrow.transform.localPosition.magnitude;

        _SpriteNarrow.enabled = false;
        mGuideLineRender.enabled = false;
        mNarrowOriScale = _SpriteNarrow.transform.localScale.x;
        mNarrowDstScale = mNarrowOriScale * 1.5f;

        MarbleEventManager.OnMarbleRevive.AddListener(OnMarbleRevive);
    }

    void OnMarbleRevive()
    {
        enabled = true;
        mState = State.Ready;
    }

    void DrawGuideLine(Vector3 position, Vector3 dir)
    {
        mGuideLineRender.SetPosition(0, transform.worldToLocalMatrix.MultiplyPoint(position));

        // first hit point
        // TODO : Optimize this, use layer
        RaycastHit2D result = Physics2D.RaycastAll(position, dir)[2];

        mGuideLineRender.SetPosition(1, transform.worldToLocalMatrix.MultiplyPoint(result.point));

        // second hit point
        var reflectDir = Vector2.Reflect((result.point - (Vector2)position).normalized, result.normal);
        
        result = Physics2D.Raycast(result.point + reflectDir * 0.005f, reflectDir);

        mGuideLineRender.SetPosition(2, transform.worldToLocalMatrix.MultiplyPoint((Vector3)result.point));

    }

    // Update is called once per frame
    public override void Tick()
    {
        // TODO: use disable
        if (mState == State.Launched)
            return;

        // TODO: use Input System
        // TODO: use StateMachine
        if (mState == State.Ready)
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            Vector2 hitPosition = Vector2.zero;
            var hitResult = MarbleUtility.isHit(ref hitPosition, _ColliderLaunchArea);

            if (!hitResult)
                return;

            mState = State.Launching;
        }

        if(mState == State.Launching)
        {
            if (Input.GetMouseButton(0))
            {
                // hit launch area
                _SpriteNarrow.enabled = true;
                mGuideLineRender.enabled = true;

                var mousePositionInWorld = MarbleGameManager.Instance.CameraMarbleGame.ScreenToWorldPoint(Input.mousePosition);

                

                var dirWithoutNormalize = (Vector2)(mousePositionInWorld - transform.position);
                var len = dirWithoutNormalize.magnitude;

                mLaunchDir = (dirWithoutNormalize).normalized;

                DrawGuideLine(transform.position, mLaunchDir);

                float angle = Mathf.Acos(mLaunchDir.x) * Mathf.Rad2Deg * Mathf.Sign(mLaunchDir.y);
                var localAngle = _SpriteNarrow.transform.localEulerAngles;
                localAngle.z = angle;
                _SpriteNarrow.transform.localEulerAngles = localAngle;

                _SpriteNarrow.transform.localPosition = mLaunchDir * mNarrowDistanceToArea;

                // scale
                float scale = Mathf.Lerp(mNarrowOriScale, mNarrowDstScale, Mathf.Clamp01((len - _SpriteMarble.bounds.extents.x) / (_ColliderLaunchArea.radius - _SpriteMarble.bounds.extents.x)));
                var transformScale = _SpriteNarrow.transform.localScale;
                transformScale.x = scale;
                _SpriteNarrow.transform.localScale = transformScale;
            }
            else
            {
                _MarbleController.Launch(mLaunchDir);
                mState = State.Launched;

                // Start Battle Game
                BattleGameManager.Start();

                enabled = false;
                _SpriteNarrow.enabled = false;
                mGuideLineRender.enabled = false;
            }
            
        }
    }

    
}
