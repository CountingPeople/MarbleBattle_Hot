using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnLevelAdvanced = new UnityEvent();
    public static UnityEvent OnLevelFinished = new UnityEvent();

    public static UnityEvent OnGameOver = new UnityEvent();
}
