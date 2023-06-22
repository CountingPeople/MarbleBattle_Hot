using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dragable : MonoBehaviour
{
    private Vector2 mDragPosition;

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
        Debug.Log("Spawn");
    }
}