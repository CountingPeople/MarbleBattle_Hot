using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicEnum
{ 
   Hero,
   Chapter,
   Battle,
}


public class GameDataSystem : BaseSystem<GameDataSystem>
{
    public override void InitGame()
    {
        base.InitGame();

        Debug.Log("初始化数据");

        //SkillLogic.Instance.InitData();
        //EquipLogic.Instance.InitData();
        //CacheLogic.Instance.InitData();
        //PlayerLogic.Instance.InitData();
        //Battle_StageLogic.Instance.InitData();
        //AchivmentLogic.Instance.InitData();
        //Battle_DungeonLogic.Instance.InitData();

        //CacheLogic.Instance.LoadData();

        //Battle_RankLogic.Instance.InitData();
    }

    public override void UpdateGame()
    {
        base.UpdateGame();

    }


    public override void LeaveGame()
    {
        base.LeaveGame();

        CacheLogic.Instance.SaveData();
    }

}
