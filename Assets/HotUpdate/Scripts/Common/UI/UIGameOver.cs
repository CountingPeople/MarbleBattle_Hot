using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Button _BtnRestart;
    public Button _BtnQuit;

    // Start is called before the first frame update
    void Awake()
    {
        _BtnRestart.onClick.AddListener(OnBtnRestartClicked);
        _BtnQuit.onClick.AddListener(OnBtnQuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBtnRestartClicked()
    {
        GameManager.LevelSelect();
    }

    void OnBtnQuitClicked()
    {
        GameManager.MainMenu();
    }
}
