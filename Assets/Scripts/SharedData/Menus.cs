using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Menus : ScriptableObject
{
    public SceneReference mainMenu;
    public SceneReference levelSelectMenu;
    public SceneReference volumeMenu;
    public SceneReference credits;
    public SceneReference endScreen;

    private LevelList levelList;

    public void LoadScene(SceneReference scene) => SceneManager.LoadScene(scene);

    public void StartGame()
    {
        SceneManager.LoadScene(levelList.Levels[0].scene);
    }

    public void GoMainMenu() => SceneManager.LoadScene(mainMenu);
    public void GoEnd() => SceneManager.LoadScene(endScreen);
    public void GoLevelSelect() => SceneManager.LoadScene(levelSelectMenu);
    public void GoVolume() => SceneManager.LoadScene(volumeMenu);
    public void GoCredits() => SceneManager.LoadScene(credits);

    public void Quit() => Application.Quit();
}
