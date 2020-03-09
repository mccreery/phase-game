using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserStart : MonoBehaviour
{
    public GameObject barrier;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Destroy(barrier);
        }
    }
}
