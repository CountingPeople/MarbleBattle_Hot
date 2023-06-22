//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class LoadPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/LoadPanel.prefab";

	#region variable

    private Slider sli_slider = null;
    public Slider Sli_slider => this.sli_slider;
    private Text txt_progress = null;
    public Text Txt_progress => this.txt_progress;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.sli_slider = this.transform.Find("sli_slider").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_slider, "sli_slider");
#endif
        this.txt_progress = this.transform.Find("sli_slider/txt_progress").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_progress, "sli_slider/txt_progress");
#endif

    }
}