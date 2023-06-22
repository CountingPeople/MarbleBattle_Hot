using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

	//摄像机参照的模型
	public Transform target;
	//摄像机距离模型的默认距离
	public float distance = 20.0f;
	//鼠标在x轴和y轴方向移动的速度
	float x;
	float y;
	//限制旋转角度的最小值与最大值
	float yMinLimit = 10.0f;
	float yMaxLimit = 80.0f;
	//鼠标在x和y轴方向移动的速度
	float xSpeed = 250.0f;
	float ySpeed = 120.0f;

	Quaternion rotation;
	// Use this for initialization
	void Start()
	{
		//初始化x和y轴角度，使其等于参照模型的角度
		Vector2 Angles = transform.eulerAngles;
		y = Angles.y;
		x = Angles.x;

	}
	void LateUpdate()
	{
		if (target)
		{
			//我的分析：1.根据垂直方向的增减量修改摄像机距离参照物的距离
			distance += Input.GetAxis("Mouse ScrollWheel")*30;
			//根据鼠标移动修改摄像机的角度
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			//我的分析：2.这句相当于限制了摄像机在X轴方向上的视觉范围
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			//我的分析：3.刚开始我还在纠结为什么将y作为X轴的旋转角度量，将x作为Y轴的角度旋转量。但仔细一想，这里的x和y分别就指的是鼠标水平方向和垂直方向的移动量，对应的不就刚好是y轴方向的转动与x轴方向的转动么，哈哈
			rotation = Quaternion.Euler(y, x, 0);
			//我的分析：4.这一句就比较有意思了，后面详解
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
			//设置摄像机的位置与旋转
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	float ClampAngle(float angle, float min, float max)
	{
		//我的分析：5.这里之所以要加360，因为比如-380,与-20是等价的
		if (angle < -360)
		{
			angle += 360;
		}
		if (angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp(angle, min, max);
	}

}
