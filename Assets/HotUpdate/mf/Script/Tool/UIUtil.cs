using BM;
using ET;
using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FrameEnum
{ 
   Green=1,
   Blue,
   Purple,
   Orabge,
   Red
}

public class UIUtil : MonoSingleton<UIUtil>
{

    float row = 11;
    float scubeX = 220;
    float scubeZ = 20;

    public void ClearCamModel()
    {
        var tran = CameraModule.Instance.GetModel2Camera().transform;
        for(int i=0;i< tran.childCount;i++)
        {
            GameObject.Destroy(tran.GetChild(i).gameObject);
        }
    }

    public void CreateModel(RawImage rawImage, float index, GameObject obj=null,Vector3 scale=default(Vector3))
    {
        if (obj != null)
        {
            obj.transform.SetParent(CameraModule.Instance.GetModel2Camera().transform);
            obj.transform.localPosition = new Vector3(-scubeX / 2 + scubeZ * (index + 0.5f), -5, scubeZ / 2);
            obj.transform.localScale = scale;
        }

        rawImage.texture = CameraModule.Instance.GetModel2Camera().targetTexture;

        float x = index / row;
        float y = 0;
        float w = 1 / row;
        float h = 1;
        rawImage.uvRect = new Rect(x, y, w, h);
    }

    public void SetModelPos(float index, GameObject obj=null)
    {
        string modelName= $"model{index}";
        Transform tempParent = CameraModule.Instance.GetModel2Camera().transform;
        var tempModel = tempParent.Find(modelName);
        if (tempModel != null)
        {
            GameObject.Destroy(tempModel.gameObject);
        }
        if (obj != null)
        {
            obj.transform.SetParent(tempParent);
            obj.transform.localPosition = new Vector3(-scubeX / 2 + scubeZ * (index + 0.5f), -5, scubeZ / 2);
            obj.name = modelName;
        }
    }


    /// <summary>
    /// ���ð�ȫ����
    /// </summary>
    /// <param name="panel"></param>
    public void UI_SetSafeArea(Transform tran)
    {
        var panel = tran.GetComponent<RectTransform>();
        if (panel == null)
        {
            Debug.Log("û��RectTransform���");
            return;
        }
        var safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }

    public AudioClip GetAudio(string path)
    {
        var pre = ResourcesModule.Instance.Load<AudioClip>(path, true, "LocalBundles");
        if (pre == null)
        {
            Debug.LogError($"error GetAudio {path}");
            return null;
        }
        return pre as AudioClip;
    }


    public Sprite GetImage(string path) 
    {
        var pre = ResourcesModule.Instance.Load<Sprite>(path,true, "LocalBundles");
        if (pre == null)
        {
            Debug.LogError($"error GetImage {path}");
            return null;
        }
        return pre as Sprite;
    }
    public Sprite GetFrame(int frameEnum)
    {
        return GetImage($"Assets/LocalBundles/UI/Icon/Frame/frame_itemframe_01_n_{frameEnum}.png");
    }

    private string[] colTbl = new string[] { "", "77F896", "77DEF8", "CC77F8", "F6A940", "FF5147", "FF5147" };

    public string GetQualityName(int quality,string content)
    {
        return string.Format("<color=#{0}>{1}</color>", colTbl[quality], content);
    }

    public List<UIItem> ShowList<T>(IList list,UIView uIView,Transform tran) where T : UIItem, new()
    {
        for (int i = 0; i < tran.childCount; i++)
        {
            tran.GetChild(i).gameObject.SetActive(false);
        }
        List<UIItem> itemList = new List<UIItem>();
        for (int i = 0; i < list.Count; i++)
        {
            UIItem temp = null;
            if (i >= tran.childCount)
            {
                temp = uIView.AddSubItem<T>();
                temp.SetParent(tran);
            }
            else {
                temp = uIView.GetSubItem<T>();
            }
            temp.gameObject.SetActive(true);
            temp.SetData(list[i],i);
            itemList.Add(temp);
        }
        return itemList;
    }


    public async ETTask LoadRes(string path, Transform parent, Vector3 pos)
    {
        var per = await AssetComponent.LoadAsync<GameObject>(path,GameContant.LocalBundles);
        if (per != null)
        {
            var obj = GameObject.Instantiate(per, parent);
            obj.transform.localPosition = pos;

        }
        else {
            Debug.Log(path);
        }
    }

    public GameObject LoadObj(string path, Transform tran, Vector3 pos, Vector3 scale, Vector3 angle = default(Vector3))
    {
        var per = ResourcesModule.Instance.Load<GameObject>(path,true,GameContant.LocalBundles);
        var obj = GameObject.Instantiate(per, tran);
        obj.transform.localPosition = pos;
        obj.transform.localScale = scale;
        obj.transform.localEulerAngles = angle;
        return obj;
    }

    //public GameObject LoadObj(string path, Transform parent, Vector3 pos,bool isAddBlood=false)
    //{
    //    var per = ResourcesModule.Instance.Load<GameObject>(path,true,GameContant.LocalBundles);
    //    var obj = GameObject.Instantiate(per, parent);
    //    obj.transform.localPosition = pos;

    //    if (isAddBlood)
    //    {
    //        var panel = UIModule.Instance.GetPanel<UIBattleTextPanelView>();
    //        var blood = panel.AddSubItem<UIBloodView>(panel.Tran_root);
    //        var bloodFlow = blood.gameObject.AddComponent<BloodFlow>();
    //        bloodFlow.parentTran = obj.GetComponent<Dummy>().GetDummy("tran_blood");
    //        obj.GetComponent<Player_AttackCom>().SetHitBack((value) => { bloodFlow.ShowBlood(value.ToString(), Color.red); });
    //    }

    //    return obj;
    //}

    //public void ChangeModel(GameObject obj,RoleDto roleDto)
    //{
    //    var slot1 = roleDto.GetSlotEquip(2);
    //    if (slot1 != null) PutOnEquip(obj, DummyEnum.DummyPropLeft, slot1.model);

    //    var slot3 = roleDto.GetSlotEquip(3);
    //    if (slot3 != null) PutOnEquip(obj, DummyEnum.DummyPropRight, slot3.model);

    //    var slot6 = roleDto.GetSlotEquip(1);
    //    if (slot6 != null) PutOnEquip(obj, DummyEnum.DummyPropHead, slot6.model);

    //    var slot4 = roleDto.GetSlotEquip(4);
    //    if (slot4 != null)
    //    {
    //        var bodyDummy = obj.GetComponent<Dummy>().GetDummy(DummyEnum.DummyPropBody);
    //        var per = AssetComponent.Load<Material>(slot4.model, GameContant.LocalBundles);
    //        if (per != null)
    //        {
    //            bodyDummy.GetComponent<SkinnedMeshRenderer>().material = per;
    //        }
    //    }
    //}
    //private void PutOnEquip(GameObject obj, DummyEnum dummy, string path)
    //{
    //    var tran = obj.GetComponent<Dummy>().GetDummy(dummy);
    //    if (tran.childCount > 0)
    //    {
    //        for (int i = 0; i < tran.childCount; i++)
    //        {
    //            GameObject.Destroy(tran.GetChild(i).gameObject);
    //        }
    //    }

    //    LoadRes(path, tran, Vector3.zero).Coroutine();
    //}

    // �պ����б�
    string[] surnames = { "˾��", "ŷ��", "�ĺ�", "���", "����", "�Ϲ�", "����",
                      "�Ϲ�", "����", "����", "����", "����", "��ԯ", "���",
                      "����", "���", "�ӳ�", "����", "��ұ", "����", "���",
                      "����", "����", "̫ʷ", "ξ��", "����", "����", "�̨",
                      "�ʸ�", "����", "����", "����", "���", "˾��", "�ٹ�",
                      "��ԯ", "�ٳ�", "�θ�", "����", "����", "����", "����",
                      "����", "΢��", "��˧", "�ÿ�", "����", "����", "����" };
    string[] names = { "ΰ", "��", "��", "��Ӣ", "��", "��", "��", "ǿ",
                   "��", "��", "��", "��", "��", "��", "��", "��",
                   "��", "��", "����", "ϼ", "ƽ", "��", "��Ӣ", "��",
                   "��", "��", "��", "����", "�ٻ�", "��", "˧", "С��",
                   "С��", "С��", "С��", "С��", "Сѩ", "С��", "С��", "Сϼ",
                   "С��", "СӢ", "С÷", "С��", "С��", "С��", "С�", "С��",
                   "С��", "С��", "Сţ", "С��", "Сɽ", "СϪ", "С��", "С��",
                   "С��", "С��", "С��", "С��", "С��", "С��", "С��", "СƼ",
                   "С��", "Сɺ", "С��", "С��", "С��", "С��", "С��", "С��",
                   "С��", "С��", "С��", "С��", "С��", "С��", "С��", "С��",
                   "С��", "С��", "С��", "Сޱ", "С��", "Сƽ", "С��", "С��",
                   "С��", "С��", "С��", "С��", "С��", "С��", "С��", "С��" };

    // ���������б�
    List<string> nameList = new List<string>();


    public string GetRandName()
    {
        string tempName = null;
        while (nameList.Count < 1000)
        {
            string name = surnames[UnityEngine.Random.Range(0, surnames.Length)] + names[UnityEngine.Random.Range(0, names.Length)];
            if (!nameList.Contains(name))
            {
                tempName = name;
                nameList.Add(name);
                break;
            }
        }
        if (tempName == null)
        {
            nameList.Clear();
            tempName = GetRandName();
        }
        return tempName;
    }

    public string GetRandHead()
    { 
       return string.Format(GameContant.HeadPath, UnityEngine.Random.Range(1, 15));
    }
}
