using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
