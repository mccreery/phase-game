﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public SceneReference mainMenu;
    public SceneReference levelSelectMenu;
    public SceneReference volumeMenu;
    public SceneReference credits;
    public SceneReference endScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }

    private LevelList levelList;

    public void StartGame()
    {
        SceneManager.LoadScene(levelList.Levels[0].scene);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(Instance.mainMenu);
    }

    public void GoEnd()
    {
        SceneManager.LoadScene(Instance.endScreen);
    }

    public void GoLevelSelect()
    {
        SceneManager.LoadScene(Instance.levelSelectMenu);
    }

    public void GoVolume()
    {
        SceneManager.LoadScene(Instance.volumeMenu);
    }

    public void GoCredits()
    {
        SceneManager.LoadScene(Instance.credits);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
