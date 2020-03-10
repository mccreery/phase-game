using System.Collections;
using System.Collections.Generic;
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
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Cursor.visible = pauseMenu.activeSelf;
        }
    }
}
