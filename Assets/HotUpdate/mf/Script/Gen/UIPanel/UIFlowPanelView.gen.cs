//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIFlowPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIFlowPanel.prefab";

	#region variable

    private Transform tran_root = null;
    public Transform Tran_root => this.tran_root;
    private Transform tran_middle = null;
    public Transform Tran_middle => this.tran_middle;
    private Transform tran_tip = null;
    public Transform Tran_tip => this.tran_tip;
    private Text txt_content = null;
    public Text Txt_content => this.txt_content;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_root = this.transform.Find("tran_root");
#if UNITY_EDITOR
        this.NullAssert(this.tran_root, "tran_root");
#endif
        this.tran_middle = this.transform.Find("tran_root/tran_middle");
#if UNITY_EDITOR
        this.NullAssert(this.tran_middle, "tran_root/tran_middle");
#endif
        this.tran_tip = this.transform.Find("tran_root/tran_middle/tran_tip");
#if UNITY_EDITOR
        this.NullAssert(this.tran_tip, "tran_root/tran_middle/tran_tip");
#endif
        this.txt_content = this.transform.Find("tran_root/tran_middle/tran_tip/txt_content").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_content, "tran_root/tran_middle/tran_tip/txt_content");
#endif

    }
}