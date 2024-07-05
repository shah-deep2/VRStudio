using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasManager : MonoBehaviour
{
    public float distanceFromCamera = 15f;

    public Canvas canvas;
    private Camera mainCamera;
    private bool isVisible = false;

    void Start()
    {
        mainCamera = Camera.main;
        
        // Initially hide the canvas
        SetCanvasVisibility(false);
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