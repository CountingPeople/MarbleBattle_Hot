using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAreaGizmo : MonoBehaviour
{
    public Vector2 _MonsterAreaSize = Vector2.one;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 size = _MonsterAreaSize;
        size.z = 0.1f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
