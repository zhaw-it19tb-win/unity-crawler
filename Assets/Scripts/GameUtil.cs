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

    public static string GameId;

    public static bool IsPlayerTeleported { get; set; }

    void Awake()
    {
        if (GU == null)
        {
            GU = this;

            GameId = Guid.NewGuid().ToString("N");
            Debug.Log("GameId="+ GameId);
            
            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += DoWhenSceneLoads;

            InitializeLevelModels();
        }

        RegenerateLevelModels();        
    }

    private void RegenerateLevelModels()
    {
        if (LevelModels.Any(m => m.IsBossDefeated)) 
        {
            var finishedLevelName = LevelModels.Single(m => m.IsBossDefeated).Name;

            LevelModels.RemoveAll(l => l.IsBossDefeated);

            switch (finishedLevelName)
            {
                case "Grass":
                    LevelModels.Add(LevelStructureController.GetGrassLevelModel());
                    break;
                case "Underwater":
                    LevelModels.Add(LevelStructureController.GetUnderwaterLevelMode());
                    break;
                default:
                    LevelModels.Add(LevelStructureController.GetRandomDungeonLevelModel());
                    break;
            }
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