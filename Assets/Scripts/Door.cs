using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;

    public SpriteRenderer top, bottom;
    public Sprite topOpenSprite, bottomOpenSprite;

    public AudioSource openSound;
    public AudioSource lockedSound;

    public GameObject keyPrompt;
    public GameObject openPrompt;

    private bool touchingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set player flag
        touchingPlayer = touchingPlayer || collision.gameObject.CompareTag("Player");

        if (touchingPlayer)
        {
            if (player.GetComponent<PlayerMovement>().exit)
            {
                openPrompt.SetActive(true);
            }
            else
            {
                keyPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Clear player flag
        touchingPlayer = touchingPlayer && !collision.gameObject.CompareTag("Player");

        if (!touchingPlayer)
        {
            keyPrompt.SetActive(false);
            openPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (touchingPlayer && Input.GetButtonDown("Interact"))
        {
            if (player.GetComponent<PlayerMovement>().exit)
            {
                StartCoroutine(Open());
                enabled = false;
            }
            else
            {
                lockedSound.Play();
            }
        }
    }

    private IEnumerator Open()
    {
        Time.timeScale = 0.0f;

        // Play sound effect and open door
        openSound.Play();
        yield return new WaitForSecondsRealtime(openSound.clip.length / 2);
        top.sprite = topOpenSprite;
        bottom.sprite = bottomOpenSprite;
        yield return new WaitForSecondsRealtime(openSound.clip.length / 2);

        Time.timeScale = 1.0f;

        LevelManager.Instance.SkipLevels(1);
    }
}
