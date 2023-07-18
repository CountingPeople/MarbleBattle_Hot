using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArea : IMarbleEntity
{
    public MarbleController _MarbleController;
    public MeshRenderer _SpriteNarrow;
    public Transform _NarrowRotation;
    public Transform _NarrowScale;
    public MeshRenderer _SpriteMarble;

    private SphereCollider _ColliderLaunchArea;
    private float mNarrowDistanceToArea = 0;
    private Vector3 mLaunchDir = Vector2.zero;
    private float mNarrowOriScale = 1.0f;
    private float mNarrowDstScale = 1.0f;

    // GuideLine
    private LineRenderer mGuideLineRender;

    private Vector3 mMousePositionReady = Vector3.zero;

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

        _ColliderLaunchArea = GetComponent<SphereCollider>();
        mGuideLineRender = GetComponent<LineRenderer>();
        mNarrowDistanceToArea = _SpriteNarrow.transform.localPosition.magnitude;

        _SpriteNarrow.enabled = false;
        mGuideLineRender.enabled = false;
        mNarrowOriScale = _NarrowScale.localScale.x;
        mNarrowDstScale = mNarrowOriScale * 1.4f;

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
        RaycastHit result = Physics.RaycastAll(position, dir)[0];

        mGuideLineRender.SetPosition(1, transform.worldToLocalMatrix.MultiplyPoint(result.point));

        // second hit point
        var reflectDir = Vector3.Reflect((result.point - position).normalized, result.normal);
        
        Physics.Raycast(result.point + reflectDir * 0.005f, reflectDir, out result);

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

            Vector3 hitPosition = Vector2.zero;
            var hitResult = MarbleUtility.isHit(ref hitPosition, _ColliderLaunchArea, LayerMask.GetMask("LaunchArea"));
            
            if (!hitResult)
                return;

            mState = State.Launching;
            mMousePositionReady = Input.mousePosition;
        }

        if(mState == State.Launching)
        {
            if (Input.GetMouseButton(0))
            {
                // hit launch area
                _SpriteNarrow.enabled = true;
                mGuideLineRender.enabled = true;

                // .(mouseReady) ---- .(now)->
                // in screen space
                var dirWithoutNormalize = (Input.mousePosition - mMousePositionReady);
                var len = dirWithoutNormalize.magnitude;
                var dirWithNormalize = (dirWithoutNormalize).normalized;
                
                // map to world space
                mLaunchDir = new Vector3(dirWithNormalize.x, 0.0f, dirWithNormalize.y);

                // DrawGuideLine(transform.position, mLaunchDir);

                // narrow's rotation
                float angle = Mathf.Acos(mLaunchDir.x) * Mathf.Rad2Deg * -Mathf.Sign(mLaunchDir.z);
                var localAngle = _NarrowRotation.localEulerAngles;
                localAngle.y = angle;
                _NarrowRotation.localEulerAngles = localAngle;

                // _SpriteNarrow.transform.localPosition = mLaunchDir * mNarrowDistanceToArea;

                // narrow's scale
                float scale = Mathf.Lerp(mNarrowOriScale, mNarrowDstScale, Mathf.Clamp01(len / 300));
                var transformScale = _SpriteNarrow.transform.localScale;
                transformScale.x = scale;
                _NarrowScale.localScale = transformScale;
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
