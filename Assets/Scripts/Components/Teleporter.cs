using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            LoadNewScene("HorizontalPath");
        }
    }

    private void LoadNewScene(string scenePath)
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenePath, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(activeScene);
    }
}
