using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI scoreValueLabel;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();

        canvas.enabled = false;
        scoreValueLabel.text = GameMaster.score.ToString();
        
    }

    public void ShowScreen()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        Global.isGamePaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Global.isGamePaused = false;
    }
}
