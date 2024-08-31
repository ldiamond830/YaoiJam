using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    DialogueUIController controller;
    [SerializeField]
    bool launchDialogueOnStartScene;


    private void Start()
    {
        if (launchDialogueOnStartScene) StartDialogue();
    }

    public void StartDialogue()
    {
        controller.ShowDialogueUI();
    }
}
