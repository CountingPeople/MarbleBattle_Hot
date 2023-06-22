using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDto 
{
    PropContainer container;
    GamePropEnum propId;
    float rawValue;
    PropTable propTable;
    private BindableProperty<float> valueBind = new BindableProperty<float>();

    public PropDto(PropContainer container, GamePropEnum propId)
    {
        this.propId = propId;
        this.container = container;
        propTable = PropTable.Instance.GetData((int)propId);
    }

    public void ResetValue()
    {
        valueBind.Value = rawValue;
    }

    public void SetRawValue(float value)
    {
        rawValue = value;
        valueBind.Value = rawValue;
    }
    public float GetRawValue()
    {
        return rawValue;
    }

    public void AddRawValue(PropDto propDto)
    {
        rawValue = rawValue + propDto.GetRawValue();
        valueBind.Value = rawValue;
    }

    public void SubRawValue(PropDto propDto)
    {
        rawValue = rawValue - propDto.GetRawValue();
        valueBind.Value = rawValue;
    }

    public void SetSynState(bool state)
    {
        valueBind.IsEnable = state;
    }

    public void Publish()
    {
        valueBind.Publish();
    }

    public float GetValue()
    {
        return valueBind.Value;
    }

    public void SetValue(float value)
    {
        valueBind.Value = value;
    }

    public void AddValue(PropDto propDto)
    {
        valueBind.Value = valueBind.Value + propDto.GetValue();
    }
    public void SubValue(PropDto propDto)
    {
        valueBind.Value = valueBind.Value - propDto.GetValue();
    }

    public BindableProperty<float> GetBind()
    {
        return valueBind;
    }

    public GamePropEnum GetPropId()
    {
        return propId;
    }

    public string GetName()
    {
        return propTable.name;
    }

    public string GetIcon()
    {
        return propTable.icon;
    }

    public string GetShowValue()
    {
        string str = propTable.isRate ? rawValue + "%" : rawValue.ToString();
        return str;
    }


    public float GetCalShow()
    {
        float value = 0;
        foreach (var item in container.GetAllPropsValueCur())
        {
            value += item.Value;
        }
        return value;
    }

    public PropDto Clone(PropContainer container)
    {
        PropDto temp = null;
        if (container != null)
        {
            temp = new PropDto(container,propId);
        }
        else 
        {
            Debug.Log("容器为空无法克隆");
        }
        return temp;
    }

}
