using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierCountShow : MonoBehaviour
{
    TextMeshPro mText;

    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshPro>();

        MarbleEventManager.OnSoldierQueueChanged.AddListener(OnSoldierCountChanged);
    }

    void OnSoldierCountChanged(int count)
    {
        mText.text = count.ToString();
    }
}
