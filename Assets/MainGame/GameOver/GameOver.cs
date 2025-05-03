using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Canvas canvas;
    public Image background;
    public TextMeshProUGUI textLabel;
    public TextMeshProUGUI scoreValueLabel;

    void Start()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();

        canvas.enabled = false;
    }

    public void ShowLoseScreen()
    {
        background.color = Method.HexColor("#330000E6");
        textLabel.text = "Game Over!";
        ShowScreen();
    }

    public void ShowWinScreen()
    {
        background.color = Method.HexColor("#18331CE6");
        textLabel.text = "You Win!";
        ShowScreen();
    }

    void ShowScreen()
    {
        scoreValueLabel.text = GameMaster.score.ToString();
        canvas.enabled = true;
        Global.isGamePaused = true;
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Global.isGamePaused = false;
        canvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
