//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIHeroSubPanelView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/SubPanel/UIHeroSubPanel.prefab";

	#region variable

    private Text txt_prop10 = null;
    public Text Txt_prop10 => this.txt_prop10;
    private Text txt_prop9 = null;
    public Text Txt_prop9 => this.txt_prop9;
    private Text txt_prop8 = null;
    public Text Txt_prop8 => this.txt_prop8;
    private Text txt_prop7 = null;
    public Text Txt_prop7 => this.txt_prop7;
    private Text txt_prop6 = null;
    public Text Txt_prop6 => this.txt_prop6;
    private Text txt_prop5 = null;
    public Text Txt_prop5 => this.txt_prop5;
    private Text txt_prop4 = null;
    public Text Txt_prop4 => this.txt_prop4;
    private Text txt_prop3 = null;
    public Text Txt_prop3 => this.txt_prop3;
    private Text txt_prop2 = null;
    public Text Txt_prop2 => this.txt_prop2;
    private Text txt_prop1 = null;
    public Text Txt_prop1 => this.txt_prop1;
    private Transform tran_pos6 = null;
    public Transform Tran_pos6 => this.tran_pos6;
    private Transform tran_pos2 = null;
    public Transform Tran_pos2 => this.tran_pos2;
    private Transform tran_pos4 = null;
    public Transform Tran_pos4 => this.tran_pos4;
    private Transform tran_pos5 = null;
    public Transform Tran_pos5 => this.tran_pos5;
    private Transform tran_pos3 = null;
    public Transform Tran_pos3 => this.tran_pos3;
    private Transform tran_pos1 = null;
    public Transform Tran_pos1 => this.tran_pos1;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_prop10 = this.transform.Find("tran_bottom/tran_propList/txt_prop10").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop10, "tran_bottom/tran_propList/txt_prop10");
#endif
        this.txt_prop9 = this.transform.Find("tran_bottom/tran_propList/txt_prop9").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop9, "tran_bottom/tran_propList/txt_prop9");
#endif
        this.txt_prop8 = this.transform.Find("tran_bottom/tran_propList/txt_prop8").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop8, "tran_bottom/tran_propList/txt_prop8");
#endif
        this.txt_prop7 = this.transform.Find("tran_bottom/tran_propList/txt_prop7").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop7, "tran_bottom/tran_propList/txt_prop7");
#endif
        this.txt_prop6 = this.transform.Find("tran_bottom/tran_propList/txt_prop6").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop6, "tran_bottom/tran_propList/txt_prop6");
#endif
        this.txt_prop5 = this.transform.Find("tran_bottom/tran_propList/txt_prop5").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop5, "tran_bottom/tran_propList/txt_prop5");
#endif
        this.txt_prop4 = this.transform.Find("tran_bottom/tran_propList/txt_prop4").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop4, "tran_bottom/tran_propList/txt_prop4");
#endif
        this.txt_prop3 = this.transform.Find("tran_bottom/tran_propList/txt_prop3").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop3, "tran_bottom/tran_propList/txt_prop3");
#endif
        this.txt_prop2 = this.transform.Find("tran_bottom/txt_prop2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop2, "tran_bottom/txt_prop2");
#endif
        this.txt_prop1 = this.transform.Find("tran_bottom/txt_prop1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_prop1, "tran_bottom/txt_prop1");
#endif
        this.tran_pos6 = this.transform.Find("tran_right/tran_pos6");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos6, "tran_right/tran_pos6");
#endif
        this.tran_pos2 = this.transform.Find("tran_right/tran_pos2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos2, "tran_right/tran_pos2");
#endif
        this.tran_pos4 = this.transform.Find("tran_right/tran_pos4");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos4, "tran_right/tran_pos4");
#endif
        this.tran_pos5 = this.transform.Find("tran_left/tran_pos5");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos5, "tran_left/tran_pos5");
#endif
        this.tran_pos3 = this.transform.Find("tran_left/tran_pos3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos3, "tran_left/tran_pos3");
#endif
        this.tran_pos1 = this.transform.Find("tran_left/tran_pos1");
#if UNITY_EDITOR
        this.NullAssert(this.tran_pos1, "tran_left/tran_pos1");
#endif

    }
}