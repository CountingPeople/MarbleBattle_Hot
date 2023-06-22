//------------------------------------------------------------------------------------------------------------
//--------------------------------------generate file don't modify--------------------------------------------
//------------------------------------------------------------------------------------------------------------
using TMPro;
using Framework;
using UnityEngine;
using UnityEngine.UI;

internal partial class UISkillPanelView : UIPanel
{
    public override string UIPath => "prefab/ui/panel/UISkillPanel";

	#region variable

    private Button btn_playAction = null;
    public Button Btn_playAction => this.btn_playAction;
    private Dropdown drop_selectEff = null;
    public Dropdown Drop_selectEff => this.drop_selectEff;
    private InputField inp_frame = null;
    public InputField Inp_frame => this.inp_frame;
    private InputField inp_totalFrame = null;
    public InputField Inp_totalFrame => this.inp_totalFrame;
    private Dropdown drop_selectType = null;
    public Dropdown Drop_selectType => this.drop_selectType;
    private Button btn_delete = null;
    public Button Btn_delete => this.btn_delete;
    private Button btn_add = null;
    public Button Btn_add => this.btn_add;
    private Transform tran_frameList = null;
    public Transform Tran_frameList => this.tran_frameList;
    private Transform tran_cursor = null;
    public Transform Tran_cursor => this.tran_cursor;
    private Button btn_play = null;
    public Button Btn_play => this.btn_play;
    private Toggle tog_isBullet = null;
    public Toggle Tog_isBullet => this.tog_isBullet;
    private Toggle tog_haveActEff = null;
    public Toggle Tog_haveActEff => this.tog_haveActEff;
    private Toggle tog_haveHitEff = null;
    public Toggle Tog_haveHitEff => this.tog_haveHitEff;
    private Transform tran_dropSuff = null;
    public Transform Tran_dropSuff => this.tran_dropSuff;
    private Dropdown drop_sufEff = null;
    public Dropdown Drop_sufEff => this.drop_sufEff;
    private Transform tran_inpSuffAngle = null;
    public Transform Tran_inpSuffAngle => this.tran_inpSuffAngle;
    private InputField inp_suffAngle = null;
    public InputField Inp_suffAngle => this.inp_suffAngle;
    private Transform tran_dropAtk = null;
    public Transform Tran_dropAtk => this.tran_dropAtk;
    private Dropdown drop_atkEff = null;
    public Dropdown Drop_atkEff => this.drop_atkEff;
    private Transform tran_inpAtkAngle = null;
    public Transform Tran_inpAtkAngle => this.tran_inpAtkAngle;
    private InputField inp_atkAngle = null;
    public InputField Inp_atkAngle => this.inp_atkAngle;
    private Transform tran_dropBullet = null;
    public Transform Tran_dropBullet => this.tran_dropBullet;
    private Dropdown drop_bulletEff = null;
    public Dropdown Drop_bulletEff => this.drop_bulletEff;
    private Transform tran_dropTrack = null;
    public Transform Tran_dropTrack => this.tran_dropTrack;
    private Dropdown drop_track = null;
    public Dropdown Drop_track => this.drop_track;
    private Transform tran_inpSpeed = null;
    public Transform Tran_inpSpeed => this.tran_inpSpeed;
    private InputField inp_speed = null;
    public InputField Inp_speed => this.inp_speed;
    private Dropdown drop_tar = null;
    public Dropdown Drop_tar => this.drop_tar;
    private Dropdown drop_range = null;
    public Dropdown Drop_range => this.drop_range;
    private Dropdown drop_ref = null;
    public Dropdown Drop_ref => this.drop_ref;

    #endregion

    protected override void InitializeElement()
    {
        //初始化组件

        this.btn_playAction = this.transform.Find("tran_left/btn_playAction").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_playAction, "tran_left/btn_playAction");
#endif
        this.drop_selectEff = this.transform.Find("tran_left/drop_selectEff").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_selectEff, "tran_left/drop_selectEff");
#endif
        this.inp_frame = this.transform.Find("tran_left/inp_frame").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_frame, "tran_left/inp_frame");
#endif
        this.inp_totalFrame = this.transform.Find("tran_left/inp_totalFrame").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_totalFrame, "tran_left/inp_totalFrame");
#endif
        this.drop_selectType = this.transform.Find("tran_left/drop_selectType").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_selectType, "tran_left/drop_selectType");
#endif
        this.btn_delete = this.transform.Find("tran_bottom/tran_slider/btn_delete").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_delete, "tran_bottom/tran_slider/btn_delete");
#endif
        this.btn_add = this.transform.Find("tran_bottom/tran_slider/btn_add").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_add, "tran_bottom/tran_slider/btn_add");
#endif
        this.tran_frameList = this.transform.Find("tran_bottom/tran_slider/tran_frameList");
#if UNITY_EDITOR
        this.NullAssert(this.tran_frameList, "tran_bottom/tran_slider/tran_frameList");
#endif
        this.tran_cursor = this.transform.Find("tran_bottom/tran_slider/tran_frameList/tran_cursor");
#if UNITY_EDITOR
        this.NullAssert(this.tran_cursor, "tran_bottom/tran_slider/tran_frameList/tran_cursor");
#endif
        this.btn_play = this.transform.Find("tran_bg/btn_play").GetComponent<Button>();
#if UNITY_EDITOR
        this.NullAssert(this.btn_play, "tran_bg/btn_play");
#endif
        this.tog_isBullet = this.transform.Find("tran_bg/tran_tog/tog_isBullet").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_isBullet, "tran_bg/tran_tog/tog_isBullet");
#endif
        this.tog_haveActEff = this.transform.Find("tran_bg/tran_tog/tog_haveActEff").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_haveActEff, "tran_bg/tran_tog/tog_haveActEff");
#endif
        this.tog_haveHitEff = this.transform.Find("tran_bg/tran_tog/tog_haveHitEff").GetComponent<Toggle>();
#if UNITY_EDITOR
        this.NullAssert(this.tog_haveHitEff, "tran_bg/tran_tog/tog_haveHitEff");
#endif
        this.tran_dropSuff = this.transform.Find("tran_bg/tran_prop/tran_dropSuff");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dropSuff, "tran_bg/tran_prop/tran_dropSuff");
#endif
        this.drop_sufEff = this.transform.Find("tran_bg/tran_prop/tran_dropSuff/drop_sufEff").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_sufEff, "tran_bg/tran_prop/tran_dropSuff/drop_sufEff");
#endif
        this.tran_inpSuffAngle = this.transform.Find("tran_bg/tran_prop/tran_inpSuffAngle");
#if UNITY_EDITOR
        this.NullAssert(this.tran_inpSuffAngle, "tran_bg/tran_prop/tran_inpSuffAngle");
#endif
        this.inp_suffAngle = this.transform.Find("tran_bg/tran_prop/tran_inpSuffAngle/inp_suffAngle").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_suffAngle, "tran_bg/tran_prop/tran_inpSuffAngle/inp_suffAngle");
#endif
        this.tran_dropAtk = this.transform.Find("tran_bg/tran_prop/tran_dropAtk");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dropAtk, "tran_bg/tran_prop/tran_dropAtk");
#endif
        this.drop_atkEff = this.transform.Find("tran_bg/tran_prop/tran_dropAtk/drop_atkEff").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_atkEff, "tran_bg/tran_prop/tran_dropAtk/drop_atkEff");
#endif
        this.tran_inpAtkAngle = this.transform.Find("tran_bg/tran_prop/tran_inpAtkAngle");
#if UNITY_EDITOR
        this.NullAssert(this.tran_inpAtkAngle, "tran_bg/tran_prop/tran_inpAtkAngle");
#endif
        this.inp_atkAngle = this.transform.Find("tran_bg/tran_prop/tran_inpAtkAngle/inp_atkAngle").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_atkAngle, "tran_bg/tran_prop/tran_inpAtkAngle/inp_atkAngle");
#endif
        this.tran_dropBullet = this.transform.Find("tran_bg/tran_prop/tran_dropBullet");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dropBullet, "tran_bg/tran_prop/tran_dropBullet");
#endif
        this.drop_bulletEff = this.transform.Find("tran_bg/tran_prop/tran_dropBullet/drop_bulletEff").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_bulletEff, "tran_bg/tran_prop/tran_dropBullet/drop_bulletEff");
#endif
        this.tran_dropTrack = this.transform.Find("tran_bg/tran_prop/tran_dropTrack");
#if UNITY_EDITOR
        this.NullAssert(this.tran_dropTrack, "tran_bg/tran_prop/tran_dropTrack");
#endif
        this.drop_track = this.transform.Find("tran_bg/tran_prop/tran_dropTrack/drop_track").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_track, "tran_bg/tran_prop/tran_dropTrack/drop_track");
#endif
        this.tran_inpSpeed = this.transform.Find("tran_bg/tran_prop/tran_inpSpeed");
#if UNITY_EDITOR
        this.NullAssert(this.tran_inpSpeed, "tran_bg/tran_prop/tran_inpSpeed");
#endif
        this.inp_speed = this.transform.Find("tran_bg/tran_prop/tran_inpSpeed/inp_speed").GetComponent<InputField>();
#if UNITY_EDITOR
        this.NullAssert(this.inp_speed, "tran_bg/tran_prop/tran_inpSpeed/inp_speed");
#endif
        this.drop_tar = this.transform.Find("tran_bg/tran_drop/drop_tar").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_tar, "tran_bg/tran_drop/drop_tar");
#endif
        this.drop_range = this.transform.Find("tran_bg/tran_drop/drop_range").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_range, "tran_bg/tran_drop/drop_range");
#endif
        this.drop_ref = this.transform.Find("tran_bg/tran_drop/drop_ref").GetComponent<Dropdown>();
#if UNITY_EDITOR
        this.NullAssert(this.drop_ref, "tran_bg/tran_drop/drop_ref");
#endif

    }
}