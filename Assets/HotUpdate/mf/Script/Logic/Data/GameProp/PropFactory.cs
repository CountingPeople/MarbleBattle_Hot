using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory 
{
    Dictionary<int, IPropAdapter> adapterMapping = new Dictionary<int, IPropAdapter>();
    public void Init()
    {
        Type adapterType = typeof(IPropAdapter);
        Type[] types = adapterType.Assembly.GetTypes();
        foreach (var item in types)
        {
            IPropAdapter adapter = Activator.CreateInstance(item) as IPropAdapter;
            adapterMapping.Add(adapter.GetPropId(), adapter);
        }
    }

    public IPropAdapter GetAdapter(int propId)
    {
        return adapterMapping[propId];
    }

    public string GetPropName(int propId)
    {
        return GetAdapter(propId).GetName();
    }
}
