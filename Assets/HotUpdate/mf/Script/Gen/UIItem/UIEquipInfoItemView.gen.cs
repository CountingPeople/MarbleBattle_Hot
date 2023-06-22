//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIEquipInfoItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIEquipInfoItem.prefab";

	#region variable

    private Transform tran_prop = null;
    public Transform Tran_prop => this.tran_prop;
    private Text txt_prop4 = null;
    public Text Txt_prop4 => this.txt_prop4;
    private Text txt_prop3 = null;
    public Text Txt_prop3 => this.txt_prop3;
    private Text txt_prop2 = null;
    public Text Txt_prop2 => this.txt_prop2;
    private Text txt_prop1 = null;
    public Text Txt_prop1 => this.txt_prop1;
    private Text txt_skillDesc = null;
    public Text Txt_skillDesc => this.txt_skillDesc;
    private Text txt_skillName = null;
    public Text Txt_skillName => this.txt_skillName;
    private Image img_skillIcon = null;
    public Image Img_skillIcon => this.img_skillIcon;
    private Transform tran_equip = null;
    public Transform Tran_equip => this.tran_equip;
    private Text txt_mainProp = null;
    public Text Txt_mainProp => this.txt_mainProp;
    private Image img_prop = null;
    public Image Img_prop => this.img_prop;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Text txt_level = null;
    public Text Txt_level => this.txt_level;
    private Image img_frame = null;
    public Image Img_frame => this.img_frame;
    private Image img_icon = null;
    public Image Img_icon => this.img_icon;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_prop = this.transform.Find("tran_prop");
#if UNITY_EDITOR
        this.NullAssert(this.tran_prop, "tran_prop");
#endif
        this.txt_prop4 = this.transform.Find("tran_prop/txt_prop4").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop4, "tran_prop/txt_prop4");
#endif
        this.txt_prop3 = this.transform.Find("tran_prop/txt_prop3").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop3, "tran_prop/txt_prop3");
#endif
        this.txt_prop2 = this.transform.Find("tran_prop/txt_prop2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop2, "tran_prop/txt_prop2");
#endif
        this.txt_prop1 = this.transform.Find("tran_prop/txt_prop1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop1, "tran_prop/txt_prop1");
#endif
        this.txt_skillDesc = this.transform.Find("tran_skill/txt_skillDesc").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_skillDesc, "tran_skill/txt_skillDesc");
#endif
        this.txt_skillName = this.transform.Find("tran_skill/txt_skillName").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_skillName, "tran_skill/txt_skillName");
#endif
        this.img_skillIcon = this.transform.Find("tran_skill/img_skillIcon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_skillIcon, "tran_skill/img_skillIcon");
#endif
        this.tran_equip = this.transform.Find("tran_equip");
#if UNITY_EDITOR
        this.NullAssert(this.tran_equip, "tran_equip");
#endif
        this.txt_mainProp = this.transform.Find("txt_mainProp").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_mainProp, "txt_mainProp");
#endif
        this.img_prop = this.transform.Find("img_prop").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_prop, "img_prop");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif
        this.txt_level = this.transform.Find("txt_level").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_level, "txt_level");
#endif
        this.img_frame = this.transform.Find("img_frame").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_frame, "img_frame");
#endif
        this.img_icon = this.transform.Find("img_frame/img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "img_frame/img_icon");
#endif

    }
}