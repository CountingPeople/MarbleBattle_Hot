using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameNext : MonoBehaviour
{
    public Button _BtnGo;
    public Button _BtnQuit;

    void Awake()
    {
        _BtnGo.onClick.AddListener(OnBtnGoClicked);
        _BtnQuit.onClick.AddListener(OnBtnQuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBtnGoClicked()
    {
        GameManager.LevelSelect();
    }

    void OnBtnQuitClicked()
    {
        GameManager.MainMenu();
    }
}
