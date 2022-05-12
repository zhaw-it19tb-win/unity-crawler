using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused;
    public GameObject pauseMenu;

    /// <summary>
    /// When [Esc] is pressed stop the game and show the pause menu.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Close the pause menu and resume the game.
    /// </summary>
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameisPaused = false;
    }

    /// <summary>
    /// Stop the game and show the pause menu.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        GameisPaused = true;
    }

    /// <summary>
    /// Quit the game and go back to the main menu.
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
