using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject buttonTemplate;
    public List<Object> scenes = new List<Object>();

    void Start()
    {
        foreach (Object scene in scenes)
        {
            GameObject buttonObject = Instantiate(buttonTemplate, transform);
            if (eventSystem.firstSelectedGameObject == null)
            {
                eventSystem.firstSelectedGameObject = buttonObject;
            }

            buttonObject.GetComponentInChildren<Text>().text = scene.name;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(scene.name);
            });
        }
    }
}
