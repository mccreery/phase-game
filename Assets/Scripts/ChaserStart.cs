using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserStart : MonoBehaviour
{
    public GameObject chaser;
    public GameObject barrier;

    void Start()
    {
        chaser.active = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            chaser.active = true;
            Destroy(barrier);
        }
    }
}
