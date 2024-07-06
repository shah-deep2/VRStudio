using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIController : MonoBehaviour
{
    public Image displayImage;
    public TMP_InputField inputField;
    private string imagePath = Application.dataPath + "/ScreenCaptureOut.png";
    private Texture2D currentTexture;
    private TouchScreenKeyboard keyboard;
    private bool keyboardVisible = false;
    public event Action<string> OnSearchSubmitted;
    public SearchHandler searchHandler;

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
        inputField.onSelect.AddListener(OnInputFieldSelected);
        // inputField.onEndEdit.AddListener(OnInputFieldSubmit);
        inputField.onDeselect.AddListener(OnInputFieldDeselected);

        // Load and display the generated image
        LoadGeneratedImage();    
    }

    void Update()
    {
        if (keyboardVisible && keyboard != null)
        {
            inputField.text = keyboard.text;

            // Check if search is submitted
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                SubmitSearch();
                CloseKeyboard();
            }
        }
    }

    public void LoadGeneratedImage()
    {
        // CleanupOldTexture();

        // Check if the file exists
        if (File.Exists(imagePath))
        {
            // Load the image as a byte array
            byte[] fileData = File.ReadAllBytes(imagePath);

            currentTexture = new Texture2D(2, 2);
            currentTexture.LoadImage(fileData);

            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f));

            // Assign the sprite to the Image component
            displayImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Generated image not found at path: " + imagePath);
        }
    }

    private void CleanupOldTexture()
    {
        if (currentTexture != null)
        {
            Destroy(currentTexture);
            currentTexture = null;
        }

        if (displayImage.sprite != null)
        {
            Destroy(displayImage.sprite);
            displayImage.sprite = null;
        }
    }

    public void ClearInputField()
    {
        if (inputField != null)
        {
            inputField.text = "";
        }
        else
        {
            Debug.LogWarning("TMP_InputField reference is not set!");
        }
    }

    void OnInputFieldSubmit(string text)
    {
        Debug.Log("Input submitted: " + text);
        // You can add your own logic here to process the input
    }

    void OnInputFieldSelected(string value)
    {
        OpenKeyboard();
    }

    void OnInputFieldDeselected(string value)
    {
        // Only close if not submitting search
        if (keyboard.status != TouchScreenKeyboard.Status.Done)
        {
            CloseKeyboard();
        }
    }

    void OpenKeyboard()
    {
        if (!keyboardVisible)
        {
            // Use Search keyboard type
            keyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Search);
            keyboardVisible = true;
        }
    }

    void CloseKeyboard()
    {
        if (keyboardVisible)
        {
            keyboard.active = false;
            keyboardVisible = false;
        }
    }

    void SubmitSearch()
    {
        string searchText = inputField.text;
        Debug.Log($"Search submitted: {searchText}");
        searchHandler.Execute(searchText);
    }

    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onSelect.RemoveListener(OnInputFieldSelected);
            inputField.onDeselect.RemoveListener(OnInputFieldDeselected);
        }
    }
}
