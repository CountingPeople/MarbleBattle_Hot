
using UnityEngine;
/// <summary>
/// �淨����
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
/// ��ɳɾ�����
/// </summary>
public enum AchiveCondType
{ 
    Cond_KillEnemy=1,//ɱ������
    Cond_GetEquip,//���װ��
    Cond_DissiveEquip,//�ֽ�װ��
    Cond_UpLevel,//�������
    Cond_TotalPlayDay//�ܹ���������
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
   RefreshEnemy,//�����ˢ�¶���-android-������Ƶ
   RefreshDrogen,//�����ˢ�µ��³�-android-������Ƶ
   GetKey//������Կ��-android-������Ƶ
}
