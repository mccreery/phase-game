using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private string[] sentences;
    private int index;
    public float typingSpeed;
    public bool valid;
    private bool player;

    public string[] startSentences;

    void Start()
    {
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
            textDisplay.color = player ? Color.white : Color.red;
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Sentence(string[] s) {
        sentences = s;
        if (s.Length > 0)
        {
            StartCoroutine(Type());   
        }
    }

    public void Next()
    {
        if (sentences.Length > 0)
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
}
