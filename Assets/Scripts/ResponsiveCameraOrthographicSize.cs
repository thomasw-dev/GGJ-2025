using UnityEngine;

// Set the Orthographic Camera's "Size" value according to the screen's aspect ratio.
// This makes the camera responsive and its content always visible.
[RequireComponent(typeof(Camera))]
public class ResponsiveCameraOrthographicSize : MonoBehaviour
{
    float targetRatio = 1.77777f; // 16:9
    Camera cam;
    float initialSize;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        // Assume camera orthographic size is static
        initialSize = cam.orthographicSize;
    }

    void LateUpdate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenRatio = screenWidth / screenHeight;
        float multiplier = targetRatio / screenRatio;
        cam.orthographicSize = multiplier > 1 ? initialSize * multiplier : initialSize;
    }
}
