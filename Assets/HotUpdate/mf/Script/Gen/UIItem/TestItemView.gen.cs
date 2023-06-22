//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class TestItemView : UIItem
{
    public override string UIPath => "Prefab/UI/Item/TestItem";

	#region variable

    private Image img_aaa = null;
    public Image Img_aaa => this.img_aaa;
    private Text txt_test = null;
    public Text Txt_test => this.txt_test;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.img_aaa = this.transform.Find("img_aaa").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_aaa, "img_aaa");
#endif
        this.txt_test = this.transform.Find("txt_test").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_test, "txt_test");
#endif

    }
}