using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICountDown : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _CountDown;

    private float mFreshTime = 0.9f;
    private float mTimeCD = 0.0f;

    void Start()
    {
        TimeSpan time = TimeSpan.FromSeconds(LevelManager.Instance.RemainTime);
        _CountDown.text = time.ToString(@"mm\:ss");
    }

    // Update is called once per frame
    void Update()
    {
        mTimeCD += Time.deltaTime;
        if (mTimeCD >= mFreshTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(LevelManager.Instance.RemainTime);
            _CountDown.text = time.ToString(@"mm\:ss");

            mTimeCD = 0;
        }
    }
}
