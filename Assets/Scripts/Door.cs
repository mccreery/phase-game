using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;

    public SpriteRenderer top, bottom;
    public Sprite topOpenSprite, bottomOpenSprite;

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
            top.sprite = topOpenSprite;
            bottom.sprite = bottomOpenSprite;
            LevelManager.Instance.SkipLevels(1);
        }
    }
}
