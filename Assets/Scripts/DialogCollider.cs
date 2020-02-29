using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public GameObject player;
    public string sentence;
    private Dialog dialog;

    void Start()
    {
        dialog = player.GetComponent<Dialog>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            dialog.Sentence(sentence);
        }
    }
}
