using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;

public class PlayerGuardDragable : MonoBehaviour
{
    private Vector3 mDragPosition;
    private PlayerGuards mSoldierInstance;
    private LeanSelectableByFinger mLeanSelectable;
    bool mIsHold = false;

    private void Awake()
    {
        mSoldierInstance = GetComponent<PlayerGuards>();

        Debug.Assert(mSoldierInstance != null);

        // add LeanTouch's selectable
        mLeanSelectable = gameObject.AddComponent<LeanSelectableByFinger>();
        mLeanSelectable.OnSelectedFinger.AddListener(OnSelected);
        mLeanSelectable.OnSelectedFingerUp.AddListener(OnSelectedUp);
    }

    public void OnSelected(LeanFinger finger)
    {
        Vector3 p3 = finger.ScreenPosition;
        p3.z = -CameraManager.Instance.MarbleCamera.worldToCameraMatrix.MultiplyPoint(transform.position).z;
        mDragPosition = CameraManager.Instance.MarbleCamera.ScreenToWorldPoint(p3);

        mIsHold = true;
    }

    public void OnSelectedUp(LeanFinger finger)
    {
        // can be drop to group?
        bool canDrop = SoldierManager.Instance.TryDropSoldier(gameObject);
        if (canDrop)
        {
            // change guard state
            mSoldierInstance.SoilderState = PlayerGuards.State.Droping;

            Destroy(this);
            Destroy(mLeanSelectable);
        }

        mIsHold = false;
    }

    private void Update()
    {
        if (!mIsHold)
            return;

        Vector3 p3 = Input.mousePosition;
        p3.z = -CameraManager.Instance.MarbleCamera.worldToCameraMatrix.MultiplyPoint(transform.position).z;
        var curPosition = CameraManager.Instance.MarbleCamera.ScreenToWorldPoint(p3);
        Vector3 delta = curPosition - mDragPosition;

        transform.position += delta;
        mDragPosition = curPosition;
    }
}
