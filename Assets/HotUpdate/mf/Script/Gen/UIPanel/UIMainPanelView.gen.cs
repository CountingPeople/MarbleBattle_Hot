//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIMainPanelView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UIMainPanel";

	#region variable

    private Button btn_start = null;
    public Button Btn_start => this.btn_start;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_start = this.transform.Find("btn_start").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_start, "btn_start");
#endif

    }
}