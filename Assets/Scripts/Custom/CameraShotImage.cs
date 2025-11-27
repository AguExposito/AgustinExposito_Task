using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShotImage : MonoBehaviour
{
    public Camera mainCamera;                
    public RawImage galleryPrefab;           
    public Transform galleryParent;
    public AudioSource audioSource;
    public AudioClip clip;

    private RenderTexture tempRT;

    public void TakePhoto()
    {
        StartCoroutine(CaptureRoutine());
        audioSource.PlayOneShot(clip);
    }

    private IEnumerator CaptureRoutine()
    {
        tempRT = new RenderTexture(Screen.width, Screen.height, 24);

        RenderTexture originalRT = mainCamera.targetTexture;

        mainCamera.targetTexture = tempRT;

        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(tempRT.width, tempRT.height, TextureFormat.RGB24, false);
        RenderTexture.active = tempRT;
        photo.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);
        photo.Apply();

        mainCamera.targetTexture = originalRT;
        RenderTexture.active = null;

        Destroy(tempRT);

        AddToGallery(photo);
    }

    private void AddToGallery(Texture2D texture)
    {
        RawImage img = Instantiate(galleryPrefab, galleryParent);
        img.texture = texture;
    }

}
