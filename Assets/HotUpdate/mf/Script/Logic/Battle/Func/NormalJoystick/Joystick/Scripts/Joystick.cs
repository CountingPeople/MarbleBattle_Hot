// Copyright (c) Bian Shanghai
// https://github.com/Bian-Sh/UniJoystick
// Licensed under the MIT license. See the LICENSE.md file in the project root for more information.
namespace zFrame.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float maxRadius = 100; //Handle 移动最大半径
        [EnumFlags]
        public Direction activatedAxis = (Direction)(-1); //选择激活的轴向
        [SerializeField] bool dynamic = true; // 动态摇杆
        [SerializeField] Transform handle; //摇杆
        [SerializeField] Transform backGround; //背景
        [SerializeField] Transform lockImg;//锁定按钮 

        private bool isLockJoystick = false;//是否锁定摇杆
        private Vector3 dragPosition;

        private bool isCountHover = false;//是否开始悬停计时
        private float hoverTimer = 0;
        private float hoverTimeLimit = 1;

        private bool isJoystickEdge = false;//是否开始移动到边缘计时
        private float edgeTimer = 0;
        private float edgeTimeLimit = 0.5f;

        private Vector3 touchPos;
        private float touchLength;
        private bool isJoyCenter;

        public JoystickEvent OnValueChanged = new JoystickEvent(); //事件 ： 摇杆被拖拽时
        public JoystickEvent OnPointerDown = new JoystickEvent(); // 事件： 摇杆被按下时
        public JoystickEvent OnPointerUp = new JoystickEvent(); //事件 ： 摇杆上抬起时
        public bool IsDraging { get { return fingerId != int.MinValue; } } //摇杆拖拽状态
        public bool DynamicJoystick //运行时代码配置摇杆是否为动态摇杆
        {
            set
            {
                if (dynamic != value)
                {
                    dynamic = value;
                    ConfigJoystick();
                }
            }
            get
            {
                return dynamic;
            }
        }
        #region MonoBehaviour functions
        private void Awake()
        {
            SetJoystickAlpha(0.2f);
            backGroundOriginLocalPostion = backGround.localPosition;
        }
    
        void Update() {
            //SetLockShow();
            HoverEvent();
            OnValueChanged.Invoke(handle.localPosition / maxRadius);
        }
        void OnDisable() => RestJoystick(); //意外被 Disable 各单位需要被重置
        #endregion

        #region The implement of pointer event Interface
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            isLockJoystick = false;
            SetJoystickAlpha(1);
            if (eventData.pointerId < -1 || IsDraging) return; //适配 Touch：只响应一个Touch；适配鼠标：只响应左键
            fingerId = eventData.pointerId;
            pointerDownPosition = eventData.position;
            if (dynamic)
            {
                pointerDownPosition[2] = eventData.pressEventCamera?.WorldToScreenPoint(backGround.position).z ?? backGround.position.z;
                backGround.position = eventData.pressEventCamera?.ScreenToWorldPoint(pointerDownPosition) ?? pointerDownPosition; ;
            }
            OnPointerDown.Invoke(eventData.position);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (fingerId != eventData.pointerId)
            {
                return;
            }
            isLockJoystick = false;
            Vector2 direction = eventData.position - (Vector2)pointerDownPosition; //得到BackGround 指向 Handle 的向量
            float radius = Mathf.Clamp(Vector3.Magnitude(direction), 0, maxRadius); //获取并锁定向量的长度 以控制 Handle 半径
            Vector2 localPosition = new Vector2()
            {
                x = (0 != (activatedAxis & Direction.Horizontal)) ? (direction.normalized * radius).x : 0, //确认是否激活水平轴向
                y = (0 != (activatedAxis & Direction.Vertical)) ? (direction.normalized * radius).y : 0       //确认是否激活垂直轴向，激活就搞事情
            };
            handle.localPosition = localPosition;      //更新 Handle 位置

            dragPosition = eventData.position;
            SetJoystickState();
        }

        /// <summary>
        /// 设置摇杆的状态
        /// </summary>
        private void SetJoystickState() 
        {
            /// 设置锁的位置
            touchPos = handle.localPosition / maxRadius;
            touchLength = Mathf.Pow(touchPos.x, 2) + Mathf.Pow(touchPos.y, 2);
            isJoystickEdge = touchLength > 0.9f;

            isJoyCenter = touchLength < 0.1f;
            SetJoystickAlpha(isJoyCenter ? 0.2f:1);
        }

        /// <summary>
        /// 设置摇杆的透明度
        /// </summary>
        /// <param name="value"></param>
        private void SetJoystickAlpha(float value) 
        {
            if (backGround.GetComponent<CanvasGroup>() != null)
            {
                backGround.GetComponent<CanvasGroup>().alpha = value;
            }
        }

        /// <summary>
        /// 设置锁显示
        /// </summary>
        //private void SetLockShow() 
        //{
        //    if (lockImg == null) 
        //    {
        //        return;
        //    }

        //    if (isJoystickEdge)
        //    {
        //        edgeTimer += Time.deltaTime;
        //        if (edgeTimer > edgeTimeLimit)
        //        {
        //            lockImg.position = (handle.position - backGround.position).normalized * 150 + backGround.position;
        //            lockImg.gameObject.SetActive(isJoystickEdge);
        //        }
        //    }
        //    else {
        //        isJoystickEdge = false;
        //        edgeTimer = 0;
        //        if (lockImg.gameObject.activeSelf)
        //        {
        //            lockImg.gameObject.SetActive(false);
        //        }
        //    }
            
        //}

        /// <summary>
        /// 悬停事件
        /// </summary>
        private void HoverEvent() 
        {
            if (lockImg == null)
            {
                return;
            }

            if (lockImg.gameObject.activeSelf)
            {
                isCountHover = Vector3.Distance(dragPosition, lockImg.position) < lockImg.GetComponent<RectTransform>().rect.width / 2;
                if (isCountHover)
                {
                    hoverTimer += Time.deltaTime;
                    if (hoverTimer > hoverTimeLimit)
                    {
                        //Debug.Log($"锁定摇杆{isCountHover} {hoverTimer}");
                        isLockJoystick = true;
                    }
                }
                else 
                {
                    hoverTimer = 0;
                    isLockJoystick = false;
                }
            }
            else
            {
                hoverTimer = 0;
            }
        }


        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (isLockJoystick) 
            {
                return;
            }
            if (fingerId != eventData.pointerId)
            {
                return;//正确的手指抬起时才会重置摇杆；
            }
            RestJoystick();
            OnPointerUp.Invoke(eventData.position);

            SetJoystickState();
        }
        #endregion

        #region Assistant functions / fields / structures
        void RestJoystick()
        {
            backGround.localPosition = backGroundOriginLocalPostion;
            handle.localPosition = Vector3.zero;
            fingerId = int.MinValue; 
        }

        void ConfigJoystick() //配置动态/静态摇杆
        {
                if (!dynamic) backGroundOriginLocalPostion = backGround.localPosition;
                GetComponent<Image>().raycastTarget = dynamic;
                handle.GetComponent<Image>().raycastTarget = !dynamic;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!handle) handle = transform.Find("BackGround/Handle");
            if (!backGround) backGround = transform.Find("BackGround");
            ConfigJoystick();
        }
#endif
        private Vector3 backGroundOriginLocalPostion, pointerDownPosition;
        private int fingerId = int.MinValue; //当前触发摇杆的 pointerId ，预设一个永远无法企及的值
        [System.Serializable] public class JoystickEvent : UnityEvent<Vector2> { }
        [System.Flags]
        public enum Direction
        {
            Horizontal = 1 << 0,
            Vertical = 1 << 1
        }
        #endregion

        /// <summary>
        /// 设置移动到边缘显示锁的时间
        /// </summary>
        public void SetShowLockTime(float time) 
        {
            edgeTimeLimit = time;
        }

        /// <summary>
        /// 设置悬停在锁上面锁定摇杆的时间
        /// </summary>
        public void SetHoverLockTime(float time) 
        {
            hoverTimeLimit = time;
        }
    }
}
