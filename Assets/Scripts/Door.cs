using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;

    public SpriteRenderer top, bottom;
    public Sprite topOpenSprite, bottomOpenSprite;

    public AudioSource openSound;

    private bool touchingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set player flag
        touchingPlayer = touchingPlayer || collision.gameObject.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Clear player flag
        touchingPlayer = touchingPlayer && !collision.gameObject.CompareTag("Player");
    }

    private void Update()
    {
        if (touchingPlayer && player.GetComponent<PlayerMovement>().exit && Input.GetButtonDown("Interact"))
        {
            StartCoroutine(Open());
            enabled = false;
        }
    }

    private IEnumerator Open()
    {
        // Play sound effect and open door
        openSound.Play();
        yield return new WaitForSecondsRealtime(openSound.clip.length / 2);
        top.sprite = topOpenSprite;
        bottom.sprite = bottomOpenSprite;
        yield return new WaitForSecondsRealtime(openSound.clip.length / 2);

        LevelManager.Instance.SkipLevels(1);
    }
}
