using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


internal partial class UIFlowPanelView
{

    public override UILayerEnum LayerEnum => UILayerEnum.Tooltip;

    protected override void OnCreate()
    {
        //code
    }

    protected override void OnDestroy()
    {
        //code
    }

    internal override void Internal_OnEnable()
    {
        base.Internal_OnEnable();

    }


    public void ShowTip(string content)
    {
        txt_content.text = content;

        tran_tip.transform.localPosition = new Vector3(0, -160, 0);
        tran_tip.gameObject.SetActive(true);
        tran_tip.transform.DORestart();
        tran_tip.transform.DOLocalMoveY(0, 0.8f).OnComplete(() => {
            tran_tip.transform.localPosition = new Vector3(0, -160, 0);
            tran_tip.gameObject.SetActive(false);
        });
    }




}
