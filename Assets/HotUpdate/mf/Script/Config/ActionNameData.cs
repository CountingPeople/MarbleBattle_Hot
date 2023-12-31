﻿public enum ActionName
{
    WalkWithRootMotion,
    Walk,
    Run,
    RunWithRootMotion,
    Relax,
    Idle,
    Jump,
    MeleeRightAttack01,
    MeleeRightAttack02,
    MeleeLeftAttack01,
    ProjectileRightAttack01,
    CastSpell,
    SpinAttack,
    TakeDamage,
    Die,
    Defend,
    ShakeHead,
    NodHead,
    Stunned,
    WaveHand,
    PickUp,
    DrinkPotion,
    MeleeRightAttack03,
    LeftPunchAttack,
    RightPunchAttack,
    CastSpell02,
    FlyIdle,
    StrafeRightWithRootMotion,
    StrafeLeftWithRootMotion,
    CrossbowShootAttack,
    FlyForward,
    FlyTakeDamage,
    FlyMeleeRightAttack01,
    FlyMeleeRightAttack02,
    FlyMeleeRightAttack03,
    FlyMeleeLeftAttack01,
    FlyRightPunchAttack,
    FlyLeftPunchAttack,
    FlyCrossbowShootAttack,
    FlyProjectileRightAttack01,
    FlyCastSpell01,
    FlyCastSpell02,
    FlyDefend,
    FlyDie,
    ChopTree,
    Digging,
    Talking,
    Sitting,
    IdletoLayGround,
    LayGroundtoIdle,
    OntheGroundLoop,
    Crying,
    Clapping,
    JumpRightAttack01,
    DashWithRootMotion,
    Dash,
    Victory,
    SpearIdle,
    SpearMeleeAttack01,
    SpearMeleeAttack02,
    SpearWalk,
    SpearRun,
    SpearCastSpell01,
    SpearStrafeLeftWithRootMotion,
    SpearStrafeRightWithRootMotion,
    SpearWalkWithRootMotion,
    SpearRunWithRootMotion,
    SpearRelax,
    SpearDashWithRootMotion,
    SpearDash,
    SpearTakeDamage,
    SpearDefend,
    SpearDie,
    SpearJump,
    StrafeRight,
    StrafeLeft,
    CrossbowReload,
    CrossbowAim,
    CrossbowRightAim,
    CrossbowRightShootAttack,
    CrossbowRightReload,
    JumpWithoutRootMotion,
    THSwordIdle,
    THWalkWithoutRootMotion,
    THWalkWithRootMotion,
    THRunWithRootMotion,
    THRunWithoutRootMotion,
    THDashWithoutRootMotion,
    THDashWithRootMotion,
    THSwordMeleeAttack02,
    THSwordMeleeAttack01,
    THSwordCastSpell,
    THStrafeLeftWithoutRootMotion,
    THStrafeRightWithoutRootMotion,
    THStrafeRightWithRootMotion,
    THStrafeLeftWithRootMotion,
    THJumpWithRootMotion,
    THJumpWithoutRootMotion,
    THSwordDefend,
    THSwordTakeDamage,
    THSwordRelax,
    THSwordDie,
    RollForwardWithoutRootMotion,
    RollForwardWithRootMotion,
    RightThrow,
    PushWithRootMotion,
    PushWithoutRootMotion,
    PullWithRootMotion,
    PullWithoutRootMotion,
    LookUpWaveHand,
    LongbowShootAttack01,
    LongbowAim01,
    FlyLongbowAim01,
    FlyLongbowShootAttack01,
    WalkBackward,
    WalkBackwardWithRootMotion,
    RollBackwardWithRootMotion,
    RollBackwardWithoutRootMotion,
    Strumming,
    Treadwater,
    Swim02,
    Swim01,
    TorchWalkBackwardWRootMotion,
    TorchWalkBackwardWORootMotion,
    TorchWalkForwardWRootMotion,
    TorchWalkForwardWORootMotion,
    TorchIdle,
    TorchLookAround,
    TorchStrafeLeftWRootMotion,
    TorchStrafeLeftWORootMotion,
    TorchStrafeRightWRootMotion,
    TorchStrafeRightWORootMotion,
    LeftThrow,
    SpawnGround,
    RunBackwardWRootMotion,
    RunBackwardWORootMotion,
    Hammeringonanvil,
    PushButton,
    SawingWood,
    NinjaRunInPlace,
    NinjaRunWRoot,
}
public static class ActionNameData
{
    public static string[] actionArr = new string[]
    {
        "Walk With Root Motion",
        "Walk",
        "Run",
        "Run With Root Motion",
        "Relax",
        "Idle",
        "Jump",
        "Melee Right Attack 01",
        "Melee Right Attack 02",
        "Melee Left Attack 01",
        "Projectile Right Attack 01",
        "Cast Spell",
        "Spin Attack",
        "Take Damage",
        "Die",
        "Defend",
        "Shake Head",
        "Nod Head",
        "Stunned",
        "Wave Hand",
        "Pick Up",
        "Drink Potion",
        "Melee Right Attack 03",
        "Left Punch Attack",
        "Right Punch Attack",
        "Cast Spell 02",
        "Fly Idle",
        "Strafe Right With Root Motion",
        "Strafe Left With Root Motion",
        "Crossbow Shoot Attack",
        "Fly Forward",
        "Fly Take Damage",
        "Fly Melee Right Attack 01",
        "Fly Melee Right Attack 02",
        "Fly Melee Right Attack 03",
        "Fly Melee Left Attack 01",
        "Fly Right Punch Attack",
        "Fly Left Punch Attack",
        "Fly Crossbow Shoot Attack",
        "Fly Projectile Right Attack 01",
        "Fly Cast Spell 01",
        "Fly Cast Spell 02",
        "Fly Defend",
        "Fly Die",
        "Chop Tree",
        "Digging",
        "Talking",
        "Sitting",
        "Idle to Lay Ground",
        "Lay Ground to Idle",
        "On the Ground Loop",
        "Crying",
        "Clapping",
        "Jump Right Attack 01",
        "Dash With Root Motion",
        "Dash",
        "Victory",
        "Spear Idle",
        "Spear Melee Attack 01",
        "Spear Melee Attack 02",
        "Spear Walk",
        "Spear Run",
        "Spear Cast Spell 01",
        "Spear Strafe Left With Root Motion",
        "Spear Strafe Right With Root Motion",
        "Spear Walk With Root Motion",
        "Spear Run With Root Motion",
        "Spear Relax",
        "Spear Dash With Root Motion",
        "Spear Dash",
        "Spear Take Damage",
        "Spear Defend",
        "Spear Die",
        "Spear Jump",
        "Strafe Right",
        "Strafe Left",
        "Crossbow Reload",
        "Crossbow Aim",
        "Crossbow Right Aim",
        "Crossbow Right Shoot Attack",
        "Crossbow Right Reload",
        "Jump Without Root Motion",
        "TH Sword Idle",
        "TH Walk Without Root Motion",
        "TH Walk With Root Motion",
        "TH Run With Root Motion",
        "TH Run Without Root Motion",
        "TH Dash Without Root Motion",
        "TH Dash With Root Motion",
        "TH Sword Melee Attack 02",
        "TH Sword Melee Attack 01",
        "TH Sword Cast Spell",
        "TH Strafe Left Without Root Motion",
        "TH Strafe Right Without Root Motion",
        "TH Strafe Right With Root Motion",
        "TH Strafe Left With Root Motion",
        "TH Jump With Root Motion",
        "TH Jump Without Root Motion",
        "TH Sword Defend",
        "TH Sword Take Damage",
        "TH Sword Relax",
        "TH Sword Die",
        "Roll Forward Without Root Motion",
        "Roll Forward With Root Motion",
        "Right Throw",
        "Push With Root Motion",
        "Push Without Root Motion",
        "Pull With Root Motion",
        "Pull Without Root Motion",
        "Look Up Wave Hand",
        "Longbow Shoot Attack 01",
        "Longbow Aim 01",
        "Fly Longbow Aim 01",
        "Fly Longbow Shoot Attack 01",
        "Walk Backward",
        "Walk Backward With Root Motion",
        "Roll Backward With Root Motion",
        "Roll Backward Without Root Motion",
        "Strumming",
        "Tread water",
        "Swim 02",
        "Swim 01",
        "Torch Walk Backward W Root Motion",
        "Torch Walk Backward WO Root Motion",
        "Torch Walk Forward W Root Motion",
        "Torch Walk Forward WO Root Motion",
        "Torch Idle",
        "Torch Look Around",
        "Torch Strafe Left W Root Motion",
        "Torch Strafe Left WO Root Motion",
        "Torch Strafe Right W Root Motion",
        "Torch Strafe Right WO Root Motion",
        "Left Throw",
        "Spawn Ground",
        "Run Backward W Root Motion",
        "Run Backward WO Root Motion",
        "Hammering on anvil",
        "Push Button",
        "Sawing Wood",
        "Ninja Run In Place",
        "Ninja Run W Root",
    };
    public static string GetActionName(ActionName actionName)
    {
        return actionArr[(int)actionName];
    }
}
