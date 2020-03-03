using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public string[] sentences;
    public bool[] player;
    public DialogUI dialog;

    public bool repeat;
    private bool valid;

    void Start()
    {
        valid = true;   
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && dialog.valid && valid)
        {
            valid = repeat ? true : false;
            dialog.Sentence(sentences, player);
        }
    }
}
