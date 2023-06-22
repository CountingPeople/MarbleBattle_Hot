//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIAchivmentItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIAchivmentItem.prefab";

	#region variable

    private Slider sli_progress = null;
    public Slider Sli_progress => this.sli_progress;
    private Button btn_get = null;
    public Button Btn_get => this.btn_get;
    private Text txt_haveGet = null;
    public Text Txt_haveGet => this.txt_haveGet;
    private Text txt_notFinish = null;
    public Text Txt_notFinish => this.txt_notFinish;
    private Text txt_content = null;
    public Text Txt_content => this.txt_content;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.sli_progress = this.transform.Find("sli_progress").GetComponent<Slider>();
#if UNITY_EDITOR
        this.NullAssert(this.sli_progress, "sli_progress");
#endif
        this.btn_get = this.transform.Find("btn_get").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_get, "btn_get");
#endif
        this.txt_haveGet = this.transform.Find("txt_haveGet").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_haveGet, "txt_haveGet");
#endif
        this.txt_notFinish = this.transform.Find("txt_notFinish").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_notFinish, "txt_notFinish");
#endif
        this.txt_content = this.transform.Find("txt_content").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_content, "txt_content");
#endif

    }
}