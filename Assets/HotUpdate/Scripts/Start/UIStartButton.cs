using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartButton : MonoBehaviour
{

    public Button _StartButton;

    void Start()
    {
#if UNITY_EDITOR
        Debug.Assert(_StartButton != null, "Button is null");
#endif

        _StartButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        GameManager.LevelSelect();
    }
}
