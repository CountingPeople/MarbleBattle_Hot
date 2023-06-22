//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIMissionItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIMissionItem.prefab";

	#region variable

    private Text txt_num = null;
    public Text Txt_num => this.txt_num;
    private Transform tran_list = null;
    public Transform Tran_list => this.tran_list;
    private Transform tran_Stage4 = null;
    public Transform Tran_Stage4 => this.tran_Stage4;
    private Transform tran_Stage3 = null;
    public Transform Tran_Stage3 => this.tran_Stage3;
    private Transform tran_Stage2 = null;
    public Transform Tran_Stage2 => this.tran_Stage2;
    private Transform tran_star3 = null;
    public Transform Tran_star3 => this.tran_star3;
    private Transform tran_star2 = null;
    public Transform Tran_star2 => this.tran_star2;
    private Transform tran_star1 = null;
    public Transform Tran_star1 => this.tran_star1;
    private Transform tran_Stage1 = null;
    public Transform Tran_Stage1 => this.tran_Stage1;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.txt_num = this.transform.Find("txt_num").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_num, "txt_num");
#endif
        this.tran_list = this.transform.Find("tran_list");
#if UNITY_EDITOR
        this.NullAssert(this.tran_list, "tran_list");
#endif
        this.tran_Stage4 = this.transform.Find("tran_list/tran_Stage4");
#if UNITY_EDITOR
        this.NullAssert(this.tran_Stage4, "tran_list/tran_Stage4");
#endif
        this.tran_Stage3 = this.transform.Find("tran_list/tran_Stage3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_Stage3, "tran_list/tran_Stage3");
#endif
        this.tran_Stage2 = this.transform.Find("tran_list/tran_Stage2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_Stage2, "tran_list/tran_Stage2");
#endif
        this.tran_star3 = this.transform.Find("tran_list/tran_Stage2/tran_starList/Star_On/tran_star3");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star3, "tran_list/tran_Stage2/tran_starList/Star_On/tran_star3");
#endif
        this.tran_star2 = this.transform.Find("tran_list/tran_Stage2/tran_starList/Star_On/tran_star2");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star2, "tran_list/tran_Stage2/tran_starList/Star_On/tran_star2");
#endif
        this.tran_star1 = this.transform.Find("tran_list/tran_Stage2/tran_starList/Star_On/tran_star1");
#if UNITY_EDITOR
        this.NullAssert(this.tran_star1, "tran_list/tran_Stage2/tran_starList/Star_On/tran_star1");
#endif
        this.tran_Stage1 = this.transform.Find("tran_list/tran_Stage1");
#if UNITY_EDITOR
        this.NullAssert(this.tran_Stage1, "tran_list/tran_Stage1");
#endif

    }
}