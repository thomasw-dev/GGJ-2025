using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public GameObject cross;

    void Start()
    {
        cross.SetActive(false);
    }

    public void LoseLife()
    {
        cross.SetActive(true);
    }
}
