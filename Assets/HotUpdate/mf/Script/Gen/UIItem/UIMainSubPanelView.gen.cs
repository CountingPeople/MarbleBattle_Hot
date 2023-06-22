//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIMainSubPanelView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/SubPanel/UIMainSubPanel.prefab";

	#region variable

    private Button btn_rank = null;
    public Button Btn_rank => this.btn_rank;
    private Button btn_achive = null;
    public Button Btn_achive => this.btn_achive;
    private Transform tran_dragon = null;
    public Transform Tran_dragon => this.tran_dragon;
    private Text txt_dragon = null;
    public Text Txt_dragon => this.txt_dragon;
    private Button btn_dragon = null;
    public Button Btn_dragon => this.btn_dragon;
    private Transform tran_battle = null;
    public Transform Tran_battle => this.tran_battle;
    private Text txt_battle = null;
    public Text Txt_battle => this.txt_battle;
    private Button btn_battle = null;
    public Button Btn_battle => this.btn_battle;
    private Button btn_get = null;
    public Button Btn_get => this.btn_get;
    private Text txt_challenge = null;
    public Text Txt_challenge => this.txt_challenge;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_rank = this.transform.Find("btn_rank").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_rank, "btn_rank");
#endif
        this.btn_achive = this.transform.Find("btn_achive").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_achive, "btn_achive");
#endif
        this.tran_dragon = this.transform.Find("GameObject_2/tran_dragon");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dragon, "GameObject_2/tran_dragon");
#endif
        this.txt_dragon = this.transform.Find("GameObject_2/tran_dragon/txt_dragon").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_dragon, "GameObject_2/tran_dragon/txt_dragon");
#endif
        this.btn_dragon = this.transform.Find("GameObject_2/btn_dragon").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_dragon, "GameObject_2/btn_dragon");
#endif
        this.tran_battle = this.transform.Find("GameObject_1/tran_battle");
#if UNITY_EDITOR
        this.NullAssert(this.tran_battle, "GameObject_1/tran_battle");
#endif
        this.txt_battle = this.transform.Find("GameObject_1/tran_battle/txt_battle").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_battle, "GameObject_1/tran_battle/txt_battle");
#endif
        this.btn_battle = this.transform.Find("GameObject_1/btn_battle").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_battle, "GameObject_1/btn_battle");
#endif
        this.btn_get = this.transform.Find("btn_get").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_get, "btn_get");
#endif
        this.txt_challenge = this.transform.Find("btn_get/txt_challenge").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_challenge, "btn_get/txt_challenge");
#endif

    }
}