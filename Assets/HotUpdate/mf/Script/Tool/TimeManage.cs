using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UpdateEvent {
    public string name;
    public Action callBack;
 
}


public class TimeManage : MonoSingleton<TimeManage>
{

    public delegate void TimeDel();
    public Action TimeArrive;
    public List<Mytime> timeList = new List<Mytime>();

    public List<UpdateEvent> upDateEventList = new List<UpdateEvent>();

    // Use this for initialization

    // Update is called once per frame

    private void FixedUpdate()
    {
        for (int i = 0; i < timeList.Count; i++)
        {

            var item = timeList[i];

            if (!item.pause)
            {

                item.curTime += Time.fixedDeltaTime; ;
            }
            if (item.curTime >= item.delay)
            {

                timeList.Remove(item);
                if (item.OnArrival != null)
                {
                    item.OnArrival();

                }
            }
        }

        for (int i = 0; i < upDateEventList.Count; i++)
        {
            var item = upDateEventList[i];
            if (item.callBack != null)
            {
                item.callBack();
            }
            else 
            {
                upDateEventList.RemoveAt(i);
            }
        }
    }

    public void AddUpDateEvent(string name,Action callBack)
    {
        var temp = new UpdateEvent()
        {
            name = name,
            callBack = callBack,
        };
        upDateEventList.Add(temp);
    }

    public void RemoveUpDateEvent(string name)
    {
       var temp= upDateEventList.Find(item => item.name == name);
        upDateEventList.Remove(temp);
    }


    /// <summary>
    /// 为名字为name的时间延时增加调用事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="del"></param>
    public void AddEvent(string name, Action del)
    {
        Mytime t = GetTimeByName(name);
        if (t != null)
        {
            t.OnArrival += del;
        }
    }
    /// <summary>
    /// 移除名字为name的时间延时的调用事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="del"></param>
    public void RemoveEvent(string name, Action del)
    {
        Mytime t = GetTimeByName(name);
        if (t != null)
        {
            t.OnArrival -= del;
        }
    }
    /// <summary>
    /// 根据下标移除名字为name的时间延时的调用事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="index"></param>
    public void RemoveEvent(string name, int index)
    {
        Mytime t = GetTimeByName(name);
        if (index < t.OnArrival.GetInvocationList().Length)
            t.OnArrival -= (Action)t.OnArrival.GetInvocationList()[index];

    }

    /// <summary>
    /// 移除名字为name的最后一个调用事件
    /// </summary>
    /// <param name="name"></param>
    public void RemoveLastEvent(string name)
    {
        Mytime t = GetTimeByName(name);
        int index = t.OnArrival.GetInvocationList().Length - 1;
        if (index >= 0)
            t.OnArrival -= (Action)t.OnArrival.GetInvocationList()[index];

    }
    /// <summary>
    /// 延时调用事件
    /// </summary>
    /// <param name="t">延时时间</param>
    /// <param name="d">调用事件</param>
    /// <param name="name">时间延时的名字（除了空字符串的名字，其他名字都是唯一的）</param>
    /// <param name="group">为这个时间延迟分配一个组，方便对一个组进行删除事件</param>
    public void DelayInvoke(float t, Action d, string name = "", string group = "")
    {
        if (!Contains(name) || string.IsNullOrEmpty(name))
        {
            Mytime mytime = new Mytime();
            mytime.originTime = Time.realtimeSinceStartup;
            mytime.delay = t;
            mytime.OnArrival = d;
            mytime.name = name;
            mytime.groupName = group;
            timeList.Add(mytime);
        }
        else
        {
            Debug.LogError("------有相同名字的时间延迟,延迟调用失败。");
        }

    }

    /// <summary>
    /// 时间延迟数据
    /// </summary>
    public class Mytime
    {
        public string name;
        public float delay;
        public float originTime;
        public Action OnArrival;
        public string groupName;
        public float curTime;
        public bool pause = false;
        public int GetCurrentInt()
        {

            return (int)Mathf.Floor(curTime);

        }
    }
    /// <summary>
    /// 对名字为name的时间延迟进行暂停
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isPause"></param>
    public void SetPause(string name, bool isPause)
    {
        Mytime t = GetTimeByName(name);
        if (t != null)
        {
            t.pause = isPause;
        }
    }
    /// <summary>
    /// 取消名字为name的延迟
    /// </summary>
    /// <param name="name"></param>
    public void Cancel(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].name.Equals(name))
            {
                timeList.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 取消一组时间延迟
    /// </summary>
    /// <param name="group"></param>
    public void CancelGroup(string group)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].groupName.Equals(group))
            {
                timeList.RemoveAt(i);
                i--;
            }
        }
    }
    /// <summary>
    /// 取消所有时间延迟
    /// </summary>
    public void CancelAll()
    {
        timeList.Clear();
    }
    /// <summary>
    /// 是否有名字为name的时间延迟
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Contains(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].name.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 是否有组名为group的时间延迟
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool ContainsGroup(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].groupName.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 根据名字获取时间延迟数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Mytime GetTimeByName(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (name.Equals(timeList[i].name))
            {
                return timeList[i];
            }

        }
        return null;
    }
    /// <summary>
    /// 返回当前延迟时间进行的整数，用来做计时
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetCurrenInt(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].name.Equals(name))
            {
                return Mathf.FloorToInt(timeList[i].curTime + 0.021f);
            }
        }

        return -1;
    }
    /// <summary>
    /// 返回当前延迟时间剩余时间的整数，用来做倒计时
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetCurrenIntReverse(string name)
    {
        for (int i = 0; i < timeList.Count; i++)
        {
            if (timeList[i].name.Equals(name))
            {

                return (Mathf.CeilToInt(timeList[i].delay - 0.021f - timeList[i].curTime));
            }
        }
        return -1;
    }
}
