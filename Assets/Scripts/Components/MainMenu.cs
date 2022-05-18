using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool firstCall = true;

    /// <summary>
    /// Load the next scene according to build index (see File\Build Settings...). Called by the PlayButton.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Exit the application. Called by the QuitButton.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// If returning from the pause menu remove the redundant EventSystem.
    /// </summary>
    void OnEnable()
    {
        if (firstCall)
        {
            firstCall = false;
        } else
        {
            Destroy(GameObject.Find("MenuEventSystem"));
        }
    }
}
