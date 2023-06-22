using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LoadEventBase: ILoadEvent
{
    public virtual void Init()
    { 
    
    }

    public virtual bool IsCanContinue()
    {
        return true;
    }

    public virtual void OnComplete()
    {

    }

    public virtual void OnProgress(float value)
    { 
    
    }

    public virtual float WaitTime()
    {
        return 0.01f;
    }
    
}
