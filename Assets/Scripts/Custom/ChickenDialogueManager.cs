using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ChickenDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public int chickenCurrentDialogueIndex = 0;
    public int flowerCount = 0;
    public string[] chickenDialogue;

    public void SetDialogue()
    {
        dialogueText.text = chickenDialogue[chickenCurrentDialogueIndex];
        if (chickenCurrentDialogueIndex == chickenDialogue.Length - 1)
        {
            GetComponent<RemoveItem>().Remove();
            FindObjectOfType<IslandGameManager>().CheckChicken();
        }
    }
}
