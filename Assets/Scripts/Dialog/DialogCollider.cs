using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public DialogUI dialog;
    public DialogText[] sentences;
    public bool repeat;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            enabled = repeat;
            dialog.Enqueue(sentences);
        }
    }
}
