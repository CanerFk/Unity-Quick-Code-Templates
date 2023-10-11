using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    private Text nameText;
    private Text dialogueText;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void startDialogue(Dialogue dialogue, Text dialogueUiText)
    {
        dialogueText = dialogueUiText;
        dialogueText.gameObject.GetComponent<Text>().enabled = true;
        
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        displayNextSentence();
    }
    public void displayNextSentence()
    {
        if(sentences.Count==0)
        {
            StartCoroutine(EndSentence());
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(nextSentence());
    }
    IEnumerator TypeSentence(string sentence, float typingSpeed = 0.05f)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    IEnumerator EndSentence()
    {
        yield return new WaitForSeconds(2.5f);
        endDialogue();
    }
    IEnumerator nextSentence()
    {
        yield return new WaitForSeconds(2f);
        displayNextSentence();
    }
    public void endDialogue()
    {
        dialogueText.gameObject.GetComponent<Text>().enabled = false;
    }
}
