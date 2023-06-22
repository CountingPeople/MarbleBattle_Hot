using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHPBar : MonoBehaviour
{
    public RectTransform _BarRect;

    private Vector2 mBarSizeDelta;
    private float mBarWidth;

    void Start()
    {
        mBarWidth = _BarRect.rect.width;
        mBarSizeDelta = _BarRect.offsetMax;

        BattleEventManager.OnPlayerHPChanged.AddListener(OnHPChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHPChanged(float hp)
    {
        float percent = 1 - hp / BattleGameManager.Player.FullHP;
        _BarRect.offsetMax = new Vector2(mBarSizeDelta.x - mBarWidth * percent, mBarSizeDelta.y);
    }
}
