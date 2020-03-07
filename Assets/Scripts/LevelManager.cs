using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private List<NamedScene> levels = new List<NamedScene>();
    public IReadOnlyCollection<NamedScene> Levels => levels.AsReadOnly();

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
    }

    public int CurrentLevel
    {
        get
        {
            string currentPath = SceneManager.GetActiveScene().path;
            return Instance.levels.FindIndex(namedScene => namedScene.scene.ScenePath == currentPath);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(Instance.levels[levelIndex].scene);
    }

    public void SkipLevels(int numLevels)
    {
        LoadLevel(CurrentLevel + numLevels);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}

[Serializable]
public struct NamedScene
{
    public SceneReference scene;
    public string humanName;
}
