//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIEquipSelectPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIEquipSelectPanel.prefab";

	#region variable

    private Transform tran_equip = null;
    public Transform Tran_equip => this.tran_equip;
    private Button btn_equip = null;
    public Button Btn_equip => this.btn_equip;
    private Button btn_dissive = null;
    public Button Btn_dissive => this.btn_dissive;
    private Transform tran_info = null;
    public Transform Tran_info => this.tran_info;
    private Text txt_skillDesc = null;
    public Text Txt_skillDesc => this.txt_skillDesc;
    private Text txt_skillName = null;
    public Text Txt_skillName => this.txt_skillName;
    private Image img_skillIcon = null;
    public Image Img_skillIcon => this.img_skillIcon;
    private Text txt_prop4 = null;
    public Text Txt_prop4 => this.txt_prop4;
    private Text txt_prop3 = null;
    public Text Txt_prop3 => this.txt_prop3;
    private Text txt_prop2 = null;
    public Text Txt_prop2 => this.txt_prop2;
    private Text txt_prop1 = null;
    public Text Txt_prop1 => this.txt_prop1;
    private Image img_prop = null;
    public Image Img_prop => this.img_prop;
    private Text txt_mainProp = null;
    public Text Txt_mainProp => this.txt_mainProp;
    private Text txt_level = null;
    public Text Txt_level => this.txt_level;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_equip = this.transform.Find("HomeScene_Popup_Signup/tran_equip");
#if UNITY_EDITOR
        this.NullAssert(this.tran_equip, "HomeScene_Popup_Signup/tran_equip");
#endif
        this.btn_equip = this.transform.Find("HomeScene_Popup_Signup/tran_equip/btn_equip").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_equip, "HomeScene_Popup_Signup/tran_equip/btn_equip");
#endif
        this.btn_dissive = this.transform.Find("HomeScene_Popup_Signup/tran_equip/btn_dissive").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_dissive, "HomeScene_Popup_Signup/tran_equip/btn_dissive");
#endif
        this.tran_info = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info");
#if UNITY_EDITOR
        this.NullAssert(this.tran_info, "HomeScene_Popup_Signup/tran_info/tran_info");
#endif
        this.txt_skillDesc = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/txt_skillDesc").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_skillDesc, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/txt_skillDesc");
#endif
        this.txt_skillName = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/txt_skillName").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_skillName, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/txt_skillName");
#endif
        this.img_skillIcon = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/img_skillIcon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_skillIcon, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_skill/img_skillIcon");
#endif
        this.txt_prop4 = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop4").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop4, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop4");
#endif
        this.txt_prop3 = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop3").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop3, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop3");
#endif
        this.txt_prop2 = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop2, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop2");
#endif
        this.txt_prop1 = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop1, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/tran_prop/txt_prop1");
#endif
        this.img_prop = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/img_prop").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_prop, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/img_prop");
#endif
        this.txt_mainProp = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/img_prop/txt_mainProp").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_mainProp, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/img_prop/txt_mainProp");
#endif
        this.txt_level = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/txt_level").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_level, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/txt_level");
#endif
        this.txt_name = this.transform.Find("HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "HomeScene_Popup_Signup/tran_info/tran_info/tran_equip/txt_name");
#endif
        this.tran_list = this.transform.Find("HomeScene_Popup_Signup/tran_info/Image/tram_sclv/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "HomeScene_Popup_Signup/tran_info/Image/tram_sclv/Viewport/tran_list");
#endif
        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif

    }
}