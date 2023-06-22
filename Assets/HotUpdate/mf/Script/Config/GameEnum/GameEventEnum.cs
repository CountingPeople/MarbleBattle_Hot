

/// <summary>
/// 游戏事件枚举
/// </summary>
internal enum GameEventEnum
{
    #region 1-1000为UI刷新

    ShipItemDropData,
    ShipItemDrop,

    SkillItemDropData,
    SkillItemDrop,

    SynRoundIndx,//同步回合数
    SynActionFaction,//同步行动阵营

    RefreshBattleShipList,//刷新战斗船只列表
    RefreshBattleUnit,//刷新战斗船只单位

    GenBlood,//创建血条
    SetBloodFlow,//飘字
    SetBloodSlider,//设置进度
    ClickBattleShip,//点击战船

    AddBuffList,//添加buff列表
    AddBuffSingle,//添加buff
    RemoveBuffList,//移除buff列表
    RemoveBuffSingle,//移除buff
    SynSkillCd,//同步技能Cd

    SynUpdate,

    PlaySkill,


    #endregion

    #region net

    OnConnect,
    OnDisconnect,
    OnBackData,

    OnLoginBack,
    OnCreateBack,
    OnShipUp,//请求上阵
    OnShipDown,//请求下阵
    OnReqMoveOnUnit,//请求移动
    OnReqAtk,//请求攻击
    OnReqSkillAtk,//请求技能攻击

    #endregion

    #region 1000-2000为刷新战斗片段

    SetSelectCell,
    GenUnit,
    MoveOnUnit,


    #endregion

    #region 2000-3000 统计数据

    #endregion
}

