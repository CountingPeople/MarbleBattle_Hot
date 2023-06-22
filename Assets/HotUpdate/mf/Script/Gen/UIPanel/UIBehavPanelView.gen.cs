//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIBehavPanelView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UIBehavPanel";

	#region variable

    private Dropdown drop_select = null;
    public Dropdown Drop_select => this.drop_select;
    private InputField inp_name = null;
    public InputField Inp_name => this.inp_name;
    private Button btn_import = null;
    public Button Btn_import => this.btn_import;
    private Button btn_export = null;
    public Button Btn_export => this.btn_export;
    private Button btn_execute = null;
    public Button Btn_execute => this.btn_execute;
    private Transform tran_logic = null;
    public Transform Tran_logic => this.tran_logic;
    private Transform tran_mouseRect = null;
    public Transform Tran_mouseRect => this.tran_mouseRect;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.drop_select = this.transform.Find("tran_btn/drop_select").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_select, "tran_btn/drop_select");
#endif
        this.inp_name = this.transform.Find("tran_btn/inp_name").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_name, "tran_btn/inp_name");
#endif
        this.btn_import = this.transform.Find("tran_btn/btn_import").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_import, "tran_btn/btn_import");
#endif
        this.btn_export = this.transform.Find("tran_btn/btn_export").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_export, "tran_btn/btn_export");
#endif
        this.btn_execute = this.transform.Find("tran_btn/btn_execute").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_execute, "tran_btn/btn_execute");
#endif
        this.tran_logic = this.transform.Find("tran_sclv/Viewport/tran_logic");
#if UNITY_EDITOR
        this.NullAssert(this.tran_logic, "tran_sclv/Viewport/tran_logic");
#endif
        this.tran_mouseRect = this.transform.Find("tran_mouseRect");
#if UNITY_EDITOR
        this.NullAssert(this.tran_mouseRect, "tran_mouseRect");
#endif

    }
}