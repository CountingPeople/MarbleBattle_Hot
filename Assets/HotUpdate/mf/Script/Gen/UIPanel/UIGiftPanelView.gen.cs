//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIGiftPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIGiftPanel.prefab";

	#region variable

    private Text txt_count = null;
    public Text Txt_count => this.txt_count;
    private Button btn_get = null;
    public Button Btn_get => this.btn_get;
    private Image img_gitf = null;
    public Image Img_gitf => this.img_gitf;
    private Image img_quality = null;
    public Image Img_quality => this.img_quality;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_count = this.transform.Find("tran_root/txt_count").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_count, "tran_root/txt_count");
#endif
        this.btn_get = this.transform.Find("tran_root/btn_get").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_get, "tran_root/btn_get");
#endif
        this.img_gitf = this.transform.Find("tran_root/img_gitf").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_gitf, "tran_root/img_gitf");
#endif
        this.img_quality = this.transform.Find("tran_root/img_quality").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_quality, "tran_root/img_quality");
#endif
        this.txt_name = this.transform.Find("tran_root/txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "tran_root/txt_name");
#endif

    }
}