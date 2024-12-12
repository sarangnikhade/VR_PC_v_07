using UnityEngine;
using UnityEngine.UI;

public class RenderModeManager : MonoBehaviour
{
    [Header("Canvas References")]
    public Canvas targetCanvas;

    [Header("VR Settings")]
    public float worldSpaceCanvasScale = 0.001f;
    public Vector3 worldSpaceCanvasPosition = Vector3.zero;
    public Vector3 worldSpaceCanvasRotation = Vector3.zero;

    void Start()
    {
        // Ensure we have a canvas reference
        if (targetCanvas == null)
        {
            targetCanvas = GetComponent<Canvas>();
        }

        // Configure render mode based on platform
        ConfigureRenderMode();
    }

    void ConfigureRenderMode()
    {
        if (IsVRPlatform())
        {
            SetupVRRenderMode();
        }
        else
        {
            SetupPCRenderMode();
        }
    }

    bool IsVRPlatform()
    {
        // Comprehensive VR platform detection
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        return UnityEngine.XR.XRSettings.enabled;
#elif UNITY_ANDROID
        return UnityEngine.XR.XRSettings.enabled;
#else
        return false;
#endif
    }

    void SetupVRRenderMode()
    {
        if (targetCanvas == null) return;

        // Set to World Space for VR
        targetCanvas.renderMode = RenderMode.WorldSpace;

        // Position and scale canvas for VR
        targetCanvas.transform.localPosition = worldSpaceCanvasPosition;
        targetCanvas.transform.localEulerAngles = worldSpaceCanvasRotation;
        targetCanvas.transform.localScale = Vector3.one * worldSpaceCanvasScale;
    }

    void SetupPCRenderMode()
    {
        if (targetCanvas == null) return;

        // Set to Screen Space Overlay for PC
        targetCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    // Optional: Runtime platform checking method
    public void ForceRenderMode(bool isVR)
    {
        if (isVR)
        {
            SetupVRRenderMode();
        }
        else
        {
            SetupPCRenderMode();
        }
    }
}