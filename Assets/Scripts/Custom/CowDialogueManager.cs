using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CowDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public int cowCurrentDialogueIndex = 0;
    public string[] cowDialogue;

    public void SetDialogue()
    {
        dialogueText.text = cowDialogue[cowCurrentDialogueIndex];
        if (cowCurrentDialogueIndex == cowDialogue.Length - 1)
        {
            FindObjectOfType<IslandGameManager>().CheckCow();
        }
    }
}
