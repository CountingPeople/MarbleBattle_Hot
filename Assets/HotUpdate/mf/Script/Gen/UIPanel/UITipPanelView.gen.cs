//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UITipPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UITipPanel.prefab";

	#region variable

    private Text txt_content = null;
    public Text Txt_content => this.txt_content;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;
    private Text txt_title = null;
    public Text Txt_title => this.txt_title;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_content = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/tran_sclv/Viewport/txt_content").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_content, "HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/tran_sclv/Viewport/txt_content");
#endif
        this.btn_close = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/btn_close");
#endif
        this.txt_title = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/Popup_TitleBar/txt_title").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_title, "HomeScene_Popup_Signup/Popup_Signup/Popup/Popup_bg/Popup_TitleBar/txt_title");
#endif

    }
}