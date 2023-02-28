using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Start()
    {
        Screen.SetResolution(1440, 1080, true);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        ResetGame();
        SceneManager.LoadScene(0);
    }

    private void ResetGame()
    {
        GameSession.gameScore = 0;
        GameSession.gameSpeed = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
