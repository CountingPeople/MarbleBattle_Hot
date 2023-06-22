
using UnityEngine;
/// <summary>
/// 玩法类型
/// </summary>
public enum GamePlayEnum
{ 
   Default,
   Challenge,
   Dungeon,
   Level,
   Rank,
   Achive,
}

/// <summary>
/// 达成成就条件
/// </summary>
public enum AchiveCondType
{ 
    Cond_KillEnemy=1,//杀死敌人
    Cond_GetEquip,//获得装备
    Cond_DissiveEquip,//分解装备
    Cond_UpLevel,//玩家升级
    Cond_TotalPlayDay//总共上线天数
}

public enum AchiveGetState
{ 
   NotFinish,
   CanGet,
   HaveGet
}

public enum TrackType
{
    Direct,
    Parabolic,
    Collider,
    PlayerCollider,
}

public enum HitReferType
{
    Self,
    Other,
}

public enum PlayerPropEnum
{ 
   Player_Level,
   Player_Name,
   Player_Head,
   Player_Exp,
   Player_Rank
}

public enum BattleResult
{
    NotStart,
    Battle,
    Win,
    Fail
}

public enum MovementState : byte
{
    None = 0,
    Forward = 1 << 0,
    Backward = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    IsGrounded = 1 << 4,
    IsSprinting = 1 << 5,
    IsJump = 1 << 6,
}

[System.Serializable]
public struct DefaultAnimatorData
{
    public AnimationClip idleClip;
    public AnimationClip moveClip;
    public AnimationClip moveBackwardClip;
    public AnimationClip moveLeftClip;
    public AnimationClip moveRightClip;
    public AnimationClip moveForwardLeftClip;
    public AnimationClip moveForwardRightClip;
    public AnimationClip moveBackwardLeftClip;
    public AnimationClip moveBackwardRightClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    public AnimationClip hurtClip;
    public AnimationClip deadClip;
    public AnimationClip actionClip;
    public AnimationClip castSkillClip;
    public AnimationClip defendClip;
}

public enum BattleAwardEnum
{ 
    DungenRandRatio=6,
}

public enum AudioEnum
{
   Audio_2D,
   Audio_3D,
}


public enum EventEnum
{ 
    BagHaveNoEquip
}

public enum AdEnum
{ 
   RefreshEnemy,//看广告刷新对手-android-激励视频
   RefreshDrogen,//看广告刷新地下城-android-激励视频
   GetKey//看广告得钥匙-android-激励视频
}
