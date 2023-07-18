using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private const string _StartSceneName = "Start";
    private const string _LevelSelectSceneName = "LevelSelect";
    private const string _GameSceneName = "3DMain";

    public enum GameState
    {
        Start,
        Game,
    }

    public static void LevelSelect()
    {
        SceneManager.LoadScene(_LevelSelectSceneName);
    }

    public static void StartGame()
    {
        SceneManager.LoadScene(_GameSceneName);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(_StartSceneName);
    }
}
