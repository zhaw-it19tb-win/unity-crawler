using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    //[SerializeField]
    //private bool canTeleport;

    //private void Update()
    //{
    //    if (canTeleport)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            canTeleport = false;


    //            LoadNewScene("Assets/Scenes/HorizontalPath");
    //        }

    //    }
    //}


    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //canTeleport = true;
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
