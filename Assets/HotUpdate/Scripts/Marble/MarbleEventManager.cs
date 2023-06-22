using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MarbleEventManager
{
    public static UnityEvent<BrickController> OnBrickDestory = new UnityEvent<BrickController>();
    public static UnityEvent<float> OnHPBrickDestory = new UnityEvent<float>();

    public static UnityEvent OnMarbleRevive = new UnityEvent();
    public static UnityEvent RequestMarbleStartRevive = new UnityEvent();

    public static UnityEvent OnMarbleHitBorder = new UnityEvent();
}
