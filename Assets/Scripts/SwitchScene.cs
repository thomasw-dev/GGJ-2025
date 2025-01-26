using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Global.isGamePaused = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
