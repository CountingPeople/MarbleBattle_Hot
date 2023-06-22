using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;
using Object = UnityEngine.Object;

namespace Framework
{
    public sealed class UIModule : BaseModule<UIModule>
    {
        private Canvas _canvas = null;
        private GameObject _eventSystem = null;
        public EventSystem CurSystem { get; private set; }
        private GameObject _mainObj = null;
        public GameObject MainUIObj => _mainObj;
        public Canvas MainCanvas => _canvas;
        private Dictionary<UILayerEnum, RectTransform> _tranDic = null;

        private List<UIPanel> _panelList = null;

        //private UIPanel _focusPanel = null;

        public override void Init()
        {
            base.Init();
            CreateMainObj();
            CreateLayerObj();
            _panelList = new List<UIPanel>();
        }
        private void CreateMainObj()
        {
            //var rootPer = Resources.Load<GameObject>("Prefab/UIRoot");
            var rootPer = ResourcesModule.Instance.Load<GameObject>("Assets/Bundles/Prefab/UIRoot.prefab");
            var rootTran = GameObject.Instantiate<GameObject>(rootPer, this.transform).transform;

            //rootTran.Find("DynamicCanvas").GetComponent<Canvas>().worldCamera = GameObject.Find("UICam").GetComponent<Camera>();
            //rootTran.Find("StaticCanvas").GetComponent<Canvas>().worldCamera = GameObject.Find("UICam").GetComponent<Camera>();

            //_mainObj = new GameObject("MainUI");
            //_mainObj.transform.SetParent(rootTran.Find("StaticCanvas"));


            _mainObj = new GameObject("MainUI", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            _mainObj.transform.SetParent(this.transform);
            _canvas = _mainObj.GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;


            CanvasScaler canvasScaler = _mainObj.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(768, 1334);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            canvasScaler.matchWidthOrHeight = 1;


            _tranDic = new Dictionary<UILayerEnum, RectTransform>();
            _eventSystem = new GameObject("System", typeof(EventSystem), typeof(StandaloneInputModule));
            _eventSystem.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
            _eventSystem.transform.SetParent(this.transform);
            CurSystem = _eventSystem.GetComponent<EventSystem>();
        }
        private void CreateLayerObj()
        {
            Type enumType = typeof(UILayerEnum);
            string[] strs = Enum.GetNames(enumType);
            foreach (var item in strs)
            {
                UILayerEnum uILayerEnum = (UILayerEnum)Enum.Parse(enumType, item);
                GameObject obj = new GameObject(uILayerEnum.ToString(), typeof(RectTransform));
                obj.transform.SetParent(MainUIObj.transform);
                RectTransform rectTransform = obj.GetComponent<RectTransform>();

                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                //if (GameSetting.IsNotchScreen)
                //{
                //    float width = Screen.width;
                //    float height = Screen.height;
                //    float offsetY = (height - GameSetting.SafeArea.y) / 2f;
                //    float offsetX = (width - GameSetting.SafeArea.x) / 2f;
                //    rectTransform.offsetMax = new Vector2(offsetX * -1, offsetY * -1);
                //    rectTransform.offsetMin = new Vector2(offsetX, offsetY);
                //}
                //else
                //{
                //    rectTransform.offsetMax = Vector2.zero;
                //    rectTransform.offsetMin = Vector2.zero;
                //}
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.SetSiblingIndex((int)uILayerEnum);
                _tranDic.Add(uILayerEnum, rectTransform);
            }
        }
        public T CreatePanel<T>() where T : UIPanel, new()
        {
            T t = UIView.CreateView<T>();
            if (t.gameObject == null)
            {
                return null;
            }
            t.transform.SetParent(_tranDic[t.LayerEnum]);
            _panelList.Add(t);
            return t;
        }
        public T ShowPanel<T>() where T : UIPanel, new()
        {
            UIPanel panel = null;
            foreach (var item in _panelList)
            {
                if (item is T)
                {
                    panel = item;
                    break;
                }
            }
            if (panel == null)
            {
                panel = CreatePanel<T>();
                panel.active = true;
            }
            else
            {
                panel.active = true;
                panel.SetAsLastSibling();
            }
            return panel as T;
        }

        public T GetPanel<T>() where T : UIPanel, new()
        {
            UIPanel panel = null;
            foreach (var item in _panelList)
            {
                if (item is T)
                {
                    panel = item;
                    break;
                }
            }
            if (panel == null)
            {
                panel = CreatePanel<T>();
            }
            return panel as T;
        }
        public void HidePanel(UIPanel uIPanel)
        {
            uIPanel.active = false;
        }

        public void HidePanel<T>() where T : UIPanel, new()
        {
            foreach (var item in _panelList)
            {
                if (item is T)
                {
                    if (item != null)
                    {
                        item.active = false;
                    }
                }
            }
        }

        public void HidePanelByLayer(UILayerEnum layerEnum)
        {
            foreach (var item in _panelList)
            {
                if ((item.LayerEnum & layerEnum) > 0)
                {
                    if (item != null)
                    {
                        item.active = false;
                    }
                }
            }
        }

        public void HideAllPanel()
        {
            foreach (var item in _panelList)
            {
                item.active = false;
            }
        }
        public void ClosePanel(UIPanel uIPanel)
        {
            if (uIPanel != null)
            {
                _panelList.Remove(uIPanel);
                if (uIPanel.gameObject != null)
                {
                    UIElement.Destroy(uIPanel);
                }
            }
        }
        public void ClosePanel<T>() where T : UIPanel, new()
        {
            List<UIPanel> tempList = new List<UIPanel>();
            foreach (var item in _panelList)
            {
                if (item is T || item == null)
                {
                    tempList.Add(item);
                }
            }
            foreach (var item in tempList)
            {
                _panelList.Remove(item);
                if (item.gameObject != null)
                {
                    UIElement.Destroy(item);
                }
            }
        }

        public void ClosePanelByLayer(UILayerEnum layerEnum)
        {
            List<UIPanel> tempList = new List<UIPanel>();
            foreach (var item in _panelList)
            {
                if ((item.LayerEnum & layerEnum) > 0)
                {
                    tempList.Add(item);
                }
            }
            foreach (var item in tempList)
            {
                _panelList.Remove(item);
                if (item.gameObject != null)
                {
                    UIElement.Destroy(item);
                }
            }
        }
        public void CloseAllPanel()
        {
            foreach (var item in _panelList)
            { 
                if (item != null && item.gameObject != null)
                {
                    UIElement.Destroy(item);
                }
            }
            _panelList.Clear();
        }

        public Transform GetParent(UILayerEnum uILayerEnum)
        {
            if (_tranDic.ContainsKey(uILayerEnum))
            {
                return _tranDic[uILayerEnum];
            }
            else
            {
                return null;
            }
        }

        public override void Freed()
        {
            foreach (var item in _panelList)
            {
                if (item != null)
                {
                    UIElement.Destroy(item);
                }
            }

            _panelList.Clear();
            Object.Destroy(this.gameObject);
        }
        /// <summary>
        /// 判断某个类型的Panel是否已经被打开
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsOpen<T>() where T : UIPanel, new()
        {
            UIPanel panel = null;
            foreach (var item in _panelList)
            {
                if (item is T)
                {
                    panel = item;
                    break;
                }
            }
            if (panel == null)
            {
                return false;
            }
            return panel.active;
        }

        public List<T> GetAllPanel<T>() where T : UIPanel, new()
        {
            List<T> tempList = new List<T>();
            foreach (var item in _panelList)
            {
                if (item is T || item == null)
                {
                    tempList.Add(item as T);
                }
            }
            return tempList;
        }

        public List<UIPanel> GetAllPanel()
        {
            return _panelList.ToList();
        }
    }
}
