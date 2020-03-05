using UnityEngine;

public class DialogCollider : MonoBehaviour
{
    public DialogUI dialog;
    public DialogText[] sentences;
    public bool repeat;
    private bool display = true;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (display)
            {
                dialog.Enqueue(sentences);
                display = repeat;
            }
        }
    }
}
