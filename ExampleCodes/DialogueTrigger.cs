using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Text textUI;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue,textUI);
    }
}
