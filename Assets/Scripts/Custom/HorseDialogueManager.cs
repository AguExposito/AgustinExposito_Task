using Code.Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class HorseDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public int horseCurrentDialogueIndex = 0;
    public string[] horseDialogue;
    public TouchInteractable dirtTI;

    public void SetDialogue()
    {
        DirtITInteractable(true);
        dialogueText.text = horseDialogue[horseCurrentDialogueIndex];
        if (horseCurrentDialogueIndex == horseDialogue.Length - 1) {
            FindObjectOfType<IslandGameManager>().CheckHorse();
        }
    }
    public void DirtITInteractable(bool state) {
        dirtTI.IsInteractableOnStart = state;
        dirtTI.IsInteractable = state;
    }
}
