using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelTitle : MonoBehaviour
{
    private Text text;

    public float stayTime = 2.0f;
    public float fadeTime = 1.0f;

    [SerializeField]
    private LevelList levelList;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = levelList.Levels[levelList.CurrentIndex].humanName;
    }

    private void Update()
    {
        Color textColor = text.color;
        textColor.a = Mathf.Clamp(-Time.timeSinceLevelLoad / fadeTime + stayTime / fadeTime + 1, 0, 1);

        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            text.color = textColor;
        }
    }
}
