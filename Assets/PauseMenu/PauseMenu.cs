using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;
    [SerializeField] Image pauseButton;
    [SerializeField] Sprite[] pauseButtonSprites;
    [SerializeField] AudioSource pauseSound;

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
        pauseButton.sprite = pauseButtonSprites[1];
        Time.timeScale = 1;
        Global.isGamePaused = false;
    }

    public void Pause()
    {
        canvas.enabled = true;
        pauseButton.sprite = pauseButtonSprites[0];
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
        pauseSound.Play();
    }
}
