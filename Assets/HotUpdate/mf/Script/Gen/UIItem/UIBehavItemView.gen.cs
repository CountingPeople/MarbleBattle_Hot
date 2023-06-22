//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBehavItemView : UIItem
{
    public override string UIPath => "prefab/ui/item/UIBehavItem";

	#region variable

    private Slider sli_progress = null;
    public Slider Sli_progress => this.sli_progress;
    private Transform tran_script = null;
    public Transform Tran_script => this.tran_script;
    private Transform tran_child = null;
    public Transform Tran_child => this.tran_child;
    private Button btn_add = null;
    public Button Btn_add => this.btn_add;
    private Button btn_remove = null;
    public Button Btn_remove => this.btn_remove;
    private Transform tran_start = null;
    public Transform Tran_start => this.tran_start;
    private Transform tran_end = null;
    public Transform Tran_end => this.tran_end;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Transform tran_bg = null;
    public Transform Tran_bg => this.tran_bg;
    private Transform tran_select = null;
    public Transform Tran_select => this.tran_select;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.sli_progress = this.transform.Find("sli_progress").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_progress, "sli_progress");
#endif
        this.tran_script = this.transform.Find("tran_script");
#if UNITY_EDITOR
        this.NullAssert(this.tran_script, "tran_script");
#endif
        this.tran_child = this.transform.Find("tran_child");
#if UNITY_EDITOR
        this.NullAssert(this.tran_child, "tran_child");
#endif
        this.btn_add = this.transform.Find("btn_add").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_add, "btn_add");
#endif
        this.btn_remove = this.transform.Find("btn_remove").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_remove, "btn_remove");
#endif
        this.tran_start = this.transform.Find("tran_start");
#if UNITY_EDITOR
        this.NullAssert(this.tran_start, "tran_start");
#endif
        this.tran_end = this.transform.Find("tran_end");
#if UNITY_EDITOR
        this.NullAssert(this.tran_end, "tran_end");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif
        this.tran_bg = this.transform.Find("tran_bg");
#if UNITY_EDITOR
        this.NullAssert(this.tran_bg, "tran_bg");
#endif
        this.tran_select = this.transform.Find("tran_select");
#if UNITY_EDITOR
        this.NullAssert(this.tran_select, "tran_select");
#endif

    }
}