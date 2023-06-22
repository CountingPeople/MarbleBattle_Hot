
using System;

namespace Framework
{
    public interface IState
    {

        void SetNextEvent<T>()where T: LoadEventBase;
        /// <summary>
        /// 进入
        /// </summary>
        void OnEnter();
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deltaTime"></param>
        void OnUpdate(float deltaTime);
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="ispause">是否是暂停</param>
        void OnPause(bool ispause);
        /// <summary>
        /// 离开
        /// </summary>
        void OnLeave();
    }
}
