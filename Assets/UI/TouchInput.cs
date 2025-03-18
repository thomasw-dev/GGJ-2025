using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public static Vector2 delta;
    [SerializeField] Joystick joystick;

    void Update()
    {
        if (joystick != null)
        {
            delta = joystick.Direction;
        }
    }
}
