using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventModule : BaseModule<EventModule>
{
    private Dictionary<EventEnum, List<Action>> cacheEvent = new Dictionary<EventEnum, List<Action>>();


    public void OnEvent(EventEnum eventEnum)
    {
        if (cacheEvent.ContainsKey(eventEnum))
        {
            foreach (var item in cacheEvent[eventEnum])
            {
                item.Invoke();
            }
        }
    }

    public void AddListener(EventEnum eventEnum,Action callBack)
    {
        if (!cacheEvent.ContainsKey(eventEnum))
        {
            cacheEvent[eventEnum] = new List<Action>();
        }
        cacheEvent[eventEnum].Add(callBack);
    }

    public void RemoveListener(EventEnum eventEnum, Action callBack)
    {
        if (cacheEvent.ContainsKey(eventEnum))
        {
            cacheEvent[eventEnum].Remove(callBack);
        }
    }
}
