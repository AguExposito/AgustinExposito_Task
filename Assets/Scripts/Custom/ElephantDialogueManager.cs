using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ElephantDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public int elephantCurrentDialogueIndex = 0;
    public string[] elephantDialogue;

    public void SetDialogue()
    {
        dialogueText.text = elephantDialogue[elephantCurrentDialogueIndex];
        if (elephantCurrentDialogueIndex == elephantDialogue.Length - 1)
        {
            FindObjectOfType<IslandGameManager>().CheckElephant();
        }
    }
}
