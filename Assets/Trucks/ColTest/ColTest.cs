using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.TryGetComponent(out TruckManager truckManager))
        {
            Debug.Log("Got manager!");
        }
    }
}
