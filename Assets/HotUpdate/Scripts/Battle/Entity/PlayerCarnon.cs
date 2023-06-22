using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarnon : IBattleEntity
{
    public bool _IsFlip = false;
    public Transform _ShotOrigin;
    public Transform _PaoDir;
    public float _ShotVelocity = 1.0f;

    private GameObject mWeapon;

    public enum CarnonType
    {
        Unknown,
        Left,
        Right
    }

    public CarnonType _CarnonType = CarnonType.Unknown;

    private static CarnonType _ActiveCarnon = CarnonType.Left;

    private void Awake()
    {
        Camp = ECamp.Player;
    }

    protected override void Start()
    {
        base.Start();

#if UNITY_EDITOR
        Debug.Assert(_CarnonType != CarnonType.Unknown);
#endif
        mWeapon = ResourcesModule.Instance.Load<GameObject>("Assets/Bundles/Res/Prefabs/Battle/weapon_ci.prefab");

        MarbleEventManager.OnMarbleHitBorder.AddListener(OnBrickHitBorder);
    }

    // Update is called once per frame
    public override void Tick()
    {
        
    }

    bool ReadyShot()
    {
        return _ActiveCarnon == _CarnonType;
    }

    public static void ChangeCarnon()
    {
        _ActiveCarnon = _ActiveCarnon == CarnonType.Left ? CarnonType.Right : CarnonType.Left;
    }

    void OnBrickHitBorder()
    {
        Shot();
    }

    void Shot()
    {
        if (!ReadyShot())
            return;

        // get target
        var enemy = MonsterManager.Instance.GetNearestEnemy(transform.position);
        if (enemy == null)
            return;

        var shooter = enemy.GetComponent<EnemySpikeShooter>();
        Vector3 targetPosition = shooter.transform.position;
        Vector3 dir = (targetPosition - transform.position).normalized;
        if (_IsFlip)
            dir = -dir;
        _PaoDir.right = dir;

        var weapon = GameObject.Instantiate<GameObject>(mWeapon, transform.parent);
        var paodanEntity = weapon.GetComponent<PlayerWeaponPaodan>();
        var shootDir = _ShotOrigin.right;
        if(_IsFlip)
            shootDir = -shootDir;

        paodanEntity.Shot(_ShotOrigin, shootDir * _ShotVelocity);
    }
}
