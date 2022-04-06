using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        {
            LoadNewScene("HorizontalPath");
        }
    }

    private void LoadNewScene(string scenePath)
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync(activeScene);
    }
}
