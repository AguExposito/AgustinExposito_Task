using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlowerInteractable : MonoBehaviour
{
    public GameObject matsuneHiku;
    public GameObject questionMark;
    public ChickenDialogueManager chickenDialogueManager;
    public AudioSource sfx_AS;
    public AudioClip clip;
    public GameObject[] ps;

    public void PickFlower() {
        chickenDialogueManager.flowerCount++;
        if (chickenDialogueManager.flowerCount >= 3) { 
            chickenDialogueManager.chickenCurrentDialogueIndex++;
            matsuneHiku.GetComponent<NotableObject>().IsCurrentlyDetectable = true;
            foreach (var ps in ps)
            {
                ps.SetActive(true);
            }
            questionMark.SetActive(true);
        }
        sfx_AS.PlayOneShot(clip);
    }
}
