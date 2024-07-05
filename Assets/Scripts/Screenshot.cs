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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Capture();
        
    }

    private void Capture()
    {    
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/ScreenCaptureOut.png");
        // Debug.Log("pressed");
        // Texture2D tex = GetRTPixels();
        // byte[] bytes = tex.EncodeToJPG();
        // string path = Application.dataPath + "/Screencapture.jpg";

        // System.IO.File.WriteAllBytes(path, bytes);
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
