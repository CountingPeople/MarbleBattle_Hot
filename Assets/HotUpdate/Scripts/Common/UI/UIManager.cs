using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _PanelGameNext;
    public GameObject _PannelGameOver;

    void Start()
    {
        _PanelGameNext.SetActive(false);
        _PannelGameOver.SetActive(false);

        GlobalEventManager.OnLevelFinished.AddListener(OnLevelFinished);
        GlobalEventManager.OnGameOver.AddListener(OnGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLevelFinished()
    {
        _PanelGameNext.SetActive(true);
    }

    void OnGameOver()
    {
        _PannelGameOver.SetActive(true);
    }
}
