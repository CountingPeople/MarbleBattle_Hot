using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardDragable : MonoBehaviour
{
    private Vector2 mDragPosition;
    private PlayerGuards mSoldierInstance;

    private void Awake()
    {
        mSoldierInstance = GetComponent<PlayerGuards>();

        Debug.Assert(mSoldierInstance != null);
    }

    public void OnMouseDown()
    {
        mDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDrag()
    {
        var curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 delta = curPosition - mDragPosition;
        transform.position += delta;
        mDragPosition = curPosition;
    }

    private void OnMouseUp()
    {
        // tell SoldierManager
        BattleEventManager.OnSoldierDrop.Invoke(gameObject);

        // change guard state
        mSoldierInstance.SoilderState = PlayerGuards.State.Droping;

        Destroy(this);
    }
}
