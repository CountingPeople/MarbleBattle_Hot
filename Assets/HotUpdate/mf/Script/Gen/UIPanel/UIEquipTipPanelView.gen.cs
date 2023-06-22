//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIEquipTipPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIEquipTipPanel.prefab";

	#region variable

    private Transform tran_item = null;
    public Transform Tran_item => this.tran_item;
    private Button btn_change = null;
    public Button Btn_change => this.btn_change;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_item = this.transform.Find("tran_item");
#if UNITY_EDITOR
        this.NullAssert(this.tran_item, "tran_item");
#endif
        this.btn_change = this.transform.Find("btn_change").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_change, "btn_change");
#endif
        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif

    }
}