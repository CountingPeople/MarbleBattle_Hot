//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIEnemyPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIEnemyPanel.prefab";

	#region variable

    private RawImage raw_enemy = null;
    public RawImage Raw_enemy => this.raw_enemy;
    private RawImage raw_my = null;
    public RawImage Raw_my => this.raw_my;
    private Button btn_start = null;
    public Button Btn_start => this.btn_start;
    private Button btn_change = null;
    public Button Btn_change => this.btn_change;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;
    private Text txt_level2 = null;
    public Text Txt_level2 => this.txt_level2;
    private Text txt_hp2 = null;
    public Text Txt_hp2 => this.txt_hp2;
    private Text txt_power2 = null;
    public Text Txt_power2 => this.txt_power2;
    private Text txt_name2 = null;
    public Text Txt_name2 => this.txt_name2;
    private Text txt_level1 = null;
    public Text Txt_level1 => this.txt_level1;
    private Text txt_hp1 = null;
    public Text Txt_hp1 => this.txt_hp1;
    private Text txt_power1 = null;
    public Text Txt_power1 => this.txt_power1;
    private Text txt_name1 = null;
    public Text Txt_name1 => this.txt_name1;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.raw_enemy = this.transform.Find("raw_enemy").GetComponent<RawImage>();
#if UNITY_EDITOR
        this.NullAssert(this.raw_enemy, "raw_enemy");
#endif
        this.raw_my = this.transform.Find("raw_my").GetComponent<RawImage>();
#if UNITY_EDITOR
        this.NullAssert(this.raw_my, "raw_my");
#endif
        this.btn_start = this.transform.Find("btn_start").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_start, "btn_start");
#endif
        this.btn_change = this.transform.Find("btn_change").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_change, "btn_change");
#endif
        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif
        this.txt_level2 = this.transform.Find("tran_enemy/Image (2)/txt_level2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_level2, "tran_enemy/Image (2)/txt_level2");
#endif
        this.txt_hp2 = this.transform.Find("tran_enemy/txt_hp2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_hp2, "tran_enemy/txt_hp2");
#endif
        this.txt_power2 = this.transform.Find("tran_enemy/txt_power2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_power2, "tran_enemy/txt_power2");
#endif
        this.txt_name2 = this.transform.Find("tran_enemy/txt_name2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name2, "tran_enemy/txt_name2");
#endif
        this.txt_level1 = this.transform.Find("tran_my/Image (2)/txt_level1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_level1, "tran_my/Image (2)/txt_level1");
#endif
        this.txt_hp1 = this.transform.Find("tran_my/txt_hp1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_hp1, "tran_my/txt_hp1");
#endif
        this.txt_power1 = this.transform.Find("tran_my/txt_power1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_power1, "tran_my/txt_power1");
#endif
        this.txt_name1 = this.transform.Find("tran_my/txt_name1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name1, "tran_my/txt_name1");
#endif

    }
}