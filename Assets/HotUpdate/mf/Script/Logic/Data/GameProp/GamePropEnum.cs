using System;
public enum GamePropEnum
{
    Power=1, //攻击力
    Hp=2, //血量

    Atk_Cold = 3, //寒冷攻击
    Atk_Physical = 4, //物理攻击
    Atk_Fire = 5, //火焰攻击
    Atk_Poison = 6, //剧毒攻击

    Def_Cold =7, //寒冷抗性
    Def_Physical=8, //物理抗性
    Def_Fire=9, //火焰抗性
    Def_Poison=10, //剧毒抗性

    Rate_Block=11, //格挡几率
    Add_CriHurt=12, //暴击伤害加成
    Rate_Cri=13, //暴击几率
    Rate_Dodge=14, //躲闪几率
}

public enum GameSlotEnum
{ 
   Head,
   Shield,
   Sword,
   Body,
   Necklace,
   Ring,
}
