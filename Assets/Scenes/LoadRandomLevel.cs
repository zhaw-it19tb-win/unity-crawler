using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRandomLevel : MonoBehaviour
{
    public void LoadLevelRandomly()
    {
        SceneManager.LoadScene(Random.Range(1, 3)); 
    }
}
