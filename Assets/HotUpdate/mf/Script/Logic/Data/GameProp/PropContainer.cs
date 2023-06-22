using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PropContainer 
{
    Dictionary<GamePropEnum, PropDto> propMap = new Dictionary<GamePropEnum, PropDto>();

    public void ResetValue()
    {
        propMap.Values.ToList().ForEach(value => value.ResetValue());
    }

    public PropDto GetProp(GamePropEnum propId)
    {
        PropDto temp = null;
        if (propMap.TryGetValue(propId,out temp))
        {
            temp = propMap[propId];
            return temp;
        }
        temp = new PropDto(this,propId);
        propMap[propId] = temp;
        return temp;
    }

    public void SetRawValue(GamePropEnum propId, float rawValue)
    {
        var temp = GetProp(propId);
        temp.SetRawValue(rawValue);
    }

    public void SetValue(GamePropEnum propId, float rawValue)
    {
        var temp = GetProp(propId);
        temp.SetValue(rawValue);
    }

    public void SetRawValueByAdd(GamePropEnum propId,int addRawValue)
    {
        var temp = GetProp(propId);
        temp.SetRawValue(temp.GetRawValue() + addRawValue);
    }

    public void AddRawValue(PropContainer container)
    {
        foreach (var item in container.propMap)
        {
            GetProp(item.Key).AddRawValue(item.Value);
        }
    }

    public void AddValue(PropContainer container)
    {
        foreach (var item in container.propMap)
        {
            GetProp(item.Key).AddValue(item.Value);
        }
    }

    public void SubValue(PropContainer container)
    {
        foreach (var item in container.propMap)
        {
            GetProp(item.Key).SubValue(item.Value);
        }
    }


    public void SubRawValue(PropContainer container)
    {
        foreach (var item in container.propMap)
        {
            GetProp(item.Key).SubRawValue(item.Value);
        }
    }

    public void SetSynState(bool state)
    {
        foreach (var item in propMap)
        {
            GetProp(item.Key).SetSynState(state);
        }
    }

    public void Publish()
    {
        foreach (var item in propMap)
        {
            GetProp(item.Key).Publish();
        }
    }


    public float GetRawValue(GamePropEnum propId)
    {
        var temp = GetProp(propId);
        return temp.GetRawValue();
    }

    public float GetValue(GamePropEnum propId)
    {
        var temp = GetProp(propId);
        return temp.GetValue();
    }

    public void Reset()
    {
        foreach (var item in propMap)
        {
            item.Value.SetRawValue(0);
        }
    }

    public PropContainer Clone()
    {
        var container = new PropContainer();
        foreach (var item in propMap)
        {
            container.propMap.Add(item.Key, item.Value.Clone(container));
        }
        return container;
    }

    public Dictionary<GamePropEnum, PropDto> GetAllProps()
    {
        return propMap;
    }

    public Dictionary<GamePropEnum, float> GetAllPropsValue()
    {
        var temp = new Dictionary<GamePropEnum, float>();
        foreach (var item in propMap)
        {
            temp.Add(item.Key, item.Value.GetRawValue());
        }
        return temp;
    }

    public Dictionary<GamePropEnum, float> GetAllPropsValueCur()
    {
        var temp = new Dictionary<GamePropEnum, float>();
        foreach (var item in propMap)
        {
            temp.Add(item.Key, item.Value.GetValue());
        }
        return temp;
    }


    public void LogProps()
    {
        string str = "";
        foreach (var item in propMap)
        {
            str += $"{item.Value.GetName()} {item.Value.GetRawValue()} \n";
        }
        Debug.Log(str);
    }
}
