using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BloodFlow : MonoBehaviour
{

    public Camera mainCam;
    public Transform parentTran;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (parentTran == null) 
        {
            transform.gameObject.SetActive(false);
            return;
        }
        if (!parentTran.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(false);
            return;
        }

        //Vector3 temp = mainCam.WorldToScreenPoint(new Vector3(parentTran.position.x, parentTran.position.y+ offset, parentTran.position.z));
        Vector3 temp = mainCam.WorldToScreenPoint(parentTran.position);
        //temp.x -= Screen.width * 0.5f;
        //temp.y -= Screen.height * 0.5f;
        transform.GetComponent<RectTransform>().position = temp;
    }

    public void SynBlood(float cur, float total) 
    {
        transform.GetComponent<Slider>().value = cur / total;
    }

    private Text bloodText;
    public void ShowBlood(string str, Color color)
    {
        //if (!DataModule.Instance.playerBattleData.isShowFlow)
        //{
        //    return;
        //}

        var obj = PerfabTool.Instance.GetObj(FlowTextEnum.UIFlowText);
        obj.obj.transform.SetParent(transform);

        bloodText = obj.obj.GetComponentInChildren<Text>();
        bloodText.color = color;
        bloodText.text = str;

        obj.obj.transform.localPosition = Vector3.zero;
        obj.obj.transform.DOLocalMoveY(100, 2);
        bloodText.DOFade(0, 3).OnComplete(() =>
        {
            PerfabTool.Instance.FreeFirst(FlowTextEnum.UIFlowText);
            obj.FreeObj();
        });
        bloodText.transform.localScale = Vector3.one;
        bloodText.transform.DOScale(2, 1).SetEase(Ease.OutExpo);
    }

    private Color[] colors = new Color[] {Color.green,Color.blue, Color.blue,new Color(250,0,188,255), Color.red };
    //public void SetSliderColor(RoleType roleType)
    //{
    //    transform.Find("FillArea/Fill").GetComponent<Image>().color= colors[(int)roleType-1];
    //}

    public void SetNpc(string name)
    {
        offset = 2;
        transform.Find("tran_name").gameObject.SetActive(true);
        transform.Find("tran_name").GetComponent<Text>().text=name;
    }
}
