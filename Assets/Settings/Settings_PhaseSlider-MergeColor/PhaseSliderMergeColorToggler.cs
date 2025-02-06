using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseSliderMergeColorToggler : MonoBehaviour
{
    [SerializeField] GameObject phaseSlider;
    [SerializeField] GameObject mergeColorToggle;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu")
        {
            phaseSlider.SetActive(true);
            mergeColorToggle.SetActive(false);
        }
        if (currentScene == "MainGame")
        {
            phaseSlider.SetActive(false);
            mergeColorToggle.SetActive(true);
        }
    }
}
