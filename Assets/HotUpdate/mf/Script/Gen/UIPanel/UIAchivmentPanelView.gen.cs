//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIAchivmentPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIAchivmentPanel.prefab";

	#region variable

    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_list = this.transform.Find("tran_root/Scroll View/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_root/Scroll View/Viewport/tran_list");
#endif
        this.btn_close = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close");
#endif

    }
}