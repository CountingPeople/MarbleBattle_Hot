using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    public Transform target;
    private Vector3 dis;
    // Start is called before the first frame update
    void Start()
    {
        dis = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - dis;
    }
}
