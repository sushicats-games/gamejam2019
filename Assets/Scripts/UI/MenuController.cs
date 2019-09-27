using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string GameSceneName;
    public string MainMenuSceneName;

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
