using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    private TextMeshPro textDisplay;
    private string sentence;
    private int index;
    public float typingSpeed;
   
    void Start()
    {
        textDisplay = GetComponentInChildren<TextMeshPro>();
    }

    IEnumerator Type()
    {
        foreach(char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Sentence(string s) {
        sentence = s;
        textDisplay.text = "";
        StartCoroutine(Type());
    }

    public void Clear()
    {
        if (textDisplay.text == sentence)
        {
            textDisplay.text = "";
        }
    }

}
