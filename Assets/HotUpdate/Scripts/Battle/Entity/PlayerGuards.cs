using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuards : IBattleEntity
{
    public float _FireCD = 0.5f;
    private float mFireTimer;
    public float _ShotVelocity = 1.0f;
    public float _HPMax = 10.0f;
    private GameObject mHPGameObject;
    public Transform _HP;
    private Vector3 mHPTransformScale;

    // TODO: global config @zhangrufu
    private float mDropingVelocity = 1.5f;

    // State
    // TODO: FSM
    public enum State
    {
        WaitPlace,
        Droping,
        Ready,
    }
    private State mState = State.WaitPlace;
    public State SoilderState
    {
        get { return mState; }
        set 
        { 
            mState = value;
            if (mState == State.WaitPlace)
                mHPGameObject.SetActive(false);
            else
                mHPGameObject.SetActive(true);
        }
    }

    public float CurHP { get; set; }

    public float _TargetT = 0.5f;

    private GameObject mWeapon;

    public SpriteRenderer _BodyRenderer;
    public SpriteRenderer BodyRenderer
    {
        get { return _BodyRenderer; }
    }

    private void Awake()
    {
        Camp = ECamp.Player;
        mHPTransformScale = _HP.localScale;

        mHPGameObject = transform.GetChild(1).gameObject;
        Debug.Assert(mHPGameObject != null && mHPGameObject.name == "HPBackground", "SoilderTemplate child order is changed!");
    }

    protected override void Start()
    {
        base.Start();

        CurHP = _HPMax;
        mWeapon = ResourcesModule.LoadAssetAtPath<GameObject>("Assets/Bundles/Res/Prefabs/Battle/weapon_yuci.prefab");
        mFireTimer = _FireCD;
    }

    public override void Tick()
    {
        // TODO: FSM
        switch (SoilderState)
        {
            case State.WaitPlace:
                return;
            case State.Droping:
                Droping();
                break;
            case State.Ready:
                mFireTimer -= Time.deltaTime;
                if (mFireTimer <= 0)
                {
                    Shot();
                    mFireTimer = _FireCD;
                }
                break;
        } 
    }

    void Droping()
    {
        Vector3 dropDir = Vector3.down;
        transform.position += Time.deltaTime * mDropingVelocity * dropDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerWeapon = collision.gameObject.GetComponent<IBattleEntity>();
        if (playerWeapon == null)
            return;

        // TODO: use FSM @zhangrufu
        if(playerWeapon.Camp != ECamp.Enemy)
        {
            if(SoilderState == State.Droping)
            {
                if(playerWeapon is GroundEntity)
                {
                    SoilderState = State.Ready;
                    return;
                }
            }

            return;
        }

        CurHP -= playerWeapon.GetHit();
        var scale = mHPTransformScale;
        scale.x *= (CurHP / _HPMax);
        _HP.localScale = scale;

        if (CurHP > 0)
            return;

        GameObject.Destroy(gameObject);
    }

    void Shot()
    {
        // get target
        var enemy = MonsterManager.Instance.GetNearestEnemy(transform.position);
        if (enemy == null)
            return;

        var shooter = enemy.GetComponent<EnemySpikeShooter>();
        Vector3 targetPosition = shooter.transform.position;
        Vector3 dir = (targetPosition - transform.position).normalized;

        var weapon = GameObject.Instantiate<GameObject>(mWeapon, transform.parent);
        var paodanEntity = weapon.GetComponent<PlayerWeaponPaodan>();

        paodanEntity.Shot(transform, dir * _ShotVelocity);
    }
}
