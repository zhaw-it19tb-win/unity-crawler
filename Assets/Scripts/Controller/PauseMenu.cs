using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool gameisPaused;
    public GameObject pauseMenu, mainMenu, optionsMenu;
    public GameObject firstButtonMainMenu, firstButtonOptionsMenu, nextButtonOptionsMenu;

    /// <summary>
    /// When [Esc] is pressed stop the game and show the pause menu.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameisPaused)
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
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gameisPaused = false;
    }

    /// <summary>
    /// Stop the game and show the pause menu.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        mainMenu.SetActive(true);
        gameisPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonMainMenu);
    }

    /// <summary>
    /// Quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Select first button of the options menu (use when opening the options menu).
    /// </summary>
    public void FocusFirstButtonOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonOptionsMenu);
    }

    /// <summary>
    /// Select next button after the options menu (use when closing the options menu).
    /// </summary>
    public void FocusNexttButtonOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextButtonOptionsMenu);
    }
}
