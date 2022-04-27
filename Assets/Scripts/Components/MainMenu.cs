using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Load the next scene according to build index (see File\Build Settings...). Called by the PlayButton.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Exit the application. Called by the QuitButton.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
