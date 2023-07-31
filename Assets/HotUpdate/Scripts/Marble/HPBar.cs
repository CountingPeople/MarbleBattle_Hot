using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private float mFullScale;

    void Start()
    {
        mFullScale = transform.localScale.x;

        BattleEventManager.OnPlayerHPChanged.AddListener(OnHPChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHPChanged(float hp)
    {
        float percent = hp / BattleGameManager.Player.FullHP;
        var localScale = transform.localScale;
        localScale.x = mFullScale * percent;
        transform.localScale = localScale;
    }
}
