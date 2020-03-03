using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public Slider volume;
    public Button back;

    void Start()
    {
        back.onClick.AddListener(Back);
    }

    void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
