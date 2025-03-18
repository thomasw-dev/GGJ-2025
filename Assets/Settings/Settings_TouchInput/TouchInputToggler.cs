using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputToggler : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] GameObject touchInput;

    void Update()
    {
        if (toggle != null && touchInput != null)
        {
            bool value = toggle.isOn;
            Global.useTouchInput = value;
            touchInput.SetActive(value);
        }
    }
}
