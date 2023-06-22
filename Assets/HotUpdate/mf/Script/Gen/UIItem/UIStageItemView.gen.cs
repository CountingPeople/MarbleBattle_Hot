//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIStageItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIStageItem.prefab";

	#region variable

    private Button btn_click = null;
    public Button Btn_click => this.btn_click;
    private Transform tran_state3 = null;
    public Transform Tran_state3 => this.tran_state3;
    private Transform tran_state2 = null;
    public Transform Tran_state2 => this.tran_state2;
    private Text txt_stage2 = null;
    public Text Txt_stage2 => this.txt_stage2;
    private Text txt_stage1 = null;
    public Text Txt_stage1 => this.txt_stage1;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_click = this.transform.Find("btn_click").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_click, "btn_click");
#endif
        this.tran_state3 = this.transform.Find("tran_state3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_state3, "tran_state3");
#endif
        this.tran_state2 = this.transform.Find("tran_state2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_state2, "tran_state2");
#endif
        this.txt_stage2 = this.transform.Find("tran_state2/txt_stage2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_stage2, "tran_state2/txt_stage2");
#endif
        this.txt_stage1 = this.transform.Find("txt_stage1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_stage1, "txt_stage1");
#endif

    }
}