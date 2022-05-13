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

    [SerializeField()]
    public GameObject DamagePopup;

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
            var level = LevelModels.Single(m => m.IsBossDefeated);
            level.LevelSolvedCounter++;
            level.IsBossDefeated = false;

            switch (finishedLevelName)
            {
                case "Sky":
                    level.SceneTeleporterRelations = LevelStructureController.GetSkyLevelRandomizedSceneTeleporterRelations();
                    break;
                case "Underwater":
                    level.SceneTeleporterRelations = LevelStructureController.GetUnderwaterLevelRandomizedSceneTeleporterRelations();
                    break;
                case "Dessert":
                    level.SceneTeleporterRelations = LevelStructureController.GetDessertLevelRandomizedSceneTeleporterRelations();
                    break;
                case "Fire":
                    level.SceneTeleporterRelations = LevelStructureController.GetFireLevelRandomizedSceneTeleporterRelations();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    private void InitializeLevelModels()
    {
        LevelModels = new List<LevelModel>();
        LevelModels.Add(LevelStructureController.GetSkyLevelModel());
        LevelModels.Add(LevelStructureController.GetUnderwaterLevelMode());
        LevelModels.Add(LevelStructureController.GetDessertLevelModel());
        LevelModels.Add(LevelStructureController.GetFireLevelModel());
    }

    private void DoWhenSceneLoads(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: ");
        Debug.Log(scene);
        Debug.Log("loading mode: ");
        Debug.Log(mode);

    }
}