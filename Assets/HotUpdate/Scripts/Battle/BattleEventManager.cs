using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class BattleEventManager
{
    public static UnityEvent<IBattleEntity> OnGroundAttacked = new UnityEvent<IBattleEntity>();

    public static UnityEvent<float> OnPlayerHPChanged = new UnityEvent<float>();
    public static UnityEvent OnPlayerDead = new UnityEvent();
}
