using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    static InitManager _Instance = null;

    private bool mOwn = false;

    private void Init()
    {
        DataManager.Init();
        LevelManager.Init();

        DontDestroyOnLoad(gameObject);

        mOwn = true;
    }

    void Awake()
    {
        if (_Instance != null)
            return;

        Init();

        _Instance = this;
    }

    
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(mOwn)
            _Instance = null;
    }
}
