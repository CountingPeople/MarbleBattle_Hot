
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 消息分发模块(全局)
/// </summary>
internal sealed class MessageModule : BaseModule<MessageModule>
{
    private Dictionary<GameEventEnum, List<Action<object>>> _eventDic = null;

    public override void Init()
    {
        _eventDic = new Dictionary<GameEventEnum, List<Action<object>>>();
    }

    internal MessageModule AddEventListener(GameEventEnum key, Action<object> act)
    {
        if (!_eventDic.ContainsKey(key))
        {
            _eventDic.Add(key, new List<Action<object>>());
        }
        _eventDic[key].Add(act);
        return this;
    }

    internal MessageModule RemoveEventListener(GameEventEnum key, Action<object> act = null)
    {
        if (_eventDic.ContainsKey(key))
        {
            if (_eventDic[key].Contains(act))
            {
                if (act != null)
                {
                    _eventDic[key].Remove(act);

                }
                else
                {
                    _eventDic[key].Clear();
                }
            }
        }
        return this;
    }
    /// <summary>
    /// 执行事件
    /// 同时会执行游戏内事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    internal MessageModule OnEvent(GameEventEnum key, object obj = null)
    {
        if (_eventDic.ContainsKey(key))
        {
            _eventDic[key].ForEach(delegate (Action<object> x)
            {
                x(obj);
            });
        }
        return this;
    }

    public override void Freed()
    {
        _eventDic.Clear();
    }
}
