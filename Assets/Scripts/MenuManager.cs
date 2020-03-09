using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public SceneReference mainMenu;
    public SceneReference levelSelectMenu;
    public SceneReference volumeMenu;
    public SceneReference credits;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }

    public void StartGame()
    {
        LevelManager.Instance.LoadLevel(0);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(Instance.mainMenu);
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
