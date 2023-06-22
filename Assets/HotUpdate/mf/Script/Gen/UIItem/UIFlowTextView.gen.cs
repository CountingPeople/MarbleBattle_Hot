//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIFlowTextView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIFlowText.prefab";

	#region variable

    private Text txt_value = null;
    public Text Txt_value => this.txt_value;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_value = this.transform.Find("txt_value").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_value, "txt_value");
#endif

    }
}