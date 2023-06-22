//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIFormationPanelView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UIFormationPanel";

	#region variable

    private Transform tran_root = null;
    public Transform Tran_root => this.tran_root;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Button btn_battle = null;
    public Button Btn_battle => this.btn_battle;
    private GameObject raw_player5 = null;
    public GameObject Raw_player5 => this.raw_player5;
    private GameObject raw_player4 = null;
    public GameObject Raw_player4 => this.raw_player4;
    private GameObject raw_player3 = null;
    public GameObject Raw_player3 => this.raw_player3;
    private GameObject raw_player2 = null;
    public GameObject Raw_player2 => this.raw_player2;
    private GameObject raw_player1 = null;
    public GameObject Raw_player1 => this.raw_player1;
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
        this.tran_list = this.transform.Find("tran_root/tran_bottom/tran_sclv/Viewport/tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_root/tran_bottom/tran_sclv/Viewport/tran_list");
#endif
        this.btn_battle = this.transform.Find("tran_root/tran_bottom/btn_battle").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_battle, "tran_root/tran_bottom/btn_battle");
#endif
        this.raw_player5 = this.transform.Find("tran_root/tran_middle/raw_player5").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.raw_player5, "tran_root/tran_middle/raw_player5");
#endif
        this.raw_player4 = this.transform.Find("tran_root/tran_middle/raw_player4").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.raw_player4, "tran_root/tran_middle/raw_player4");
#endif
        this.raw_player3 = this.transform.Find("tran_root/tran_middle/raw_player3").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.raw_player3, "tran_root/tran_middle/raw_player3");
#endif
        this.raw_player2 = this.transform.Find("tran_root/tran_middle/raw_player2").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.raw_player2, "tran_root/tran_middle/raw_player2");
#endif
        this.raw_player1 = this.transform.Find("tran_root/tran_middle/raw_player1").gameObject;
#if UNITY_EDITOR
        this.NullAssert(this.raw_player1, "tran_root/tran_middle/raw_player1");
#endif
        this.btn_back = this.transform.Find("tran_root/btn_back").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_back, "tran_root/btn_back");
#endif

    }
}