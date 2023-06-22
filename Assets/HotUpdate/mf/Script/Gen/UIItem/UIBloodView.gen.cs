//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBloodView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIBlood.prefab";

	#region variable

    private Transform tran_flow = null;
    public Transform Tran_flow => this.tran_flow;
    private Slider sli_blood = null;
    public Slider Sli_blood => this.sli_blood;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_flow = this.transform.Find("tran_flow");
#if UNITY_EDITOR
        this.NullAssert(this.tran_flow, "tran_flow");
#endif
        this.sli_blood = this.transform.Find("sli_blood").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_blood, "sli_blood");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif

    }
}