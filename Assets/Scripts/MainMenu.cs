using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button play;

    void Start()
    {
        play.onClick.AddListener(Play);
    }

    void Play()
    {
        //SceneManager.LoadScene("Prototype");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
