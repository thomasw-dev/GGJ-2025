using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckFace : MonoBehaviour
{
    void Start()
    {
        // Destroy self in 1.5 seconds upon instantiated
        Destroy(gameObject, 1.5f);
    }
}
