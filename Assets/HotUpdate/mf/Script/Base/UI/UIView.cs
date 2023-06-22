using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI视图基类
    /// </summary>
    public abstract class UIView : UIElement
    {
        /// <summary>
        /// UI Resources路径
        /// </summary>
        public abstract string UIPath { get; }

        /// <summary>
        /// 设置UI层级
        /// </summary>
        public int SiblingIndex { get { return this.rectTransform.GetSiblingIndex(); } set { this.rectTransform.SetSiblingIndex(value); } }
        /// <summary>
        /// 是否是焦点
        /// </summary>
        public bool IsFocus { get; internal set; }
        private readonly Dictionary<int, UIElement> _uiElementDic;
        protected Dictionary<int, UIElement> UIElementDic { get => _uiElementDic; }

        private Dictionary<int, Coroutine> _coroutineDic;

        protected internal UIView() : base()
        {
            _uiElementDic = new Dictionary<int, UIElement>();
            _coroutineDic = new Dictionary<int, Coroutine>();
        }

        #region protected


        /// <summary>
        /// 启动协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        protected Coroutine StartCoroutine(IEnumerator routine)
        {
            Coroutine coroutine = GameApp.Instance.StartCoroutine(routine);
            _coroutineDic.Add(coroutine.GetHashCode(), coroutine);
            return coroutine;
        }
        /// <summary>
        /// 停止协程
        /// </summary>
        /// <param name="coroutine"></param>
        protected void StopCoroutine(Coroutine coroutine)
        {
            GameApp.Instance.StopCoroutine(coroutine);
            int hashCode = coroutine.GetHashCode();
            if (_coroutineDic.ContainsKey(hashCode))
            {
                _coroutineDic.Remove(hashCode);
            }
        }
        /// <summary>
        /// 停止协程
        /// </summary>
        protected void StopAllCoroutines()
        {
            foreach (var item in _coroutineDic)
            {
                GameApp.Instance.StopCoroutine(item.Value);
            }
            _coroutineDic.Clear();
        }
        /// <summary>
        /// 添加Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddSubItem<T>() where T : UIItem, new()
        {
            return AddSubItem<T>(this.transform);
        }

        /// <summary>
        /// 添加Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent">默认父亲</param>
        /// <returns></returns>
        public T AddSubItem<T>(Transform parent) where T : UIItem, new()
        {
            T t = UIView.CreateView<T>(parent);
            t._uiView = this;
            BindingElement(t);
            return t;
        }

        /// <summary>
        /// 获取一个Item
        /// 返回第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSubItem<T>() where T : UIItem, new()
        {
            T res = UIElementDic.Values.FirstOrDefault(a => a is T&&a.gameObject.activeSelf==false) as T;
            return res;
        }
        /// <summary>
        /// 获取所有类型的Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllSubItem<T>() where T : UIItem, new()
        {
            List<T> temp = new List<T>();
            List<UIElement> elements = UIElementDic.Values.Where(a => a is T).ToList();
            foreach (var item in elements)
            {
                T res = item as T;
                temp.Add(res);
            }
            return temp;
        }


        #endregion

        #region override


        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitializeElement()
        {

        }

        /// <summary>
        /// UI创建
        /// </summary>
        protected virtual void OnCreate()
        {

        }

        internal override void Internal_OnDestroy()
        {
            List<int> keys = UIElementDic.Keys.ToList();
            foreach (var item in keys)
            {
                if (UIElementDic.ContainsKey(item))
                {
                    UIElementDic[item].Internal_OnDestroy();
                }
            }
            this.OnDestroy();
            UIElementDic.Clear();
        }
        internal override void Internal_OnEnable()
        {
            foreach (var item in UIElementDic)
            {
                //处于激活状态才会相应事件
                if (item.Value.active)
                {
                    item.Value.Internal_OnEnable();
                }
            }
            this.OnEnable();
        }
        internal override void Internal_OnDisable()
        {
            foreach (var item in UIElementDic)
            {
                //处于激活状态才会相应事件
                if (item.Value.active)
                {
                    item.Value.Internal_OnDisable();
                }
            }
            this.OnDisable();
        }

        #endregion


        #region Static

        /// <summary>
        /// 创建一个View
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public static T1 CreateView<T1>(Transform parent = null) where T1 : UIView, new()
        {
            T1 t = new T1();
            if (parent == null)
            {
                if (t is UIPanel uIPanel)
                {
                    parent = UIModule.Instance.GetParent(uIPanel.LayerEnum);
                }
            }
            t.Initialize(parent);
            t.InitializeElement();
            t.OnCreate();
            if (t.gameObject != null)
            {
                t.SetAsLastSibling();
                t.OnEnable();
            }
            return t;
        }


        #endregion


        #region 内部函数
        protected void NullAssert(UnityEngine.Object obj, string name)
        {
            if (obj == null)
            {
                throw new Exception($"组件名:{name}没有找到");
                //Logger.LogError($"组件:{name}上的组件获取失败,请检查");
            }
        }

        ///// <summary>
        ///// 绑定组件到View
        ///// </summary>
        ///// <param name="behaviour"></param>
        ///// <param name="element"></param>
        //protected void BindingElement<T>(UnityEngine.Component behaviour, out T element)
        //    where T : UIElement
        //{
        //    element = GameTool.CreateUIComponent<T>(this, behaviour);
        //    if (element == null)
        //    {
        //        Logger.LogError(behaviour.GetType().Name + "没有找到对应的类型");
        //    }
        //    this.UIElementDic.Add(element.InstanceId, element);
        //}
        /// <summary>
        /// 绑定组件到View
        /// </summary>
        /// <param name="element"></param>
        internal void BindingElement(UIElement element)
        {
            this.UIElementDic.Add(element.InstanceId, element);
        }
        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <param name="uIElement"></param>
        internal void RemoveElement(UIElement uIElement)
        {
            if (UIElementDic.ContainsKey(uIElement.InstanceId))
            {
                UIElementDic.Remove(uIElement.InstanceId);
            }
        }

        private void Initialize(Transform parent)
        {
            GameObject obj = ResourcesModule.Instance.Load<GameObject>(UIPath);
            if (obj == null)
            {
                Debug.LogError($"err path {UIPath}");
            }
            _gameObject = GameObject.Instantiate(obj, parent);
            _rectTransform = _gameObject.GetComponent<RectTransform>();
        }

        #endregion
    }
}
