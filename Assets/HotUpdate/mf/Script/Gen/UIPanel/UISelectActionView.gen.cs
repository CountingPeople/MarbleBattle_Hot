//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UISelectActionView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UISelectAction";

	#region variable

    private Transform tran_option = null;
    public Transform Tran_option => this.tran_option;
    private Button btn_delete = null;
    public Button Btn_delete => this.btn_delete;
    private Button btn_close = null;
    public Button Btn_close => this.btn_close;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_option = this.transform.Find("tran_btnList/tran_option");
#if UNITY_EDITOR
        this.NullAssert(this.tran_option, "tran_btnList/tran_option");
#endif
        this.btn_delete = this.transform.Find("tran_btnList/btn_delete").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_delete, "tran_btnList/btn_delete");
#endif
        this.btn_close = this.transform.Find("btn_close").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_close, "btn_close");
#endif

    }
}