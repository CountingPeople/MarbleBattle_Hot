using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevel : MonoBehaviour
{
    public TextMeshProUGUI _MainLevel;
    public TextMeshProUGUI _SubLevel;


    void Start()
    {
        Debug.Assert(_MainLevel != null);
        Debug.Assert(_SubLevel != null);

        OnLevelChanged();

        GlobalEventManager.OnLevelAdvanced.AddListener(OnLevelChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelChanged()
    {
        _MainLevel.text = LevelManager.Instance.CurrentLevel.LevelID.ToString();
        _SubLevel.text = LevelManager.Instance.CurrentLevel.SubLevelID.ToString();
    }
}
