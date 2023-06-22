using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using zFrame.UI;

public class PlayerCtrl : MonoBehaviour
{
    private Vector3 moveVec;
    public Joystick joystick;
    private float speed=10;
    private Animator animator;

    private void Awake()
    {
        animator=GetComponent<Animator>();
    }

    private void Start()
    {
        joystick.OnValueChanged.AddListener(JoyBack);

    }

    private void JoyBack(Vector2 param)
    {
        SetMove(param.x, param.y);
    }

    private void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //SetMove(h, v);
    }

    private void SetMove(float h,float v)
    {
        moveVec = new Vector3(h, 0, v);

        if (h != 0 || v != 0)
        {
            moveVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveVec;
            GetComponent<NavMeshAgent>().Move(moveVec * Time.deltaTime * speed);

            Vector3 vec = Quaternion.Euler(0, 0, 0) * moveVec;
            Quaternion qua = Quaternion.LookRotation(vec);
            transform.rotation = Quaternion.Lerp(transform.rotation, qua, Time.deltaTime * 100);

            PlayAction(ActionName.Walk);
        }
        else if(h == 0 || v == 0)
        {
            PlayAction(ActionName.Idle);
        }
    }

    public void PlayAction(ActionName actionName)
    {
        //string actStr = roleBase.GetActionName(actionName);
        //if (animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        //{
        //    string curStr = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //    if (actStr != curStr)
        //    {
        //        animator.Play(roleBase.GetActionName(actionName),0,0);
        //    }
        //}
    }
}
