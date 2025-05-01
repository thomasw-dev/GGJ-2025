using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchInputToggler : MonoBehaviour
{
    [SerializeField] GameObject touchInput;
    [SerializeField] GameObject hintOverlay;
    bool showHintOverlayWhenUnpaused = false;
    [SerializeField] Toggle toggle;

    void Start()
    {
        // Manually toggle it to trigger OnValueChanged()
        toggle.isOn = true;
    }

    void Update()
    {
        if (showHintOverlayWhenUnpaused && !Global.isGamePaused)
        {
            hintOverlay.SetActive(true);
            showHintOverlayWhenUnpaused = false;
        }

        // Clicking anywhere will hide the hint overlay
        if (hintOverlay.activeSelf && Input.GetMouseButtonDown(0))
        {
            hintOverlay.SetActive(false);
            showHintOverlayWhenUnpaused = false;
        }
    }

    public void OnToggleChanged(Toggle toggle)
    {
        bool value = toggle.isOn;
        Global.useTouchInput = value;
        touchInput.SetActive(value);

        // Turning on touch input will show the hint overlay
        showHintOverlayWhenUnpaused = value == true;
    }
}
