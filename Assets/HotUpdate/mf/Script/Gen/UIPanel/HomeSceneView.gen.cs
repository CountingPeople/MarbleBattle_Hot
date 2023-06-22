//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class HomeSceneView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/HomeScene.prefab";

	#region variable

    private Transform tran_root = null;
    public Transform Tran_root => this.tran_root;
    private Button btn_shop = null;
    public Button Btn_shop => this.btn_shop;
    private Button btn_main = null;
    public Button Btn_main => this.btn_main;
    private Button btn_hero = null;
    public Button Btn_hero => this.btn_hero;
    private Button btn_bag = null;
    public Button Btn_bag => this.btn_bag;
    private Transform tran_full = null;
    public Transform Tran_full => this.tran_full;
    private Button btn_set = null;
    public Button Btn_set => this.btn_set;
    private Text txt_key = null;
    public Text Txt_key => this.txt_key;
    private Image img_head = null;
    public Image Img_head => this.img_head;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Text txt_level = null;
    public Text Txt_level => this.txt_level;
    private Slider sli_exp = null;
    public Slider Sli_exp => this.sli_exp;
    private Text txt_exp = null;
    public Text Txt_exp => this.txt_exp;
    private Transform tran_middle = null;
    public Transform Tran_middle => this.tran_middle;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_root = this.transform.Find("tran_root");
#if UNITY_EDITOR
        this.NullAssert(this.tran_root, "tran_root");
#endif
        this.btn_shop = this.transform.Find("tran_root/HomeMenu/btn_shop").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_shop, "tran_root/HomeMenu/btn_shop");
#endif
        this.btn_main = this.transform.Find("tran_root/HomeMenu/btn_main").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_main, "tran_root/HomeMenu/btn_main");
#endif
        this.btn_hero = this.transform.Find("tran_root/HomeMenu/btn_hero").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_hero, "tran_root/HomeMenu/btn_hero");
#endif
        this.btn_bag = this.transform.Find("tran_root/HomeMenu/btn_bag").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_bag, "tran_root/HomeMenu/btn_bag");
#endif
        this.tran_full = this.transform.Find("tran_root/tran_full");
#if UNITY_EDITOR
        this.NullAssert(this.tran_full, "tran_root/tran_full");
#endif
        this.btn_set = this.transform.Find("tran_root/tran_top/btn_set").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_set, "tran_root/tran_top/btn_set");
#endif
        this.txt_key = this.transform.Find("tran_root/tran_top/Status_Gem/txt_key").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_key, "tran_root/tran_top/Status_Gem/txt_key");
#endif
        this.img_head = this.transform.Find("tran_root/tran_top/UserLevel_Info/Image/img_head").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head, "tran_root/tran_top/UserLevel_Info/Image/img_head");
#endif
        this.txt_name = this.transform.Find("tran_root/tran_top/UserLevel_Info/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "tran_root/tran_top/UserLevel_Info/txt_name");
#endif
        this.txt_level = this.transform.Find("tran_root/tran_top/UserLevel_Info/UserLevel/txt_level").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_level, "tran_root/tran_top/UserLevel_Info/UserLevel/txt_level");
#endif
        this.sli_exp = this.transform.Find("tran_root/tran_top/UserLevel_Info/sli_exp").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_exp, "tran_root/tran_top/UserLevel_Info/sli_exp");
#endif
        this.txt_exp = this.transform.Find("tran_root/tran_top/UserLevel_Info/sli_exp/txt_exp").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_exp, "tran_root/tran_top/UserLevel_Info/sli_exp/txt_exp");
#endif
        this.tran_middle = this.transform.Find("tran_root/tran_middle");
#if UNITY_EDITOR
        this.NullAssert(this.tran_middle, "tran_root/tran_middle");
#endif

    }
}