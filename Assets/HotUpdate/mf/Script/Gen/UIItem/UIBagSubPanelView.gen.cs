//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBagSubPanelView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/SubPanel/UIBagSubPanel.prefab";

	#region variable

    private Button btn_sure = null;
    public Button Btn_sure => this.btn_sure;
    private Button btn_dis = null;
    public Button Btn_dis => this.btn_dis;
    private Slider sli_exp = null;
    public Slider Sli_exp => this.sli_exp;
    private Text txt_exp = null;
    public Text Txt_exp => this.txt_exp;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_sure = this.transform.Find("tran_bottom/btn_sure").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_sure, "tran_bottom/btn_sure");
#endif
        this.btn_dis = this.transform.Find("tran_bottom/btn_dis").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_dis, "tran_bottom/btn_dis");
#endif
        this.sli_exp = this.transform.Find("tran_bottom/sli_exp").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_exp, "tran_bottom/sli_exp");
#endif
        this.txt_exp = this.transform.Find("tran_bottom/sli_exp/txt_exp").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_exp, "tran_bottom/sli_exp/txt_exp");
#endif
        this.tran_list = this.transform.Find("tran_middle/tran_sclv/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_middle/tran_sclv/Viewport/tran_list");
#endif

    }
}