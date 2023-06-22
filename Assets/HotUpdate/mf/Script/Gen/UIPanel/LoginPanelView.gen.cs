//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class LoginPanelView : UIPanel
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Panel/LoginPanel.prefab";

	#region variable

    private Button btn_age = null;
    public Button Btn_age => this.btn_age;
    private Toggle tog_deal = null;
    public Toggle Tog_deal => this.tog_deal;
    private Button btn_secret = null;
    public Button Btn_secret => this.btn_secret;
    private Button btn_user = null;
    public Button Btn_user => this.btn_user;
    private Button btn_start = null;
    public Button Btn_start => this.btn_start;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_age = this.transform.Find("btn_age").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_age, "btn_age");
#endif
        this.tog_deal = this.transform.Find("tog_deal").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_deal, "tog_deal");
#endif
        this.btn_secret = this.transform.Find("tog_deal/Label/btn_secret").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_secret, "tog_deal/Label/btn_secret");
#endif
        this.btn_user = this.transform.Find("tog_deal/Label/btn_user").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_user, "tog_deal/Label/btn_user");
#endif
        this.btn_start = this.transform.Find("btn_start").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_start, "btn_start");
#endif

    }
}