using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    /*
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().exit) {
            player.GetComponent<PlayerMovement>().exit = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }*/
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && player.GetComponent<PlayerMovement>().exit)
        {
            player.GetComponent<PlayerMovement>().exit = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
