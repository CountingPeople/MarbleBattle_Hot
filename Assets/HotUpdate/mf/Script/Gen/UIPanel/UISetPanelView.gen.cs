//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UISetPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UISetPanel.prefab";

	#region variable

    private Toggle tog_music = null;
    public Toggle Tog_music => this.tog_music;
    private Slider sli_music = null;
    public Slider Sli_music => this.sli_music;
    private Button btn_head = null;
    public Button Btn_head => this.btn_head;
    private Button btn_name = null;
    public Button Btn_name => this.btn_name;
    private Image img_head = null;
    public Image Img_head => this.img_head;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tog_music = this.transform.Find("tran_root/tog_music").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_music, "tran_root/tog_music");
#endif
        this.sli_music = this.transform.Find("tran_root/sli_music").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_music, "tran_root/sli_music");
#endif
        this.btn_head = this.transform.Find("tran_root/btn_head").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_head, "tran_root/btn_head");
#endif
        this.btn_name = this.transform.Find("tran_root/btn_name").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_name, "tran_root/btn_name");
#endif
        this.img_head = this.transform.Find("tran_root/img_head").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head, "tran_root/img_head");
#endif
        this.txt_name = this.transform.Find("tran_root/Image_name/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "tran_root/Image_name/txt_name");
#endif
        this.btn_close = this.transform.Find("HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "HomeScene_Popup_Signup/Popup_Signup/Popup/btn_close");
#endif

    }
}