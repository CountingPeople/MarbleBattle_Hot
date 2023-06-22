using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSpikeEntity : MissileEntity
{
    private Vector3 mVelocity;

    private void Awake()
    {
        Camp = ECamp.Enemy;
    }

    public override void Tick()
    {
        transform.position += Time.deltaTime * mVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var entity = collision.gameObject.GetComponent<IBattleEntity>();
        if (entity != null && entity.Camp == ECamp.Player)
        {
            GameObject effect = GameObject.Instantiate<GameObject>(DeadEffect);
            effect.transform.position = transform.position;
            effect.transform.SetParent(transform.parent);
            GameObject.Destroy(gameObject);
        }
    }

    public void Shot(Transform shotFrom, Vector3 velocity)
    {
        transform.position = shotFrom.position;
        transform.rotation = shotFrom.rotation;
        mVelocity = velocity;
    }

    public override float GetHit()
    {
        return 5.0f;
    }
}
