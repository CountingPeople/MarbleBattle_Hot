//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIRankItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIRankItem.prefab";

	#region variable

    private Button btn_battle = null;
    public Button Btn_battle => this.btn_battle;
    private Image img_head = null;
    public Image Img_head => this.img_head;
    private Text txt_power = null;
    public Text Txt_power => this.txt_power;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;
    private Text txt_rank = null;
    public Text Txt_rank => this.txt_rank;
    private Transform tran_myRank = null;
    public Transform Tran_myRank => this.tran_myRank;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_battle = this.transform.Find("btn_battle").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_battle, "btn_battle");
#endif
        this.img_head = this.transform.Find("img_head").GetComponent<Image>();
#if UNITY_EDITOR
        this.NullAssert(this.img_head, "img_head");
#endif
        this.txt_power = this.transform.Find("txt_power").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_power, "txt_power");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif
        this.txt_rank = this.transform.Find("txt_rank").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_rank, "txt_rank");
#endif
        this.tran_myRank = this.transform.Find("tran_myRank");
#if UNITY_EDITOR
        this.NullAssert(this.tran_myRank, "tran_myRank");
#endif

    }
}