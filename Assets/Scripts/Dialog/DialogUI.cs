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

    public GameObject friendIcon;
    public GameObject enemyIcon;

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

    private IEnumerator DisplayText(DialogText text)
    {
        textDisplay.text = string.Empty;

        for (int i = 0; i <= text.text.Length; i++)
        {
            if (sentenceQueue.Peek() != text)
            {
                break;
            }

            textDisplay.text = text.text.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public bool auto = false;
    public float autoDelayPerChar = 0.05f;

    public IEnumerator Advance()
    {
        if (sentenceQueue.Count == 0)
        {
            Open = false;
        }
        else
        {  
            DialogText next = sentenceQueue.Peek();

            textDisplay.color = next.textColor;
            enemyIcon.SetActive(next.enemy);
            friendIcon.SetActive(!next.enemy);

            nextSentence = false;
            yield return StartCoroutine(DisplayText(next));
            nextSentence = true;

            if (auto && !next.wait)
            {
                yield return new WaitForSeconds(autoDelayPerChar * next.text.Length);

                if (next == sentenceQueue.Peek())
                {
                    sentenceQueue.Dequeue();
                    yield return Advance();
                }
            }
            else if (next == sentenceQueue.Peek())
            {
                sentenceQueue.Dequeue();
            }
        }
    }

    public void Enqueue(DialogText dialogText)
    {
        sentenceQueue.Enqueue(dialogText);
        Open = true;
    }

    public void Enqueue(IEnumerable<DialogText> dialogText)
    {
        sentenceQueue.Clear();
        foreach (DialogText text in dialogText)
        {
            Enqueue(text);
        }
        StartCoroutine(Advance());
    }
}

[Serializable]
public class DialogText
{
    public string text;
    public Color textColor;
    public bool enemy;
    public bool wait;
}
