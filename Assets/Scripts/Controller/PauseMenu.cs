using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;

    /// <summary>
    /// When [Esc] is pressed stop the game and show the pause menu.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        } else
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Quit the game and go back to the main menu.
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Close the pause menu and resume the game.
    /// </summary>
    public void Resume()
    {
        isPaused = false;
    }
}
