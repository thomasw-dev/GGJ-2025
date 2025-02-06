using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckFace : MonoBehaviour
{
    void Start()
    {
        // Destroy self in 1 second upon instantiated
        Destroy(gameObject, 1f);
    }
}
