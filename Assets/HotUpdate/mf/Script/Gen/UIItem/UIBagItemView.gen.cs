//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBagItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIBagItem.prefab";

	#region variable

    private Transform tran_grid = null;
    public Transform Tran_grid => this.tran_grid;
    private Transform tran_info = null;
    public Transform Tran_info => this.tran_info;
    private Transform tran_dis = null;
    public Transform Tran_dis => this.tran_dis;
    private Button btn_dis = null;
    public Button Btn_dis => this.btn_dis;
    private Transform tran_select = null;
    public Transform Tran_select => this.tran_select;
    private Button btn_click = null;
    public Button Btn_click => this.btn_click;
    private Transform tran_lock = null;
    public Transform Tran_lock => this.tran_lock;
    private Transform tran_star5 = null;
    public Transform Tran_star5 => this.tran_star5;
    private Transform tran_star4 = null;
    public Transform Tran_star4 => this.tran_star4;
    private Transform tran_star3 = null;
    public Transform Tran_star3 => this.tran_star3;
    private Transform tran_star2 = null;
    public Transform Tran_star2 => this.tran_star2;
    private Transform tran_star1 = null;
    public Transform Tran_star1 => this.tran_star1;
    private Image img_skill = null;
    public Image Img_skill => this.img_skill;
    private Text txt_prop = null;
    public Text Txt_prop => this.txt_prop;
    private Image img_prop = null;
    public Image Img_prop => this.img_prop;
    private Image img_frame = null;
    public Image Img_frame => this.img_frame;
    private Image img_icon = null;
    public Image Img_icon => this.img_icon;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_grid = this.transform.Find("tran_grid");
#if UNITY_EDITOR
        this.NullAssert(this.tran_grid, "tran_grid");
#endif
        this.tran_info = this.transform.Find("tran_info");
#if UNITY_EDITOR
        this.NullAssert(this.tran_info, "tran_info");
#endif
        this.tran_dis = this.transform.Find("tran_info/tran_dis");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dis, "tran_info/tran_dis");
#endif
        this.btn_dis = this.transform.Find("tran_info/tran_dis/btn_dis").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_dis, "tran_info/tran_dis/btn_dis");
#endif
        this.tran_select = this.transform.Find("tran_info/tran_select");
#if UNITY_EDITOR
        this.NullAssert(this.tran_select, "tran_info/tran_select");
#endif
        this.btn_click = this.transform.Find("tran_info/btn_click").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_click, "tran_info/btn_click");
#endif
        this.tran_lock = this.transform.Find("tran_info/equipInfo/tran_lock");
#if UNITY_EDITOR
        this.NullAssert(this.tran_lock, "tran_info/equipInfo/tran_lock");
#endif
        this.tran_star5 = this.transform.Find("tran_info/equipInfo/tran_star/tran_star5");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star5, "tran_info/equipInfo/tran_star/tran_star5");
#endif
        this.tran_star4 = this.transform.Find("tran_info/equipInfo/tran_star/tran_star4");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star4, "tran_info/equipInfo/tran_star/tran_star4");
#endif
        this.tran_star3 = this.transform.Find("tran_info/equipInfo/tran_star/tran_star3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star3, "tran_info/equipInfo/tran_star/tran_star3");
#endif
        this.tran_star2 = this.transform.Find("tran_info/equipInfo/tran_star/tran_star2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star2, "tran_info/equipInfo/tran_star/tran_star2");
#endif
        this.tran_star1 = this.transform.Find("tran_info/equipInfo/tran_star/tran_star1");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star1, "tran_info/equipInfo/tran_star/tran_star1");
#endif
        this.img_skill = this.transform.Find("tran_info/equipInfo/tran_bottom/img_skill").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_skill, "tran_info/equipInfo/tran_bottom/img_skill");
#endif
        this.txt_prop = this.transform.Find("tran_info/equipInfo/tran_bottom/txt_prop").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop, "tran_info/equipInfo/tran_bottom/txt_prop");
#endif
        this.img_prop = this.transform.Find("tran_info/equipInfo/tran_bottom/img_prop").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_prop, "tran_info/equipInfo/tran_bottom/img_prop");
#endif
        this.img_frame = this.transform.Find("tran_info/equipInfo/img_frame").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_frame, "tran_info/equipInfo/img_frame");
#endif
        this.img_icon = this.transform.Find("tran_info/equipInfo/img_frame/img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "tran_info/equipInfo/img_frame/img_icon");
#endif

    }
}