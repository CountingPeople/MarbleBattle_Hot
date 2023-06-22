//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIHeroItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIHeroItem.prefab";

	#region variable

    private Button btn_click = null;
    public Button Btn_click => this.btn_click;
    private Transform tran_select = null;
    public Transform Tran_select => this.tran_select;
    private Image img_icon = null;
    public Image Img_icon => this.img_icon;
    private Image img_frame = null;
    public Image Img_frame => this.img_frame;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_click = this.transform.Find("btn_click").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_click, "btn_click");
#endif
        this.tran_select = this.transform.Find("tran_select");
#if UNITY_EDITOR
        this.NullAssert(this.tran_select, "tran_select");
#endif
        this.img_icon = this.transform.Find("img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "img_icon");
#endif
        this.img_frame = this.transform.Find("img_frame").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_frame, "img_frame");
#endif

    }
}