//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIDragonPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/UIDragonPanel.prefab";

	#region variable

    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Transform tran_time = null;
    public Transform Tran_time => this.tran_time;
    private Text txt_down = null;
    public Text Txt_down => this.txt_down;
    private Button btn_enter_ad = null;
    public Button Btn_enter_ad => this.btn_enter_ad;
    private Button btn_enter_free = null;
    public Button Btn_enter_free => this.btn_enter_free;
    private Text txt_lv = null;
    public Text Txt_lv => this.txt_lv;
    private Text txt_title = null;
    public Text Txt_title => this.txt_title;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_list = this.transform.Find("tran_root/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_root/tran_list");
#endif
        this.tran_time = this.transform.Find("tran_root/tran_time");
#if UNITY_EDITOR
        this.NullAssert(this.tran_time, "tran_root/tran_time");
#endif
        this.txt_down = this.transform.Find("tran_root/tran_time/txt_down").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_down, "tran_root/tran_time/txt_down");
#endif
        this.btn_enter_ad = this.transform.Find("tran_root/btn_enter_ad").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_enter_ad, "tran_root/btn_enter_ad");
#endif
        this.btn_enter_free = this.transform.Find("tran_root/btn_enter_free").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_enter_free, "tran_root/btn_enter_free");
#endif
        this.txt_lv = this.transform.Find("tran_root/Image (1)/txt_lv").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_lv, "tran_root/Image (1)/txt_lv");
#endif
        this.txt_title = this.transform.Find("tran_root/Image (1)/txt_title").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_title, "tran_root/Image (1)/txt_title");
#endif
        this.btn_close = this.transform.Find("tran_root/btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "tran_root/btn_close");
#endif

    }
}