//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UICreateBehaItemView : UIItem
{
    public override string UIPath => "prefab/ui/item/UICreateBehaItem";

	#region variable

    private Button btn_add = null;
    public Button Btn_add => this.btn_add;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_add = this.transform.Find("btn_add").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_add, "btn_add");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif

    }
}