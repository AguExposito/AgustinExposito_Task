using Code.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickupSack : MonoBehaviour
{
    public TouchInteractable fishingRodTI;
    public GameObject questionMark;
    public Animator elephantAnim;
    public AudioSource sfx_AS;
    public AudioClip clip;

    public void OnPickup()
    {
        fishingRodTI.IsInteractable = true;
        FindAnyObjectByType<ElephantDialogueManager>().elephantCurrentDialogueIndex++;
        elephantAnim.SetTrigger("Idle");
        questionMark.SetActive(true);
        sfx_AS.PlayOneShot(clip);
    }
}
