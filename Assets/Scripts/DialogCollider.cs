using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public string[] sentence;
    public DialogUI dialog;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && dialog.valid)
        {
            dialog.Sentence(sentence);
        }
    }
}
