using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntity : IBattleEntity
{
    private void Awake()
    {
        Camp = ECamp.Player;
    }

    // Update is called once per frame
    public override void Tick()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ew = collision.gameObject.GetComponent<IBattleEntity>();
        if (ew.Camp == ECamp.Enemy)
            BattleEventManager.OnGroundAttacked.Invoke(ew);
    }
}
