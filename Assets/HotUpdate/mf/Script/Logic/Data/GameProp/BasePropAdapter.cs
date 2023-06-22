using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropAdapter
{
    string GetName();
    string GetIconPath();
    bool IsPercent();
    int GetPropId();

    int OnReadValue(PropContainer propContainer,int rawValue);
}

public class BasePropAdapter : IPropAdapter
{
    public virtual string GetIconPath()
    {
        throw new System.NotImplementedException();
    }

    public virtual string GetName()
    {
        throw new System.NotImplementedException();
    }

    public virtual int GetPropId()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsPercent()
    {
        throw new System.NotImplementedException();
    }

    public virtual int OnReadValue(PropContainer propContainer, int rawValue)
    {
        throw new System.NotImplementedException();
    }
}
