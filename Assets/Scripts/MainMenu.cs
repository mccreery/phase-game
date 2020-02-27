using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button play;
    public Button options;
    public Button quit;

    void Start()
    {
        play.onClick.AddListener(Play);
        options.onClick.AddListener(Options);
        quit.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    void Quit()
    {
        Application.Quit();
    }

}
