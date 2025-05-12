using UnityEngine;
using UnityEngine.UI;

// Set the Canvas Scaler's "Match" value according to the screen's aspect ratio.
// This makes the canvas responsive and its content always visible.
[RequireComponent(typeof(CanvasScaler))]
public class ResponsiveCanvasScaler : MonoBehaviour
{
    float targetRatio = 1.77777f; // 16:9
    CanvasScaler canvasScaler;

    void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    void Update()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenRatio = screenWidth / screenHeight;
        canvasScaler.matchWidthOrHeight = screenRatio >= targetRatio ? 1 : 0;
    }
}
