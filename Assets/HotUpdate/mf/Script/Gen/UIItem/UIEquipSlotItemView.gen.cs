//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIEquipSlotItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIEquipSlotItem.prefab";

	#region variable

    private Button btn_click = null;
    public Button Btn_click => this.btn_click;
    private Transform tran_have = null;
    public Transform Tran_have => this.tran_have;
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
    private GameObject icon_slot = null;
    public GameObject Icon_slot => this.icon_slot;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_click = this.transform.Find("btn_click").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_click, "btn_click");
#endif
        this.tran_have = this.transform.Find("tran_have");
#if UNITY_EDITOR
        this.NullAssert(this.tran_have, "tran_have");
#endif
        this.tran_lock = this.transform.Find("tran_have/equipInfo/tran_lock");
#if UNITY_EDITOR
        this.NullAssert(this.tran_lock, "tran_have/equipInfo/tran_lock");
#endif
        this.tran_star5 = this.transform.Find("tran_have/equipInfo/tran_star/tran_star5");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star5, "tran_have/equipInfo/tran_star/tran_star5");
#endif
        this.tran_star4 = this.transform.Find("tran_have/equipInfo/tran_star/tran_star4");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star4, "tran_have/equipInfo/tran_star/tran_star4");
#endif
        this.tran_star3 = this.transform.Find("tran_have/equipInfo/tran_star/tran_star3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star3, "tran_have/equipInfo/tran_star/tran_star3");
#endif
        this.tran_star2 = this.transform.Find("tran_have/equipInfo/tran_star/tran_star2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star2, "tran_have/equipInfo/tran_star/tran_star2");
#endif
        this.tran_star1 = this.transform.Find("tran_have/equipInfo/tran_star/tran_star1");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star1, "tran_have/equipInfo/tran_star/tran_star1");
#endif
        this.img_skill = this.transform.Find("tran_have/equipInfo/tran_bottom/img_skill").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_skill, "tran_have/equipInfo/tran_bottom/img_skill");
#endif
        this.txt_prop = this.transform.Find("tran_have/equipInfo/tran_bottom/txt_prop").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop, "tran_have/equipInfo/tran_bottom/txt_prop");
#endif
        this.img_prop = this.transform.Find("tran_have/equipInfo/tran_bottom/img_prop").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_prop, "tran_have/equipInfo/tran_bottom/img_prop");
#endif
        this.img_frame = this.transform.Find("tran_have/equipInfo/img_frame").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_frame, "tran_have/equipInfo/img_frame");
#endif
        this.img_icon = this.transform.Find("tran_have/equipInfo/img_frame/img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "tran_have/equipInfo/img_frame/img_icon");
#endif
        this.icon_slot = this.transform.Find("icon_slot").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.icon_slot, "icon_slot");
#endif

    }
}