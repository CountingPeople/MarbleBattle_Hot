using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTicker : MonoBehaviour
{
    public Transform _MonsterParent;

    public Camera _CameraMarbleGame;

    private void Awake()
    {
        LevelManager.Init();
        MarbleGameManager.Init(_CameraMarbleGame);
        BattleGameManager.Init(_MonsterParent);
    }

    void Update()
    {
        MarbleGameManager.Instance.Tick(Time.deltaTime);
        BattleGameManager.Tick(Time.deltaTime);
        LevelManager.Instance.Tick(Time.deltaTime);
    }

    private void LateUpdate()
    {
        BattleGameManager.LateTick();
    }

    private void OnDestroy()
    {
        MarbleGameManager.Destory();
        BattleGameManager.Destory();
    }
}
