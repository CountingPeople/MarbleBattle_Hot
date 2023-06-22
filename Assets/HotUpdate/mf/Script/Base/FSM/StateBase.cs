using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    /// <summary>
    /// 通过状态基类
    /// </summary>
    public abstract class StateBase : IState
    {
        private float timeOfEnter;

        public ILoadEvent _loadEvent;


        public StateBase() { }


        public float activeTime
        {
            get { return (timeOfEnter == 0 ? 0 : getCurrentTime() - timeOfEnter); }
        }

        private float getCurrentTime()
        {
            return GameApp.Instance.gameTime;
        }
        /// <summary>
        /// 进入
        /// </summary>
        public virtual void OnEnter()
        {
            this.timeOfEnter = getCurrentTime();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void OnUpdate(float deltaTime)
        {

        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="ispause">是否是暂停</param>
        public virtual void OnPause(bool ispause)
        {

        }
        /// <summary>
        /// 离开
        /// </summary>
        public virtual void OnLeave()
        {
            this.timeOfEnter = 0f;
        }

        public void SetNextEvent<T>() where T : LoadEventBase
        {
            _loadEvent= Activator.CreateInstance<T>();
        }
    }
}
