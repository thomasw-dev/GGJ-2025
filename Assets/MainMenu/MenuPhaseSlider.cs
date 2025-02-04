using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPhaseSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] TMP_Text label;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = 0;
    }

    void Update()
    {
        label.text = $"Phase {slider.value + 1}";
    }
}
