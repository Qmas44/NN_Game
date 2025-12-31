using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int targetFPS = 30; // Desired framerate

    void Awake()
    {
        QualitySettings.vSyncCount = 0; // Disable VSync to allow the target framerate to be used
        Application.targetFrameRate = targetFPS; // Set the target framerate
    }
}