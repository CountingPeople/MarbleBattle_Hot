using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : StateBase
{
    public override void OnEnter()
    {
        base.OnEnter();


        UIModule.Instance.CloseAllPanel();
        UIModule.Instance.ShowPanel<UIBattlePanelView>();
        //BattleSystem.Instance.EnterBattle();
        //GamePlaySystem.Instance.StartGame(GamePlayEnum.Challenge);
    }

    public override void OnLeave()
    {
        base.OnLeave();
    }
}
