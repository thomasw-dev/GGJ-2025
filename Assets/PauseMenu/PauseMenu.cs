using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;


    private void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }

        UnPause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void UnPause()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
        Global.isGamePaused = false;
    }

    public void Pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        Global.isGamePaused = true;
    }

    public void SwitchSceneTo(string sceneName)
    {
        Time.timeScale = 1;
        Global.isGamePaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void TogglePause()
    {
        if (Global.isGamePaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }
}
