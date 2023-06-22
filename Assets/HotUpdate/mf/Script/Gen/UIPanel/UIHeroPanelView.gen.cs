//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIHeroPanelView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UIHeroPanel";

	#region variable

    private Transform tran_root = null;
    public Transform Tran_root => this.tran_root;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Toggle tog_all = null;
    public Toggle Tog_all => this.tog_all;
    private Toggle tog_hero = null;
    public Toggle Tog_hero => this.tog_hero;
    private Button btn_back = null;
    public Button Btn_back => this.btn_back;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_root = this.transform.Find("tran_root");
#if UNITY_EDITOR
        this.NullAssert(this.tran_root, "tran_root");
#endif
        this.tran_list = this.transform.Find("tran_root/tran_middle/tran_sclv/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_root/tran_middle/tran_sclv/Viewport/tran_list");
#endif
        this.tog_all = this.transform.Find("tran_root/tran_left/tran_list/tog_all").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_all, "tran_root/tran_left/tran_list/tog_all");
#endif
        this.tog_hero = this.transform.Find("tran_root/tran_left/tran_list/tog_hero").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_hero, "tran_root/tran_left/tran_list/tog_hero");
#endif
        this.btn_back = this.transform.Find("tran_root/btn_back").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_back, "tran_root/btn_back");
#endif

    }
}