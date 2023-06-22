using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : StateBase
{
    public override void OnEnter()
    {
        base.OnEnter();


        var tempPanel = UIModule.Instance.ShowPanel<LoadPanelView>();
        tempPanel.SetEvent(_loadEvent);
    }

    public override void OnLeave()
    {
        base.OnLeave();

        UIModule.Instance.ClosePanel<LoadPanelView>();
    }
}
