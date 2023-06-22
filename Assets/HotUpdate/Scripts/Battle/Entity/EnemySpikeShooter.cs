using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpikeShooter : IBattleEntity
{
    private float mMoveSpeed = 1;
    public float MoveSpeed
    {
        set { mMoveSpeed = value; }
    }

    private float mAttackCD = 1;
    public float AttackCD
    {
        set { 
            mAttackCD = value;
            mCurCD = mAttackCD;
        }
    }

    private float mCurCD = 0;

    private float mHPMax = 0.0f;
    public float HP
    {
        set
        {
            mHPMax = value;
            mHP = mHPMax;
        }
    }
    private float mHP = 0.0f;

    public Transform _ShotOrigin;
    public Transform _HP;
    private Vector3 mHPTransformScale;
    public float _ShotVelocity = 1.0f;

    GameObject mWeapon;

    private Vector3 mMoveDir = Vector3.right;
    public SpriteRenderer _SpriteRenderer;

    private void Awake()
    {
        Camp = ECamp.Enemy;
        mHPTransformScale = _HP.localScale;
    }

    protected override void Start()
    {
        base.Start();

        mWeapon = ResourcesModule.Instance.Load<GameObject>("Assets/Bundles/Res/Prefabs/Battle/weapon_bianbian.prefab");
    }

    // Update is called once per frame
    public override void Tick()
    {
        // position logic
        Bounds curBounds = _SpriteRenderer.bounds;
        Vector3 border = curBounds.center;
        border.x += mMoveDir.x * curBounds.extents.x;

        // TODO: cache this
        var battleCamera = CameraManager.Instance.BattleCamera;
        Vector2 viewport = new Vector2(0, battleCamera.orthographicSize * 2);
        viewport.x = viewport.y * battleCamera.aspect;

        float viewportBorder = viewport.x / 2.0f;

        var deltaPosition = Vector3.zero;

        float gap = viewportBorder - Mathf.Abs(border.x);
        if(gap <= 0)
        {
            float correctDis = Mathf.Abs(gap) + 0.0001f;
            mMoveDir.x *= -1;

            deltaPosition.x += mMoveDir.x * correctDis;
        }

        deltaPosition.x += mMoveDir.x * mMoveSpeed * Time.deltaTime;
        transform.Translate(deltaPosition);

        // shot logic
        mCurCD -= Time.deltaTime;
        if(mCurCD <= 0)
        {
            Shot();
            mCurCD += mAttackCD;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerWeapon = collision.gameObject.GetComponent<MissileEntity>();
        if (playerWeapon == null || playerWeapon.Camp != ECamp.Player)
            return;

        mHP -= playerWeapon.GetHit();
        var scale = mHPTransformScale;
        scale.x *= (mHP / mHPMax);
        _HP.localScale = scale;

        if (mHP > 0)
            return;

        MonsterManager.Instance.RemoveEnemy(gameObject);
        GameObject.Destroy(gameObject);
    }

    void Shot()
    {
        var weapon = GameObject.Instantiate<GameObject>(mWeapon, transform.parent);
        weapon.GetComponent<EnemyWeaponSpikeEntity>().Shot(_ShotOrigin, -_ShotOrigin.up * _ShotVelocity);
    }
}
