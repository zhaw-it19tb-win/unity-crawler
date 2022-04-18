using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string positionTag;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                LoadNewScene("HorizontalPath");
            }
        }
    }

    private void LoadNewScene(string scenePath)
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Stop("theme");
        FindObjectOfType<AudioManager>().Play("boss");
    }
}
