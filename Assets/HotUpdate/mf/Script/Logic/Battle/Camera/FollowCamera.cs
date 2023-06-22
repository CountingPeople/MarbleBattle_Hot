using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    private Transform cacheTransform;
    public Transform CacheTransform
    {
        get
        {
            if (cacheTransform == null)
                cacheTransform = GetComponent<Transform>();
            return cacheTransform;
        }
    }

    public Camera CacheCamera
    {
        get
        {
            if (targetCamera == null)
                targetCamera = GetComponent<Camera>();
            return targetCamera;
        }
    }

    private Transform cacheCameraTransform;
    public Transform CacheCameraTransform
    {
        get
        {
            if (cacheCameraTransform == null && CacheCamera != null)
                cacheCameraTransform = CacheCamera.GetComponent<Transform>();
            return cacheCameraTransform;
        }
    }

    public Camera targetCamera;
    public Transform target;
    public Vector3 targetOffset;
    [Header("Follow")]
    public float damping = 10.0f;
    public bool dontSmoothFollow;
    [Header("Look at")]
    public float lookAtDamping = 2.0f;
    public bool dontSmoothLookAt;
    [Header("Rotation")]
    public float xRotation;
    public float yRotation;
    [Tooltip("If this is TRUE, will update Y-rotation follow target")]
    public bool useTargetYRotation;
    [Header("Zoom")]
    public float zoomDistance = 10.0f;
    [Header("Zoom by ratio")]
    public bool zoomByAspectRatio;
    public List<ZoomByAspectRatioSetting> zoomByAspectRatioSettings = new List<ZoomByAspectRatioSetting>()
    {
        new ZoomByAspectRatioSetting() { width = 16, height = 9, zoomDistance = 0.0001f },
        new ZoomByAspectRatioSetting() { width = 16, height = 10, zoomDistance = 1.75f },
        new ZoomByAspectRatioSetting() { width = 3, height = 2, zoomDistance = 3f },
        new ZoomByAspectRatioSetting() { width = 4, height = 3, zoomDistance = 5.5f },
        new ZoomByAspectRatioSetting() { width = 5, height = 4, zoomDistance = 7 },
    };
    [Header("Wall hit spring")]
    public bool enableWallHitSpring;
    public LayerMask wallHitLayerMask = -1;
    public QueryTriggerInteraction wallHitQueryTriggerInteraction = QueryTriggerInteraction.Ignore;

    // Improve Garbage collector
    private Vector3 targetPosition;
    private float targetYRotation;
    private Vector3 wantedPosition;
    private float wantedYRotation;
    private float windowaspect;
    private float deltaTime;
    private Quaternion currentRotation;
    private Quaternion lookAtRotation;
    private Ray tempRay;
    private RaycastHit[] tempHits;
    private float tempDistance;

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tempRay.origin, tempRay.origin + tempRay.direction * tempDistance);
#endif
    }

    protected virtual void LateUpdate()
    {
        targetPosition = target == null ? Vector3.zero : target.position;
        Vector3 upVector = target == null ? Vector3.up : target.up;
        targetPosition += (targetOffset.x * CacheTransform.right) + (targetOffset.y * upVector) + (targetOffset.z * CacheTransform.forward);
        targetYRotation = target == null ? 0 : target.eulerAngles.y;

        if (zoomByAspectRatio)
        {
            windowaspect = CacheCamera.aspect;
            zoomByAspectRatioSettings.Sort();
            foreach (ZoomByAspectRatioSetting data in zoomByAspectRatioSettings)
            {
                if (windowaspect + windowaspect * 0.025f > data.Aspect() &&
                    windowaspect - windowaspect * 0.025f < data.Aspect())
                {
                    zoomDistance = data.zoomDistance;
                    break;
                }
            }
        }

        if (zoomDistance == 0f)
            zoomDistance = 0.0001f;

        if (CacheCamera != null && CacheCamera.orthographic)
            CacheCamera.orthographicSize = zoomDistance;

        deltaTime = Time.deltaTime;

        wantedPosition = targetPosition;
        wantedYRotation = useTargetYRotation ? targetYRotation : yRotation;

        // Convert the angle into a rotation
        currentRotation = Quaternion.Euler(xRotation, wantedYRotation, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        wantedPosition -= currentRotation * Vector3.forward * zoomDistance;

        lookAtRotation = Quaternion.LookRotation(targetPosition - wantedPosition);

        if (enableWallHitSpring)
        {
            float nearest = float.MaxValue;
            tempRay = new Ray(targetPosition, lookAtRotation * -Vector3.forward);
            tempDistance = Vector3.Distance(targetPosition, wantedPosition);
            tempHits = Physics.RaycastAll(tempRay, tempDistance, wallHitLayerMask, wallHitQueryTriggerInteraction);
            for (int i = 0; i < tempHits.Length; i++)
            {
                if (tempHits[i].distance < nearest)
                {
                    nearest = tempHits[i].distance;
                    wantedPosition = tempHits[i].point;
                }
            }
        }

        // Update position
        if (!dontSmoothFollow)
            CacheTransform.position = Vector3.Lerp(CacheTransform.position, wantedPosition, damping * deltaTime);
        else
            CacheTransform.position = wantedPosition;

        // Update rotation
        if (!dontSmoothLookAt)
            CacheTransform.rotation = Quaternion.Slerp(CacheTransform.rotation, lookAtRotation, lookAtDamping * deltaTime);
        else
            CacheTransform.rotation = lookAtRotation;
    }

    [System.Serializable]
    public struct ZoomByAspectRatioSetting : System.IComparable
    {
        public int width;
        public int height;
        public float zoomDistance;

        public float Aspect()
        {
            return (float)width / (float)height;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ZoomByAspectRatioSetting))
                return 0;
            return Aspect().CompareTo(((ZoomByAspectRatioSetting)obj).Aspect());
        }
    }
}
