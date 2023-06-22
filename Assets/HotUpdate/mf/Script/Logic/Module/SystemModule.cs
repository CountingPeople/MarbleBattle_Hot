using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

internal sealed class SystemModule : BaseModule<SystemModule>
{
    private const string GameAssemblyFullName = "Assembly-CSharp";
    private List<Assembly> assemblieList;
    private Type moduleType;

    private List<ISystem> systems = new List<ISystem>();

    public override void Init()
    {
        base.Init();
        moduleType = typeof(ISystem);
        assemblieList = new List<Assembly>();
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly item in assemblies)
        {
            if (item.GetName().Name == GameAssemblyFullName)
            {
                assemblieList.Add(item);
                break;
            }
        }
    }

    public void InitGame()
    {
        foreach (var assembly in assemblieList)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type item in types)
            {
                if (!item.IsAbstract && moduleType.IsAssignableFrom(item))
                {
                    ISystem sys = Activator.CreateInstance(item) as ISystem;
                    sys.InitGame();
                    systems.Add(sys);
                }
            }
        }
    }

    public void EnterGame()
    {
        foreach (var item in systems)
        {
            item.EnterGame();
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        foreach (var item in systems)
        {
            item.UpdateGame();
        }
    }

    public override void LateUpDate(float deltaTime)
    {
        base.LateUpDate(deltaTime);
        foreach (var item in systems)
        {
            item.LateUpdateGame();
        }
    }

    public override void Freed()
    {
        base.Freed();
        LeaveGame();
    }

    public void LeaveGame()
    {
        foreach (var item in systems)
        {
            item.LeaveGame();
        }
    }
}
