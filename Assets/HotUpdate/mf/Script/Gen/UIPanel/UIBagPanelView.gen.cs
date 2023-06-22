//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBagPanelView : UIPanel
{

    public override string UIPath => "prefab/ui/panel/UIBagPanel";

	#region variable

    private Text txt_text = null;
    public Text Txt_text => this.txt_text;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_text = this.transform.Find("txt_text").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_text, "txt_text");
#endif
        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif

    }
}