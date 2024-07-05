using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIImageController : MonoBehaviour
{
    public Image displayImage;
    public TMP_InputField inputField;
    public string imagePath;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the components are assigned
        if (displayImage == null || inputField == null)
        {
            Debug.LogError("Please assign the Image and Input Field components in the Inspector.");
            return;
        }

        imagePath = Application.dataPath + "/ScreenCaptureOut.png";

        // Add a listener to the input field
        inputField.onEndEdit.AddListener(OnInputFieldSubmit);

        // Load and display the generated image
        LoadGeneratedImage();    
    }


    void OnInputFieldSubmit(string text)
    {
        Debug.Log("Input submitted: " + text);
        // You can add your own logic here to process the input
    }

    void LoadGeneratedImage()
    {
        // Check if the file exists
        if (File.Exists(imagePath))
        {
            // Load the image as a byte array
            byte[] fileData = File.ReadAllBytes(imagePath);

            // Create a new Texture2D and load the image data
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the Image component
            displayImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Generated image not found at path: " + imagePath);
        }
    }
}
