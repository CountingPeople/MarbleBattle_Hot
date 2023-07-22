using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Framework;
using UnityEditor;

public class BrickController : MonoBehaviour
{
    private Color StartColor;
    private Color EndColor; // TODO: no use in 3D, del it ? @zhangrufu
    
    private TextMeshPro mTextNumber;
    private Animator mAnimator;

    // Cell
    private BrickManager.Grid.Cell mCell = null;
    public BrickManager.Grid.Cell BrickCell
    {
        get { return mCell; }
        set { mCell = value; }
    }

    // Brick Type
    private cfg.MarbleReward mBrickType;
    public cfg.MarbleReward BrickType
    {
        get { return mBrickType; }
        set { mBrickType = value; }
    }

    private string mBrickID;
    public string BrickID
    {
        get { return mBrickID; }
        set { mBrickID = value; }
    }

    private int mLife = 0;
    public int Life
    {
        get { return mLife; }
        set
        {
            mLife = value;
            CurLife = value;
        }
    }

    private int mCurLife;
    
    public int CurLife 
    {
        get { return mCurLife; }
        set
        {
            mCurLife = value;

            // mTextNumber.color = Color.Lerp(EndColor, StartColor, (float)Mathf.Max(CurLife - 1, 0) / (float)(mLife));
            mTextNumber.text = mCurLife.ToString();
        }
    }

    private GameObject mEffectTemplate = null;

    void Awake()
    {
        mTextNumber = GetComponentInChildren<TextMeshPro>();
        mAnimator = GetComponentInChildren<Animator>();

        mEffectTemplate = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Bundles/Res/Prefabs/Effect/Fx_Stars.prefab");

        StartColor = mTextNumber.color;
    }

    private void OnDestroy()
    {
        
    }

    public void Hit(Vector3 hitPositionInWS, Vector3 hitNormal)
    {
        CurLife -= 1;

        if (CurLife > 0)
        {
            // enter Hitted state
            mAnimator.SetTrigger("Hitted");
            return;
        }

        // brick dead

        // 1.post event
        switch(mBrickType)
        {
            case cfg.MarbleReward.Soldier:
                MarbleEventManager.OnBrickDestory.Invoke(this);
                break;
            case cfg.MarbleReward.HPRecover:
                MarbleEventManager.OnHPBrickDestory.Invoke(float.Parse(BrickID));
                break;
        }

        // 2.play effect
        // TODO: effect manager
        // TODO: read from config
        GameObject effect = GameObject.Instantiate<GameObject>(mEffectTemplate);
        effect.transform.position = hitPositionInWS;
        effect.transform.up = hitNormal;

        Destroy(gameObject);
    }
}
