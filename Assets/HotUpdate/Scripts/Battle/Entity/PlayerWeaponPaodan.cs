using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponPaodan : MissileEntity
{
    private Vector3 mVelocity;

    private void Awake()
    {
        Camp = ECamp.Player;
    }

    public override void Tick()
    {
        transform.position += Time.deltaTime * mVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemyEntity = collision.gameObject.GetComponent<IBattleEntity>();
        if (enemyEntity != null && enemyEntity.Camp == ECamp.Enemy)
        {
            if(DeadEffect != null)
            {
                GameObject effect = GameObject.Instantiate<GameObject>(DeadEffect);
                effect.transform.position = transform.position;
                effect.transform.SetParent(transform.parent);
            }

            GameObject.Destroy(gameObject);
        }
    }

    public void Shot(Transform shotFrom, Vector3 velocity)
    {
        transform.position = shotFrom.position;

        Vector3 shotDir = velocity.normalized;
        var localRotation = transform.localRotation;
        localRotation.SetFromToRotation(Vector3.up, shotDir);
        transform.localRotation = localRotation;

        mVelocity = velocity;
    }

    public override float GetHit()
    {
        return 5.0f;
    }
}
