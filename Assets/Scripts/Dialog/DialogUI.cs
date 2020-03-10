using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    private Text textDisplay;
    public float typingSpeed;
    public GameObject player;

    public DialogText[] startSentences;
    private Queue<DialogText> sentenceQueue;

    public CanvasGroup canvasGroup;
    public GameObject prompt;
    public bool nextSentence;

    public bool Open
    {
        get => canvasGroup.IsVisible();
        set
        {
            canvasGroup.SetVisible(value);
            //player.GetComponent<PlayerMovement>().enabled = !value;

            /*player.GetComponent<Rigidbody2D>().constraints = value
                ? RigidbodyConstraints2D.FreezeAll
                : RigidbodyConstraints2D.FreezeRotation;*/
        }
    }

    void Start()
    {
        textDisplay = GetComponentInChildren<Text>();
        sentenceQueue = new Queue<DialogText>();
        Open = false;
        Enqueue(startSentences);
    }

    private IEnumerator DisplayText(string text)
    {
        textDisplay.text = string.Empty;

        for (int i = 0; i <= text.Length; i++)
        {
            textDisplay.text = text.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public IEnumerator Advance()
    {
        if (sentenceQueue.Count == 0)
        {
            Open = false;
        }
        else
        {  
            DialogText next = sentenceQueue.Dequeue();
            textDisplay.color = next.textColor;

            nextSentence = false;
            //prompt.SetActive(false);
            yield return StartCoroutine(DisplayText(next.text));
            nextSentence = true;
            //prompt.SetActive(true);
        }
    }

    public void Enqueue(DialogText dialogText)
    {
        sentenceQueue.Enqueue(dialogText);
        Open = true;
    }

    public void Enqueue(IEnumerable<DialogText> dialogText)
    {
        foreach (DialogText text in dialogText)
        {
            Enqueue(text);
        }
        StartCoroutine(Advance());
    }
}

[Serializable]
public struct DialogText
{
    public string text;
    public Color textColor;
}
