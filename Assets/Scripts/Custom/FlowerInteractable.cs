using UnityEngine;

public class FlowerInteractable : MonoBehaviour
{
    public AudioSource sfx_AS;
    public AudioClip clip;
    public void PickFlower()
    {
        sfx_AS.PlayOneShot(clip);
    }   
}
