using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoSingleton<CameraFollow>
{
    private Camera cam;
    private GameObject player;
    private Vector3 camOffset = Vector3.zero;
    private Vector3 oriCamOffSet = Vector3.zero;
    private Vector3 bossCamOffSet = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            cam = Camera.main;
            //player = GameObject.FindGameObjectWithTag(GameConstant.rolePlayer);
            return;
        }
        if (camOffset == Vector3.zero)
        {
            camOffset = cam.transform.position - player.transform.position;
            oriCamOffSet = camOffset;
            bossCamOffSet = camOffset + new Vector3(-3, 0, 0);
        }
        cam.transform.position = player.transform.position + camOffset;

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetHor();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ResetHor();
        }
    }

    public void SetHor()
    {
        DOTween.To(() => camOffset, x => camOffset = x, bossCamOffSet, 2);
    }

    public void ResetHor()
    {
        DOTween.To(() => camOffset, x => camOffset = x, oriCamOffSet, 2);
    }
}
