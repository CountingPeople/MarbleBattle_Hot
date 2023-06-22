//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIChapterItemView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/UIChapterItem.prefab";

	#region variable

    private Transform tran_flow = null;
    public Transform Tran_flow => this.tran_flow;
    private Text txt_name = null;
    public Text Txt_name => this.txt_name;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_flow = this.transform.Find("tran_flow");
#if UNITY_EDITOR
        this.NullAssert(this.tran_flow, "tran_flow");
#endif
        this.txt_name = this.transform.Find("txt_name").GetComponent<Text>();
#if UNITY_EDITOR
        this.NullAssert(this.txt_name, "txt_name");
#endif

    }
}