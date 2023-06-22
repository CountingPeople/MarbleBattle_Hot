//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIHeroDetailPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIHeroDetailPanel.prefab";

	#region variable

    private Button btn_back = null;
    public Button Btn_back => this.btn_back;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_back = this.transform.Find("TopBar/btn_back").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_back, "TopBar/btn_back");
#endif

    }
}