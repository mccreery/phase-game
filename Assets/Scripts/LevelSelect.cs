using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Transform buttonParent;
    public GameObject buttonTemplate;

    [Header("Keyboard Navigation")]
    public EventSystem eventSystem;

    void Start()
    {
        foreach (NamedScene namedScene in LevelManager.Instance.Levels)
        {
            GameObject buttonObject = Instantiate(buttonTemplate, buttonParent);
            if (eventSystem != null && eventSystem.firstSelectedGameObject == null)
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
}
