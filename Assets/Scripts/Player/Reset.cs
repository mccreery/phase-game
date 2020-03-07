using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            LevelManager.Instance.ReloadLevel();
        }
    }
}
