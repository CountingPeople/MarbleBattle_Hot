using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelShow : MonoBehaviour
{
    public GameObject _BtnLevelTemplate = null;

    void Start()
    {
        var levelList = DataManager.DataTable.TbLevelConfig.DataList;
        int levelCount = 0;
        foreach (var item in levelList)
        {
            if (levelCount != item.LevelID)
                ++levelCount;
        }

        for (int i = 1; i <= levelCount; ++i)
        {
            int levelID = i;
            GameObject go = GameObject.Instantiate<GameObject>(_BtnLevelTemplate);
            go.transform.SetParent(transform);
            go.SetActive(true);

            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
            go.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                LevelManager.Instance.SetLevel(levelID, 1);
                GameManager.StartGame();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
