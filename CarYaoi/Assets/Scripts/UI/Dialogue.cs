using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    DialogueUIController controller;

    public void StartDialogue()
    {
        controller.ShowDialogueUI();
    }
}
