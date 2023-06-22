//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIDragonAwardItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIDragonAwardItem.prefab";

	#region variable

    private Image img_icon = null;
    public Image Img_icon => this.img_icon;
    private Image img_quality = null;
    public Image Img_quality => this.img_quality;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.img_icon = this.transform.Find("img_icon").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_icon, "img_icon");
#endif
        this.img_quality = this.transform.Find("img_quality").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_quality, "img_quality");
#endif

    }
}