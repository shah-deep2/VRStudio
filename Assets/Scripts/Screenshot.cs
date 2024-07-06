using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public Camera targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        // targetCamera = GetComponent<Camera>();
    }

    private void Capture()
    {    
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/ScreenCaptureOut.png");
    }

    private Texture2D GetRTPixels()
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = targetCamera.targetTexture;

        targetCamera.Render();
        Debug.Log(targetCamera);
        Debug.Log(targetCamera.targetTexture);
        
        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(targetCamera.targetTexture.width, targetCamera.targetTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        // Restore previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}
