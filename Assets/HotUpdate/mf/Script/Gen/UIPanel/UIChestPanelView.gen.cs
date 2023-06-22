//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIChestPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIChestPanel.prefab";

	#region variable

    private Button btn_get = null;
    public Button Btn_get => this.btn_get;
    private Text txt_price = null;
    public Text Txt_price => this.txt_price;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;
    private Transform tran_quality = null;
    public Transform Tran_quality => this.tran_quality;
    private Text txt_count = null;
    public Text Txt_count => this.txt_count;
    private Image img_chest = null;
    public Image Img_chest => this.img_chest;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_get = this.transform.Find("tran_root/btn_get").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_get, "tran_root/btn_get");
#endif
        this.txt_price = this.transform.Find("tran_root/btn_get/txt_price").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_price, "tran_root/btn_get/txt_price");
#endif
        this.btn_close = this.transform.Find("tran_root/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "tran_root/btn_close");
#endif
        this.tran_quality = this.transform.Find("tran_root/tran_quality");
#if UNITY_EDITOR
        this.NullAssert(this.tran_quality, "tran_root/tran_quality");
#endif
        this.txt_count = this.transform.Find("tran_root/txt_count").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_count, "tran_root/txt_count");
#endif
        this.img_chest = this.transform.Find("tran_root/img_chest").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_chest, "tran_root/img_chest");
#endif
        this.txt_name = this.transform.Find("tran_root/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "tran_root/txt_name");
#endif

    }
}