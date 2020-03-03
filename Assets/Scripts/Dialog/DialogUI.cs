using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    private Text textDisplay;
    public GameObject box;
    private string[] sentences;
    private int index;
    public float typingSpeed;
    public bool valid;
    public GameObject player;

    public string[] startSentences;
    public bool[] ifPlayer;
   
    void Start()
    {
        textDisplay = GetComponentInChildren<Text>();
        Sentence(startSentences, ifPlayer);
    }

    void Update()
    {
        valid = (textDisplay.text == "" && index == 0);
    }

    IEnumerator Type()
    {
        textDisplay.color = ifPlayer[index] ? Color.white : Color.red;
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Sentence(string[] s, bool[] p) {
        sentences = s;
        ifPlayer = p;
        if (sentences.Length > 0)
        {
            box.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
                    box.SetActive(false);
                    player.GetComponent<PlayerMovement>().enabled = true;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
}
