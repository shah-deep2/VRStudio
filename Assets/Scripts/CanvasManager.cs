using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasManager : MonoBehaviour
{
    public float distanceFromCamera = 5f;

    public Canvas canvas;
    public Camera mainCamera;
    public UIController uIController;
    private bool isVisible = false;

    void Start()
    {
        // mainCamera = Camera.main;
        
        // Initially hide the canvas
        SetCanvasVisibility(false);

        Capture(); // Dummy
    }

    void ToggleCanvas()
    {
        isVisible = !isVisible;
        SetCanvasVisibility(isVisible);

        if (isVisible)
        {
            PositionCanvasInFrontOfCamera();
        }
    }

    public void Capture()
    {    
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/ScreenCaptureOut.png");
        uIController.LoadGeneratedImage();
        uIController.ClearInputField();
        ShowCanvas();
    }

    void ShowCanvas()
    {
        SetCanvasVisibility(true);
        PositionCanvasInFrontOfCamera();
    }

    void SetCanvasVisibility(bool visible)
    {
        canvas.enabled = visible;
    }

    void PositionCanvasInFrontOfCamera()
    {
        // Position the canvas in front of the camera
        Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;
        transform.position = newPosition;

        // Make the canvas face the camera
        transform.rotation = mainCamera.transform.rotation;

        // Update the canvas plane distance
        canvas.planeDistance = distanceFromCamera;
    }
}