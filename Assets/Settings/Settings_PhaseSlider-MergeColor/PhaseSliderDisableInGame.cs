using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseSliderDisableInGame : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainGame")
        {
            gameObject.SetActive(false);
        }
    }
}
