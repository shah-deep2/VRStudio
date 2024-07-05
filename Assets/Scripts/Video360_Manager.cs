using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video360_Manager : MonoBehaviour
{
    public GameObject[] objectsInScene;
    public FadeCanvas fadeCanvas; 
    public Material videoMaterial;
    public VideoPlayer videoPlayer; 
    public float fadeDuration = 1f;

    private Material _skyMaterial;

    // Start is called before the first frame update
    private void Start()
    {
        _skyMaterial = RenderSettings.skybox;
        videoPlayer.Pause();
    }

    public void StartVideo()
    {
        StartCoroutine(FadeAndSwitchVideo(videoMaterial, videoPlayer.Play));
    }


    public void PauseVideo()
    {
        StartCoroutine(FadeAndSwitchVideo(_skyMaterial, videoPlayer.Pause));
    }

    private IEnumerator FadeAndSwitchVideo(Material targetMaterial, Action onCompleteAction)
    {
        fadeCanvas.QuickFadeIn();
        yield return new WaitForSeconds(fadeDuration);

        // Perform actions after fading in 
        HideOrShowObjects(targetMaterial);
        fadeCanvas.QuickFadeOut();

        // Perform actions after fading out 
        RenderSettings.skybox = targetMaterial; 
        onCompleteAction.Invoke();
    }

    private void HideOrShowObjects (Material targetMaterial)
    {
        SetObjectsActive(targetMaterial.Equals(_skyMaterial));
    } 

    private void SetObjectsActive(bool isActive){
        foreach (GameObject obj in objectsInScene)
        {
            obj.SetActive(isActive);
        }
    }
}







