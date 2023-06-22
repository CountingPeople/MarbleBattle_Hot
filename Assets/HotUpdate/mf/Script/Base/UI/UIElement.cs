using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public abstract class UIElement : IElement
    {
        #region 属性

        private int _instanceId = 0;
        public int InstanceId => _instanceId;

        /// <summary>
        /// 元素名
        /// </summary>
        public virtual string name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        private protected GameObject _gameObject = null;
        /// <summary>
        /// 游戏物体
        /// </summary>
        public GameObject gameObject => _gameObject;
        /// <summary>
        /// 游戏物体的Transform
        /// </summary>
        public Transform transform => _gameObject.transform;

        private protected RectTransform _rectTransform = null;
        /// <summary>
        /// 游戏物体的RectTransform
        /// </summary>
        public RectTransform rectTransform => _rectTransform;

        public virtual Vector2 sizeDelta { get => rectTransform.sizeDelta; set => rectTransform.sizeDelta = value; }
        public virtual Vector2 anchoredPosition { get => rectTransform.anchoredPosition; set => rectTransform.anchoredPosition = value; }
        public virtual Vector3 position { get => rectTransform.position; set => rectTransform.position = value; }
        public virtual Vector3 localPosition { get => rectTransform.localPosition; set => rectTransform.localPosition = value; }
        public virtual Vector3 localScale { get => rectTransform.localScale; set => rectTransform.localScale = value; }
        public virtual Vector3 eulerAngles { get => transform.eulerAngles; set => transform.eulerAngles = value; }
        public virtual Vector3 localEulerAngles { get => transform.localEulerAngles; set => transform.localEulerAngles = value; }
        public virtual Quaternion localRotation { get => transform.localRotation; set => transform.localRotation = value; }
        public virtual Quaternion rotation { get => transform.rotation; set => transform.rotation = value; }

        private protected UIView _uiView = null;
        /// <summary>
        /// 属于哪个视图
        /// </summary>
        public UIView UIView => _uiView;

        /// <summary>
        /// 隐藏标识符
        /// </summary>
        public HideFlags hideFlags { get => gameObject.hideFlags; set => gameObject.hideFlags = value; }

        /// <summary>
        /// 是否处于激活状态
        /// </summary>
        public virtual bool active
        {
            get 
            {
                if (gameObject != null)
                {
                    return gameObject.activeSelf;
                }
                return false;
            }
            set
            {
                bool b = active;
                if (b != value)
                {
                    if (value)
                    {
                        gameObject.SetActive(value);
                        Internal_OnEnable();
                    }
                    else
                    {
                        Internal_OnDisable();
                        gameObject.SetActive(value);
                    }
                }
            }
        }

        ///// <summary>
        ///// 位置
        ///// </summary>
        //public virtual Vector3 position { get => transform.position; set => transform.position = value; }
        //public virtual Vector3 anchoredPosition { get => rectTransform.anchoredPosition; set => rectTransform.anchoredPosition = value; }


        /// <summary>
        /// 事件字典
        /// </summary>
        private Dictionary<EventTriggerType, UIEventBase> _eventDic = null;

        private ILogger _logger = null;

        public ILogger Logger => _logger;

        #endregion

        #region event
        /// <summary>
        /// 拖动开始事件
        /// </summary>
        public event Action<PointerEventData> onBeginDrag { add { AddEvent(EventTriggerType.BeginDrag, value); } remove { RemoveEvent(EventTriggerType.BeginDrag, value); } }
        /// <summary>
        /// 拖动中事件
        /// </summary>
        public event Action<PointerEventData> onDrag { add { AddEvent(EventTriggerType.Drag, value); } remove { RemoveEvent(EventTriggerType.Drag, value); } }
        /// <summary>
        /// 拖动结束事件
        /// </summary>
        public event Action<PointerEventData> onEndDrag { add { AddEvent(EventTriggerType.EndDrag, value); } remove { RemoveEvent(EventTriggerType.EndDrag, value); } }
        /// <summary>
        /// 指针进入事件
        /// </summary>
        public event Action<PointerEventData> onPointerEnter { add { AddEvent(EventTriggerType.PointerEnter, value); } remove { RemoveEvent(EventTriggerType.PointerEnter, value); } }
        /// <summary>
        /// 指针离开事件
        /// </summary>
        public event Action<PointerEventData> onPointerExit { add { AddEvent(EventTriggerType.PointerExit, value); } remove { RemoveEvent(EventTriggerType.PointerExit, value); } }
        /// <summary>
        /// 指针按下事件
        /// </summary>
        public event Action<PointerEventData> onPointDown { add { AddEvent(EventTriggerType.PointerDown, value); } remove { RemoveEvent(EventTriggerType.PointerDown, value); } }
        /// <summary>
        /// 接收拖动事件
        /// *接受实现IDragHandler接口的组件拖动到本组件上面松开时触发
        /// </summary>
        public event Action<PointerEventData> onDrop { add { AddEvent(EventTriggerType.Drop, value); } remove { RemoveEvent(EventTriggerType.Drop, value); } }
        /// <summary>
        /// 指针抬起事件
        /// </summary>
        public event Action<PointerEventData> onPointerUp { add { AddEvent(EventTriggerType.PointerUp, value); } remove { RemoveEvent(EventTriggerType.PointerUp, value); } }
        /// <summary>
        /// 指针点击事件
        /// 在组件可视的区域按下且抬起时指针处于区域内(按下离开区域后抬起不会触发)
        /// </summary>
        public event Action<PointerEventData> onPointerClick { add { AddEvent(EventTriggerType.PointerClick, value); } remove { RemoveEvent(EventTriggerType.PointerClick, value); } }
        /// <summary>
        /// 初始化潜在的拖动事件
        /// *(必须实现IDragHandler接口)与IPointerDownHandler事件触发条件大致相同
        /// </summary>
        public event Action<PointerEventData> onInitializePotentialDrag { add { AddEvent(EventTriggerType.InitializePotentialDrag, value); } remove { RemoveEvent(EventTriggerType.InitializePotentialDrag, value); } }
        /// <summary>
        /// 滚动事件
        /// *滑轮在上面滚动时触发
        /// </summary>
        public event Action<PointerEventData> onScroll { add { AddEvent(EventTriggerType.Scroll, value); } remove { RemoveEvent(EventTriggerType.Scroll, value); } }
        /// <summary>
        /// 选中物体每帧触发事件
        /// </summary>
        public event Action<BaseEventData> onUpdateSelected { add { AddEvent(EventTriggerType.UpdateSelected, value); } remove { RemoveEvent(EventTriggerType.UpdateSelected, value); } }
        /// <summary>
        /// 选择事件
        /// </summary>
        public event Action<BaseEventData> onSelect { add { AddEvent(EventTriggerType.Select, value); } remove { RemoveEvent(EventTriggerType.Select, value); } }
        /// <summary>
        /// 取消选择事件
        /// </summary>
        public event Action<BaseEventData> onDeselect { add { AddEvent(EventTriggerType.Deselect, value); } remove { RemoveEvent(EventTriggerType.Deselect, value); } }
        /// <summary>
        /// 提交事件
        /// *按下InputManager里的Submit对应的按键(PC、Mac默认:Enter键)。Input.GetButtonDown
        /// </summary>
        public event Action<BaseEventData> onSubmit { add { AddEvent(EventTriggerType.Submit, value); } remove { RemoveEvent(EventTriggerType.Submit, value); } }
        /// <summary>
        /// 取消事件
        ///  *按下InputManager里的Cancel对应的按键(PC、Mac默认:Esc键)。Input.GetButtonDown
        /// </summary>
        public event Action<BaseEventData> onCancel { add { AddEvent(EventTriggerType.Cancel, value); } remove { RemoveEvent(EventTriggerType.Cancel, value); } }
        /// <summary>
        /// 移动事件(上下左右)
        /// *与InputManager里的Horizontal和Vertical按键相对应。Input.GetAxisRaw
        /// </summary>
        public event Action<AxisEventData> onMove { add { AddEvent(EventTriggerType.Move, value); } remove { RemoveEvent(EventTriggerType.Move, value); } }

        private void AddEvent(EventTriggerType triggerType, Action<PointerEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].pointerCallback += value;
            }
            else
            {
                UIEventBase uIEventBase = GameTool.GetUIEventBase(this.gameObject, triggerType);
                if (uIEventBase == null)
                {
                    throw new Exception($"{triggerType}的事件类型没有找到！");
                }
                _eventDic.Add(triggerType, uIEventBase);
                uIEventBase.pointerCallback += value;
            }
        }
        private void AddEvent(EventTriggerType triggerType, Action<BaseEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].baseCallback += value;
            }
            else
            {
                UIEventBase uIEventBase = GameTool.GetUIEventBase(this.gameObject, triggerType);
                if (uIEventBase == null)
                {
                    throw new Exception($"{triggerType}的事件类型没有找到！");
                }
                _eventDic.Add(triggerType, uIEventBase);
                uIEventBase.baseCallback += value;
            }
        }
        private void AddEvent(EventTriggerType triggerType, Action<AxisEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].axisCallback += value;
            }
            else
            {
                UIEventBase uIEventBase = GameTool.GetUIEventBase(this.gameObject, triggerType);
                if (uIEventBase == null)
                {
                    throw new Exception($"{triggerType}的事件类型没有找到！");
                }
                _eventDic.Add(triggerType, uIEventBase);
                uIEventBase.axisCallback += value;
            }
        }

        private void RemoveEvent(EventTriggerType triggerType, Action<PointerEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].pointerCallback -= value;
            }
        }
        private void RemoveEvent(EventTriggerType triggerType, Action<BaseEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].baseCallback -= value;
            }
        }
        private void RemoveEvent(EventTriggerType triggerType, Action<AxisEventData> value)
        {
            if (_eventDic.ContainsKey(triggerType))
            {
                _eventDic[triggerType].axisCallback -= value;
            }
        }
        #endregion

        protected internal UIElement()
        {
            _instanceId = GameTool.GetInstanceId();
            _eventDic = new Dictionary<EventTriggerType, UIEventBase>();
            _logger = LogModule.Instance.GetLogger(this.GetType().Name);
        }

        protected internal UIElement(GameObject obj) : this()
        {
            _gameObject = obj;
            if (_gameObject != null)
            {
                _rectTransform = gameObject.GetComponent<RectTransform>();
            }
        }

        #region static
        /// <summary>
        /// 删除Object
        /// </summary>
        /// <param name="obj"></param>
        public static void Destroy(UIElement obj)
        {
            if (obj.UIView != null)
            {
                obj.UIView.RemoveElement(obj);
            }
            obj.active = false;
            obj.Internal_OnDestroy();
            UnityEngine.Object.Destroy(obj.gameObject);
            obj._gameObject = null;
            GameTool.RecoverInstanceId(obj.InstanceId);
            obj = null;
        }

        /// <summary>
        /// 删除Object
        /// </summary>
        /// <param name="obj"></param>
        public static void DestroyImmediate(UIElement obj)
        {
            if (obj.UIView != null)
            {
                obj.UIView.RemoveElement(obj);
            }
            obj.OnDestroy();
            UnityEngine.Object.DestroyImmediate(obj.gameObject);
            obj._gameObject = null;
            GameTool.RecoverInstanceId(obj.InstanceId);
            obj = null;
        }

        /// <summary>
        /// UIElement 不能直接用 == 判断是否等于Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(UIElement obj)
        {
            if (obj == null)
            {
                return true;
            }
            return obj.gameObject == null;
        }
        #endregion

        #region public
        /// <summary>
        /// 设置到当前层级的第一位
        /// </summary>
        public void SetAsFirstSibling()
        {
            this.rectTransform.SetAsFirstSibling();
        }
        /// <summary>
        /// 设置到当前层级的最后一位
        /// </summary>
        public void SetAsLastSibling()
        {
            this.rectTransform.SetAsLastSibling();
        }
        /// <summary>
        /// 获取一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            return this.gameObject.GetComponent<T>();
        }
        /// <summary>
        /// 获取多个同一类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] GetComponents<T>() where T : Component
        {
            return this.gameObject.GetComponents<T>();
        }
        /// <summary>
        /// 在子物体上面获取一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponentInChildren<T>() where T : Component
        {
            return this.gameObject.GetComponentInChildren<T>();
        }
        /// <summary>
        /// 在子物体上面获取多个同一类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] GetComponentsInChildren<T>() where T : Component
        {
            return this.gameObject.GetComponentsInChildren<T>();
        }
        /// <summary>
        /// 在父物体上面获取一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponentInParent<T>() where T : Component
        {
            return this.gameObject.GetComponentInParent<T>();
        }
        /// <summary>
        /// 在父物体上面获取多个同一类型的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] GetComponentsInParent<T>() where T : Component
        {
            return this.gameObject.GetComponentsInParent<T>();
        }
        /// <summary>
        /// 添加一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : Component
        {
            return this.gameObject.AddComponent<T>();
        }
        /// <summary>
        /// 设计父节点
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(Transform parent)
        {
            this.transform.SetParent(parent);
        }

        /// <summary>
        /// 加载场景时候不删除
        /// </summary>
        public void DontDestroyOnLoad()
        {
            UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// 关闭这个组件
        /// </summary>
        public void Close()
        {
            Internal_Close();
        }

        #endregion


        #region virtual

        /// <summary>
        /// 启用时调用
        /// </summary>
        protected virtual void OnEnable()
        {

        }

        /// <summary>
        /// 隐藏时调用
        /// </summary>
        protected virtual void OnDisable()
        {

        }

        /// <summary>
        /// 删除时调用
        /// </summary>
        protected virtual void OnDestroy()
        {

        }

        internal virtual void Internal_OnDestroy()
        {
            this.OnDestroy();
        }
        /// <summary>
        /// 内部显示调用方法
        /// </summary>
        internal virtual void Internal_OnEnable()
        {
            this.OnEnable();
        }
        /// <summary>
        /// 内部隐藏调用方法
        /// </summary>
        internal virtual void Internal_OnDisable()
        {
            this.OnDisable();
        }

        /// <summary>
        /// 内部关闭调用方法
        /// </summary>
        internal virtual void Internal_Close()
        {
            Destroy(this);
        }
        #endregion

        #region object override

        public override int GetHashCode()
        {
            return this.InstanceId;
        }

        #endregion

        #region operator

        public static implicit operator GameObject(UIElement element)
        {
            return element.gameObject;
        }
        public static implicit operator Transform(UIElement element)
        {
            return element.transform;
        }
        public static implicit operator RectTransform(UIElement element)
        {
            return element.rectTransform;
        }
        #endregion
    }
}
