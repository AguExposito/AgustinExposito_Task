using Code.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingRod : MonoBehaviour
{
    public ElephantDialogueManager elephantDialogueManager;
    public GameObject dialogueMark;
    public void OnAnimEnd() {
        elephantDialogueManager.elephantCurrentDialogueIndex++;
        dialogueMark.SetActive(true);
    }
}
