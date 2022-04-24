using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameUtil : MonoBehaviour
{
    public static GameUtil GU;
    public static CardinalDirection TargetTeleporterLocation;
    public static List<LevelModel> LevelModels;

    void Awake()
    {
        if (GU == null)
        {
            GU = this; 
            
            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += DoWhenSceneLoads;

            InitializeLevelModels();
        }
        
    }

    private void InitializeLevelModels()
    {
        LevelModels = new List<LevelModel>();
        LevelModels.Add(LevelStructureController.GetRandomDungeonLevelModel());
        LevelModels.Add(LevelStructureController.GetGrassLevelModel());
        LevelModels.Add(LevelStructureController.GetUnderwaterLevelMode());
    }

    private void DoWhenSceneLoads(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: ");
        Debug.Log(scene);
        Debug.Log("loading mode: ");
        Debug.Log(mode);

    }
}