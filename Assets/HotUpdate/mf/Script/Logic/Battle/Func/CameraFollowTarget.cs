using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target = null;     // 目标玩家
    [SerializeField]
    [Range(0, 360)]
    float horizontalAngle = 270f;      // 水平角度
    [SerializeField]
    [Range(0, 20)]
    float initialHeight = 2f;    // 人物在视野内屏幕中的位置设置

    [SerializeField]
    [Range(10, 90)]
    float initialAngle = 40f;   // 初始俯视角度
    [SerializeField]
    [Range(10, 90)]
    float maxAngle = 50f;     // 最高俯视角度
    [SerializeField]
    [Range(10, 90)]
    float minAngle = 35f;     // 最低俯视角度

    float initialDistance;    // 初始化相机与玩家的距离 根据角度计算
    [SerializeField]
    [Range(1, 300)]
    float maxDistance = 100;        // 相机距离玩家最大距离
    [SerializeField]
    [Range(1, 100)]
    float minDistance = 5f;        // 相机距离玩家最小距离

    [SerializeField]
    [Range(1, 100)]
    float zoomSpeed = 50;       // 缩放速度

    [SerializeField]
    [Range(1f, 200)]
    float swipeSpeed = 50;      // 左右滑动速度

    float scrollWheel;        // 记录滚轮数值
    float tempAngle;          // 临时存储摄像机的初始角度
    Vector3 tempVector = new Vector3();

    void Start()
    {
        InitCamera();
    }

    void LateUpdate()
    {
        if (target == null) {
            target = GameObject.Find("player").transform;
            return;
        }
        FollowPlayer();
    }

    /// <summary>
    /// 初始化 相机与玩家距离
    /// </summary>
    void InitCamera()
    {
        tempAngle = initialAngle;

        initialDistance = Mathf.Sqrt((initialAngle - minAngle) / Calculate()) + minDistance;

        initialDistance = 10;// Mathf.Clamp(initialDistance, minDistance, maxDistance);

    }

    /// <summary>
    /// 相机跟随玩家
    /// </summary>
    void FollowPlayer()
    {
        float upRidus = Mathf.Deg2Rad * initialAngle;
        float flatRidus = Mathf.Deg2Rad * horizontalAngle;

        float x = initialDistance * Mathf.Cos(upRidus) * Mathf.Cos(flatRidus);
        float z = initialDistance * Mathf.Cos(upRidus) * Mathf.Sin(flatRidus);
        float y = initialDistance * Mathf.Sin(upRidus);

        transform.position = Vector3.zero;
        tempVector.Set(x, y, z);
        tempVector = tempVector + target.position;
        transform.position = tempVector;
        tempVector.Set(target.position.x, target.position.y + initialHeight, target.position.z);

        transform.LookAt(tempVector);
    }

    float Calculate()
    {
        float dis = maxDistance - minDistance;
        float ang = maxAngle - minAngle;
        float line = ang / (dis * dis);
        return line;
    }
}
