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
    /// Ϊ����Ϊname��ʱ����ʱ���ӵ����¼�
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
    /// �Ƴ�����Ϊname��ʱ����ʱ�ĵ����¼�
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
    /// �����±��Ƴ�����Ϊname��ʱ����ʱ�ĵ����¼�
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
    /// �Ƴ�����Ϊname�����һ�������¼�
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
    /// ��ʱ�����¼�
    /// </summary>
    /// <param name="t">��ʱʱ��</param>
    /// <param name="d">�����¼�</param>
    /// <param name="name">ʱ����ʱ�����֣����˿��ַ��������֣��������ֶ���Ψһ�ģ�</param>
    /// <param name="group">Ϊ���ʱ���ӳٷ���һ���飬�����һ�������ɾ���¼�</param>
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
            Debug.LogError("------����ͬ���ֵ�ʱ���ӳ�,�ӳٵ���ʧ�ܡ�");
        }

    }

    /// <summary>
    /// ʱ���ӳ�����
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
    /// ������Ϊname��ʱ���ӳٽ�����ͣ
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
    /// ȡ������Ϊname���ӳ�
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
    /// ȡ��һ��ʱ���ӳ�
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
    /// ȡ������ʱ���ӳ�
    /// </summary>
    public void CancelAll()
    {
        timeList.Clear();
    }
    /// <summary>
    /// �Ƿ�������Ϊname��ʱ���ӳ�
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
    /// �Ƿ�������Ϊgroup��ʱ���ӳ�
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
    /// �������ֻ�ȡʱ���ӳ�����
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
    /// ���ص�ǰ�ӳ�ʱ����е���������������ʱ
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
    /// ���ص�ǰ�ӳ�ʱ��ʣ��ʱ�������������������ʱ
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
