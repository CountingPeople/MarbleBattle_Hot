//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIGiftItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIGiftItem.prefab";

	#region variable

    private Button btn_buy3 = null;
    public Button Btn_buy3 => this.btn_buy3;
    private Button btn_buy2 = null;
    public Button Btn_buy2 => this.btn_buy2;
    private Text txt_price2 = null;
    public Text Txt_price2 => this.txt_price2;
    private Button btn_buy1 = null;
    public Button Btn_buy1 => this.btn_buy1;
    private Text txt_price = null;
    public Text Txt_price => this.txt_price;
    private Text txt_key = null;
    public Text Txt_key => this.txt_key;
    private Image img_icon = null;
    public Image Img_icon => this.img_icon;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_buy3 = this.transform.Find("Item (1)/btn_buy3").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_buy3, "Item (1)/btn_buy3");
#endif
        this.btn_buy2 = this.transform.Find("Item (1)/btn_buy2").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_buy2, "Item (1)/btn_buy2");
#endif
        this.txt_price2 = this.transform.Find("Item (1)/btn_buy2/Group/txt_price2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_price2, "Item (1)/btn_buy2/Group/txt_price2");
#endif
        this.btn_buy1 = this.transform.Find("Item (1)/btn_buy1").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_buy1, "Item (1)/btn_buy1");
#endif
        this.txt_price = this.transform.Find("Item (1)/btn_buy1/Group/txt_price").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_price, "Item (1)/btn_buy1/Group/txt_price");
#endif
        this.txt_key = this.transform.Find("Item (1)/txt_key").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_key, "Item (1)/txt_key");
#endif
        this.img_icon = this.transform.Find("Item (1)/img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "Item (1)/img_icon");
#endif

    }
}