//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIRankPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIRankPanel.prefab";

	#region variable

    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Image img_head = null;
    public Image Img_head => this.img_head;
    private Text txt_power = null;
    public Text Txt_power => this.txt_power;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Text txt_rank = null;
    public Text Txt_rank => this.txt_rank;
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
        this.img_head = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/img_head").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head, "HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/img_head");
#endif
        this.txt_power = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_power").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_power, "HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_power");
#endif
        this.txt_name = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_name");
#endif
        this.txt_rank = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_rank").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_rank, "HomeScene_Popup_Signup/Popup_Signup/Popup/Button_Blue/UIRankItem/txt_rank");
#endif
        this.btn_close = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close");
#endif

    }
}