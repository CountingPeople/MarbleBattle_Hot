//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UIStoreSubPanelView : UIItem
{
    public override string UIPath => "Assets/Bundles/Prefab/UI/Item/SubPanel/UIStoreSubPanel.prefab";

	#region variable

    private Transform tran_equip = null;
    public Transform Tran_equip => this.tran_equip;
    private Transform tran_key = null;
    public Transform Tran_key => this.tran_key;
    private Transform tran_box = null;
    public Transform Tran_box => this.tran_box;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.tran_equip = this.transform.Find("tran_root/tran_equip");
#if UNITY_EDITOR
        this.NullAssert(this.tran_equip, "tran_root/tran_equip");
#endif
        this.tran_key = this.transform.Find("tran_root/tran_key");
#if UNITY_EDITOR
        this.NullAssert(this.tran_key, "tran_root/tran_key");
#endif
        this.tran_box = this.transform.Find("tran_root/tran_box");
#if UNITY_EDITOR
        this.NullAssert(this.tran_box, "tran_root/tran_box");
#endif

    }
}