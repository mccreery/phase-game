using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Cursor.visible = pauseMenu.activeSelf;
            Time.timeScale = pauseMenu.activeSelf ? 0.0f : 1.0f;
        }
    }
}
