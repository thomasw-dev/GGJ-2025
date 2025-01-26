using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckFace : MonoBehaviour
{
    void Start()
    {
        // Destroy self in 2 seconds upon instantiated
        Destroy(gameObject, 2f);
    }
}
