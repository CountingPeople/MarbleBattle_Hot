//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIStagePanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIStagePanel.prefab";

	#region variable

    private Button btn_close = null;
    public Button Btn_close => this.btn_close;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif
        this.tran_list = this.transform.Find("Scroll View/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "Scroll View/Viewport/tran_list");
#endif

    }
}