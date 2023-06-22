//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIResultPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIResultPanel.prefab";

	#region variable

    private Transform tran_fail = null;
    public Transform Tran_fail => this.tran_fail;
    private Transform tran_win = null;
    public Transform Tran_win => this.tran_win;
    private Transform tran_down = null;
    public Transform Tran_down => this.tran_down;
    private Text txt_cond2 = null;
    public Text Txt_cond2 => this.txt_cond2;
    private Text txt_cond1 = null;
    public Text Txt_cond1 => this.txt_cond1;
    private Transform tran_winInfo = null;
    public Transform Tran_winInfo => this.tran_winInfo;
    private Transform tran_key = null;
    public Transform Tran_key => this.tran_key;
    private Text txt_keyCount = null;
    public Text Txt_keyCount => this.txt_keyCount;
    private Button btn_back = null;
    public Button Btn_back => this.btn_back;
    private Text txt_back = null;
    public Text Txt_back => this.txt_back;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_fail = this.transform.Find("Result/tran_fail");
#if UNITY_EDITOR
        this.NullAssert(this.tran_fail, "Result/tran_fail");
#endif
        this.tran_win = this.transform.Find("Result/tran_win");
#if UNITY_EDITOR
        this.NullAssert(this.tran_win, "Result/tran_win");
#endif
        this.tran_down = this.transform.Find("Result/tran_win/tran_content/tran_down");
#if UNITY_EDITOR
        this.NullAssert(this.tran_down, "Result/tran_win/tran_content/tran_down");
#endif
        this.txt_cond2 = this.transform.Find("Result/tran_win/tran_content/tran_down/txt_cond2").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_cond2, "Result/tran_win/tran_content/tran_down/txt_cond2");
#endif
        this.txt_cond1 = this.transform.Find("Result/tran_win/tran_content/tran_down/txt_cond1").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_cond1, "Result/tran_win/tran_content/tran_down/txt_cond1");
#endif
        this.tran_winInfo = this.transform.Find("Result/tran_win/tran_content/tran_winInfo");
#if UNITY_EDITOR
        this.NullAssert(this.tran_winInfo, "Result/tran_win/tran_content/tran_winInfo");
#endif
        this.tran_key = this.transform.Find("Result/tran_win/tran_content/tran_winInfo/tran_key");
#if UNITY_EDITOR
        this.NullAssert(this.tran_key, "Result/tran_win/tran_content/tran_winInfo/tran_key");
#endif
        this.txt_keyCount = this.transform.Find("Result/tran_win/tran_content/tran_winInfo/tran_key/txt_keyCount").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_keyCount, "Result/tran_win/tran_content/tran_winInfo/tran_key/txt_keyCount");
#endif
        this.btn_back = this.transform.Find("Result/btn_back").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_back, "Result/btn_back");
#endif
        this.txt_back = this.transform.Find("Result/btn_back/txt_back").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_back, "Result/btn_back/txt_back");
#endif

    }
}