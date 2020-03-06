using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect Instance { get; private set; }

    public EventSystem eventSystem;
    public Transform buttonParent;
    public GameObject buttonTemplate;

    [SerializeField]
    private List<NamedScene> levels;
    public IReadOnlyCollection<NamedScene> Levels => levels.AsReadOnly();

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
        AddButtons();
    }

    private void AddButtons()
    {
        foreach (NamedScene namedScene in levels)
        {
            GameObject buttonObject = Instantiate(buttonTemplate, buttonParent);
            if (eventSystem.firstSelectedGameObject == null)
            {
                eventSystem.firstSelectedGameObject = buttonObject;
            }

            buttonObject.GetComponentInChildren<Text>().text = namedScene.humanName;

            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(namedScene.scene);
            });
        }
    }

    public int CurrentLevel
    {
        get
        {
            string currentPath = SceneManager.GetActiveScene().path;
            return levels.FindIndex(namedScene => namedScene.scene.ScenePath == currentPath);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levels[levelIndex].scene);
    }

    public void SkipLevels(int numLevels)
    {
        LoadLevel(CurrentLevel + numLevels);
    }

    public void ReloadLevel()
    {
        SkipLevels(0);
    }
}

[Serializable]
public struct NamedScene
{
    public SceneReference scene;
    public string humanName;
}
