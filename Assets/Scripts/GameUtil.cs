using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameUtil : MonoBehaviour
{
    public static GameUtil GU;
    public static CardinalDirection TargetTeleporterLocation;
    public static List<SceneTeleportersRelationModel> SceneTeleporterRelations;

    void Awake()
    {
        if (GU == null)
        {
            GU = this; 
            
            DontDestroyOnLoad(this);

            SceneManager.sceneLoaded += DoWhenSceneLoads;

            SceneTeleporterRelations = LevelStructureController.GetLevelOneSceneTeleporterRelations();
            MapStructureGenerator.GenerateMapStructure(SceneTeleporterRelations);
        }
        
    }

    private void DoWhenSceneLoads(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: ");
        Debug.Log(scene);
        Debug.Log("loading mode: ");
        Debug.Log(mode);

    }
}