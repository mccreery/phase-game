using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    private TextMeshPro textDisplay;
    private string[] sentences;
    private int index;
    public float typingSpeed;
    public bool valid;

    public string[] startSentences;
   
    void Start()
    {
        textDisplay = GetComponentInChildren<TextMeshPro>();
        Sentence(startSentences);
    }

    void Update()
    {
        valid = (textDisplay.text == "" && index == 0);
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Sentence(string[] s) {
        sentences = s;
        StartCoroutine(Type());
    }

    public void Next()
    {
        if (textDisplay.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplay.text = "";
                index = 0;
            }
        }
    }

}
