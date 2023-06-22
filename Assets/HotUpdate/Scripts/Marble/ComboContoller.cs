using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ComboContoller : MonoBehaviour
{
    public Image _Hundreds;
    public Image _Tens;
    public Image _Digits;
    //public Texture2D _NumberTexture;

    [SerializeField]
    private int _Number;
    private int mHundreds = 0;
    private int mTens = 0;
    private int mDigits = 0;
    private Sprite[] mNumSprite;

    public int CurNumber
    {
        get { return _Number; }
        set
        {
            _Number = value;
            if (_Number > 999)
            {
                _Number = 999;
            }                
            mDigits = _Number % 10;
            mTens = (_Number % 100 - mDigits) / 10;
            mHundreds = (_Number - _Number % 10) / 100;
            ChangeNumberImage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mNumSprite = Resources.LoadAll<Sprite>("Texture/Common/UI/Numbers");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeNumberImage()
    {
        //_Digits.sprite = Resources.Load<Sprite>(_NumberTexture.name + "_1");
        //_Digits.sprite = Resources.Load<Sprite>("Numbers_0");
        _Digits.gameObject.SetActive(false);
        _Tens.gameObject.SetActive(false);
        _Hundreds.gameObject.SetActive(false);
        _Digits.sprite = mNumSprite[mDigits];
        if (_Number > 0)
        {
            _Digits.gameObject.SetActive(true);
            _Digits.sprite = mNumSprite[mDigits];
            if(_Number >= 10)
            {
                _Tens.gameObject.SetActive(true);
                _Tens.sprite = mNumSprite[mTens];
                if(_Number >= 100)
                {
                    _Hundreds.sprite = mNumSprite[mHundreds];
                    _Hundreds.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Test()
    {
        mNumSprite = Resources.LoadAll<Sprite>("Texture/Common/UI/Numbers");
        CurNumber = _Number;
    }
}

//[CustomEditor(typeof(ComboContoller))]
//public class ObjectBuilderEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        ComboContoller myScript = (ComboContoller)target;
//        if (GUILayout.Button("¸üÐÂÊý×Ö"))
//        {
//            myScript.Test();
//        }
//    }
//}
