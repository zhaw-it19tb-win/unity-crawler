using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static bool firstCall = true;
    public GameObject firstButtonMainMenu, firstButtonOptionsMenu, nextButtonOptionsMenu;

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

    /// <summary>
    /// Select first button of the main menu.
    /// If returning from the pause menu remove the redundant EventSystem.
    /// </summary>
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonMainMenu);
        if (firstCall)
        {
            firstCall = false;
        } else
        {
            Destroy(GameObject.Find("MenuEventSystem"));
        }
    }
}
