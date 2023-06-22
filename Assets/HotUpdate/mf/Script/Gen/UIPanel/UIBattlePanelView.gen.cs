//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBattlePanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIBattlePanel.prefab";

	#region variable

    private Transform tran_root = null;
    public Transform Tran_root => this.tran_root;
    private Transform tran_right = null;
    public Transform Tran_right => this.tran_right;
    private Text txt_rightSkill = null;
    public Text Txt_rightSkill => this.txt_rightSkill;
    private Image img_rightSkill = null;
    public Image Img_rightSkill => this.img_rightSkill;
    private Image img_rightBg = null;
    public Image Img_rightBg => this.img_rightBg;
    private Slider sli_right = null;
    public Slider Sli_right => this.sli_right;
    private Text txt_right = null;
    public Text Txt_right => this.txt_right;
    private Text txt_name2 = null;
    public Text Txt_name2 => this.txt_name2;
    private Image img_head2 = null;
    public Image Img_head2 => this.img_head2;
    private Transform tran_left = null;
    public Transform Tran_left => this.tran_left;
    private Text txt_leftSkill = null;
    public Text Txt_leftSkill => this.txt_leftSkill;
    private Image img_leftSkill = null;
    public Image Img_leftSkill => this.img_leftSkill;
    private Image img_leftBg = null;
    public Image Img_leftBg => this.img_leftBg;
    private Slider sli_left = null;
    public Slider Sli_left => this.sli_left;
    private Text txt_left = null;
    public Text Txt_left => this.txt_left;
    private Text txt_name1 = null;
    public Text Txt_name1 => this.txt_name1;
    private Image img_head1 = null;
    public Image Img_head1 => this.img_head1;
    private Button btn_back = null;
    public Button Btn_back => this.btn_back;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_root = this.transform.Find("tran_root");
#if UNITY_EDITOR
        this.NullAssert(this.tran_root, "tran_root");
#endif
        this.tran_right = this.transform.Find("tran_root/tran_right/tran_right");
#if UNITY_EDITOR
        this.NullAssert(this.tran_right, "tran_root/tran_right/tran_right");
#endif
        this.txt_rightSkill = this.transform.Find("tran_root/tran_right/tran_right/txt_rightSkill").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_rightSkill, "tran_root/tran_right/tran_right/txt_rightSkill");
#endif
        this.img_rightSkill = this.transform.Find("tran_root/tran_right/tran_right/img_rightSkill").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_rightSkill, "tran_root/tran_right/tran_right/img_rightSkill");
#endif
        this.img_rightBg = this.transform.Find("tran_root/tran_right/tran_right/img_rightBg").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_rightBg, "tran_root/tran_right/tran_right/img_rightBg");
#endif
        this.sli_right = this.transform.Find("tran_root/tran_right/sli_right").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_right, "tran_root/tran_right/sli_right");
#endif
        this.txt_right = this.transform.Find("tran_root/tran_right/sli_right/txt_right").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_right, "tran_root/tran_right/sli_right/txt_right");
#endif
        this.txt_name2 = this.transform.Find("tran_root/tran_right/Image/txt_name2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name2, "tran_root/tran_right/Image/txt_name2");
#endif
        this.img_head2 = this.transform.Find("tran_root/tran_right/img_bg/img_head2").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head2, "tran_root/tran_right/img_bg/img_head2");
#endif
        this.tran_left = this.transform.Find("tran_root/tran_left/tran_left");
#if UNITY_EDITOR
        this.NullAssert(this.tran_left, "tran_root/tran_left/tran_left");
#endif
        this.txt_leftSkill = this.transform.Find("tran_root/tran_left/tran_left/txt_leftSkill").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_leftSkill, "tran_root/tran_left/tran_left/txt_leftSkill");
#endif
        this.img_leftSkill = this.transform.Find("tran_root/tran_left/tran_left/img_leftSkill").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_leftSkill, "tran_root/tran_left/tran_left/img_leftSkill");
#endif
        this.img_leftBg = this.transform.Find("tran_root/tran_left/tran_left/img_leftBg").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_leftBg, "tran_root/tran_left/tran_left/img_leftBg");
#endif
        this.sli_left = this.transform.Find("tran_root/tran_left/sli_left").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_left, "tran_root/tran_left/sli_left");
#endif
        this.txt_left = this.transform.Find("tran_root/tran_left/sli_left/txt_left").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_left, "tran_root/tran_left/sli_left/txt_left");
#endif
        this.txt_name1 = this.transform.Find("tran_root/tran_left/Image/txt_name1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name1, "tran_root/tran_left/Image/txt_name1");
#endif
        this.img_head1 = this.transform.Find("tran_root/tran_left/img_bg/img_head1").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head1, "tran_root/tran_left/img_bg/img_head1");
#endif
        this.btn_back = this.transform.Find("tran_root/btn_back").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_back, "tran_root/btn_back");
#endif

    }
}