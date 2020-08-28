using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndDialogue : MonoBehaviour
{
    public DialogUI dialog;

    public Image fadeIn;
    private CanvasRenderer canvasRenderer;

    private void Awake()
    {
        canvasRenderer = fadeIn.gameObject.GetComponent<CanvasRenderer>();
    }

    public Menus menus;

    public DialogText[] text;

    private void Start()
    {
        canvasRenderer.SetAlpha(0);
        dialog.Enqueue(text);
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitUntil(() => dialog.nextSentence);
        yield return new WaitForSecondsRealtime(2f);

        fadeIn.CrossFadeAlpha(1f, 2f, true);
        AudioSource music = BackgroundMusic.GetSource();

        float alpha;
        while ((alpha = canvasRenderer.GetAlpha()) < 1f)
        {
            music.volume = 1f - alpha;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadSceneAsync(menus.mainMenu).completed += op => music.volume = 1f;
    }
}
