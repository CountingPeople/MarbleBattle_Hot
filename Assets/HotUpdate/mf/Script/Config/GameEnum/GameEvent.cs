
/// <summary>
/// 网络事件
/// </summary>
public enum NetEvent
{
    OnConnect,
    OnDisconnect,
}


public enum DragType 
{
    Null,
    Ship,
    Skill,
    BattleShip,
}

//public enum Faction { 
//   Own,
//   Player,
//   Enemy
//}

public enum GridState { 
    Effect,
    Cast,
    Normal,
    Put,
    Atk,
}

public enum ShipActionState 
{  
    Move,
    Atk,
    Skill
}

internal enum ShipPropEnum
{
    //---生命值
    Hp = 1,
    //---最大生命值
    MaxHp = 2,
    //---蓝量
    Mp = 3,
    //---最大蓝量
    MaxMp = 4,
    //---攻击
    Fire = 5,
    //---破甲
    Pojia = 6,
    //---护甲
    Armor = 7,
    //---命中
    Accuracy = 8,
    //---闪避
    Motility = 9,
    //---射程
    AttackRange = 10,
    //---移动范围
    MoveRange = 11,
    //---速度
    Speed = 12,
}

