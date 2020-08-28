using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelList : ScriptableObject
{
    [SerializeField]
    private List<NamedScene> levels = new List<NamedScene>();
    public ReadOnlyCollection<NamedScene> Levels => levels.AsReadOnly();

    public int CurrentIndex
    {
        get
        {
            string currentPath = SceneManager.GetActiveScene().path;
            return levels.FindIndex(namedScene => namedScene.scene.ScenePath == currentPath);
        }
    }
}

[Serializable]
public struct NamedScene
{
    public SceneReference scene;
    public string humanName;
    public bool hasDevice;
}
