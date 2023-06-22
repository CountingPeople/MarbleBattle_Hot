//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class TestPanelView : UIPanel
{
    public override string UIPath => "Prefab/UI/Panel/TestPanel";

	#region variable

    private Text txt_test = null;
    public Text Txt_test => this.txt_test;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_test = this.transform.Find("txt_test").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_test, "txt_test");
#endif

    }
}