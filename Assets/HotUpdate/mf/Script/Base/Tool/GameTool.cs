using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Framework
{
    internal static class GameTool
    {
        private const int FALSE = 0;
        private const int TRUE = 1;
        private static int _valueLock = 0;
        private static int _instanceId = 0;

        private static Queue<int> _instanceCacheQueue = null;
        static GameTool()
        {
            _instanceId = int.MinValue;
            _instanceCacheQueue = new Queue<int>();
        }

        /// <summary>
        /// 获取实例ID
        /// </summary>
        /// <returns></returns>
        internal static int GetInstanceId()
        {
            Begin: if (Interlocked.CompareExchange(ref _valueLock, TRUE, FALSE) == FALSE)
            {

                int result = int.MinValue;
                if (_instanceCacheQueue.Count < 1)
                {
                    result = _instanceId;
                    _instanceId++;
                }
                else
                {
                    result = _instanceCacheQueue.Dequeue();
                }
                Interlocked.Exchange(ref _valueLock, FALSE);
                return result;
            }
            else
            {
                Thread.Sleep(10);
                goto Begin;
            }
        }

        /// <summary>
        /// 回收实例ID
        /// </summary>
        /// <returns></returns>
        internal static void RecoverInstanceId(int instanceId)
        {
            Begin: if (Interlocked.CompareExchange(ref _valueLock, TRUE, FALSE) == FALSE)
            {
                if (!_instanceCacheQueue.Contains(instanceId))
                    _instanceCacheQueue.Enqueue(instanceId);
                Interlocked.Exchange(ref _valueLock, FALSE);
            }
            else
            {
                Thread.Sleep(10);
                goto Begin;
            }
        }

        /// <summary>
        /// 根据类型获取一个UIEvent
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        internal static UIEventBase GetUIEventBase(GameObject obj, EventTriggerType triggerType)
        {
            UIEventBase uIEventBase = null;
            switch (triggerType)
            {
                case EventTriggerType.PointerEnter:
                    uIEventBase = obj.AddComponent<UIPointerEnterEvent>();
                    break;
                case EventTriggerType.PointerExit:
                    uIEventBase = obj.AddComponent<UIPointerExitEvent>();
                    break;
                case EventTriggerType.PointerDown:
                    uIEventBase = obj.AddComponent<UIPointerDownEvent>();
                    break;
                case EventTriggerType.PointerUp:
                    uIEventBase = obj.AddComponent<UIPointerUpEvent>();
                    break;
                case EventTriggerType.PointerClick:
                    uIEventBase = obj.AddComponent<UIPointerClickEvent>();
                    break;
                case EventTriggerType.Drag:
                    uIEventBase = obj.AddComponent<UIDragEvent>();
                    break;
                case EventTriggerType.BeginDrag:
                    uIEventBase = obj.AddComponent<UIBeginDragEvent>();
                    break;
                case EventTriggerType.EndDrag:
                    uIEventBase = obj.AddComponent<UIEndDragEvent>();
                    break;
                case EventTriggerType.Submit:
                    uIEventBase = obj.AddComponent<UISubmitEvent>();
                    break;
                case EventTriggerType.Cancel:
                    uIEventBase = obj.AddComponent<UICancelEvent>();
                    break;
                case EventTriggerType.Drop:
                    uIEventBase = obj.AddComponent<UIDropEvent>();
                    break;
                case EventTriggerType.Scroll:
                    uIEventBase = obj.AddComponent<UIScrollEvent>();
                    break;
                case EventTriggerType.UpdateSelected:
                    uIEventBase = obj.AddComponent<UIUpdateSelectedEvent>();
                    break;
                case EventTriggerType.Select:
                    uIEventBase = obj.AddComponent<UISelectEvent>();
                    break;
                case EventTriggerType.Deselect:
                    uIEventBase = obj.AddComponent<UIDeselectEvent>();
                    break;
                case EventTriggerType.Move:
                    uIEventBase = obj.AddComponent<UIMoveEvent>();
                    break;
                case EventTriggerType.InitializePotentialDrag:
                    uIEventBase = obj.AddComponent<UIInitializePotentialDragEvent>();
                    break;
            }
            return uIEventBase;
        }

        public static void ShowTip(string str) 
        {
            //var panel = UIModule.Instance.ShowPanel<UIFlowTipView>();
            //panel.SetContent(str);
        }
    }
}
