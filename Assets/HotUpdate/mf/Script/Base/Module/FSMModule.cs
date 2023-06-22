using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// 状态机模块
    /// </summary>
    public sealed class FSMModule : BaseModule<FSMModule>
    {
        //private IState _currentState;
        private Stack<IState> _stateStack;
        private Dictionary<string, IState> _stateDic;

        /// <summary>
        /// 当前全局状态
        /// </summary>
        public IState CurrentState { get { return getStackState(); } }

        public override void Init()
        {
            _stateStack = new Stack<IState>();
            _stateDic = new Dictionary<string, IState>();
        }


        /// <summary>
        /// 返回两个状态是否相等
        /// </summary>
        public static bool StateIsEquals(IState left, IState right)
        {
            return (null != left && null != right && left.Equals(right));
        }

        /// <summary>
        /// 获取当前状态
        /// </summary>
        private IState getStackState()
        {
            return _stateStack.Count > 0 ? _stateStack.Peek() : null;
        }

        /// <summary>
        /// 过渡状态事件
        /// </summary>
        /// <param name="oldState">旧状态</param>
        /// <param name="newState">新状态</param>
        private IState TransitionStateEvents(IState oldState, IState newState)
        {
            if (null != oldState)
            {
                oldState.OnPause(true);
            }

            if (null != newState)
            {
                newState.OnEnter();
            }

            return newState;
        }

        public IState GetState<T>()where T: IState
        {
            string stateName = (typeof(T)).ToString();
            if (_stateDic.ContainsKey(stateName))
            {
                return _stateDic[stateName];
            }
            var state = Activator.CreateInstance<T>();
            _stateDic.Add(stateName, state);
            return state;
        }

        public void TransitionState<T>() where T : IState
        {
            var state = GetState<T>();
            if (state==null)
            {
                return;
            }
            IState oldState = getStackState();
            if (state.Equals(oldState))
            {
                return;
            }

            if (null != oldState)
            {
                oldState.OnLeave();
            }


            if (null != state)
            {
                state.OnEnter();
            }

            _stateStack.Push(state);
        }

        /// <summary>
        /// 返回前一个状态,并调用OnPause(false)方法
        /// 从状态栈里弹出一个状态
        /// 该状态和SetState 的状态是平行的,互不影响
        /// </summary>
        /// <returns>弹出的状态</returns>
        public IState PopState()
        {
            if (_stateStack.Count == 0)
            {
                return null;
            }
            IState oldState = _stateStack.Pop();
            IState newState = getStackState();
            if (null != oldState)
            {
                oldState.OnLeave();
            }

            if (null != newState)
            {
                newState.OnPause(false);
            }
            return oldState;
        }

        /// <summary>
        /// 压入状态
        /// 往状态栈里压入一个状态
        /// 该状态和SetState 的状态是平行的,互不影响
        /// </summary>
        public void PushState(IState state)
        {
            if (null == state)
            {
                return;
            }
            IState oldState = getStackState();
            if (state.Equals(oldState))
            {
                return;
            }
            if (null != oldState)
            {
                oldState.OnPause(true);
            }
            if (null != state)
            {
                state.OnEnter();
            }
            _stateStack.Push(state);
        }

        /// <summary>
        /// 清除状态栈
        /// 递归退出
        /// </summary>
        /// <param name="callCurrentStateLeave">清除时,是否调用当前状态的Leave事件</param>
        private void StackClear()
        {
            if (_stateStack.Count == 0)
            {
                return;
            }
            IState oldState = _stateStack.Pop();
            IState newState = getStackState();
            do
            {
                oldState.OnLeave();
                newState.OnPause(false);
                oldState = newState;
                newState = getStackState();
            } while (newState != null);
            oldState.OnLeave();
            _stateStack.Clear();
        }

        /// <summary>
        /// 设置状态,直接切换
        /// 会递归退出已存在的状态栈
        /// </summary>
        /// <param name="newState">新状态</param>
        /// <param name="canTrasitionToSelf">能否由自己转换到自己</param>
        public void SetState(IState newState, bool canTrasitionToSelf = false)
        {
            IState oldState = getStackState();
            if (!canTrasitionToSelf && StateIsEquals(oldState, newState))
            {
                return;
            }
            StackClear();
            if (null != newState)
            {
                newState.OnEnter();
            }
            _stateStack.Push(newState);
        }
        public override void Update(float deltaTime)
        {
            IState curState = getStackState();
            if (null != curState)
            {
                curState.OnUpdate(deltaTime);
            }
        }

        public void SetNextEvent<T>() where T : LoadEventBase
        {
            var state = GetState<LoadState>();
            state.SetNextEvent<T>();
            TransitionState<LoadState>();
        }

    }
}
