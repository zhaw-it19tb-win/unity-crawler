using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameUtil : MonoBehaviour
{
    public static GameUtil GU;
    public static CardinalDirection TargetTeleporterLocation;
    // TODO OSW: Extract to some level class or something (fixed scenes per level)
    // Example: for desert level only use desert scenes....
    public static List<SceneTeleportersRelation> SceneTeleporterRelations = new List<SceneTeleportersRelation>
    {
        new SceneTeleportersRelation
        {
            SceneIndex = 0, // MainScene
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = CardinalDirection.East
                }
            }
        },
        new SceneTeleportersRelation
        {
            SceneIndex = 1, // HorizontalPath
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = CardinalDirection.West
                },
                new TeleporterModel
                {
                    Location = CardinalDirection.East
                }
            }
        },
        new SceneTeleportersRelation
        {
            SceneIndex = 2, // VerticalPath
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = CardinalDirection.North
                },
                new TeleporterModel
                {
                    Location = CardinalDirection.South
                }
            }
        },
        new SceneTeleportersRelation
        {
            SceneIndex = 3, // DungeonLevel1
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = CardinalDirection.West
                },
                new TeleporterModel
                {
                    Location = CardinalDirection.North
                }
            }
        },
        new SceneTeleportersRelation
        {
            SceneIndex = 4, // DungeonLevel2
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = CardinalDirection.North // Start
                },
                new TeleporterModel
                {
                    Location = CardinalDirection.South // End
                }
            }
        }
    };

    void Awake()
    {
        if (GU != null)
            GameObject.Destroy(GU);
        else
            GU = this;

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += DoWhenSceneLoads;

        GenerateMapStructure();
    }

    private void DoWhenSceneLoads(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene: ");
        Debug.Log(scene);
        Debug.Log("loading mode: ");
        Debug.Log(mode);

    }

    private void GenerateMapStructure()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;

        while (SceneTeleporterRelations.Count(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue)) >= 2)
        {
            var originScene = SceneTeleporterRelations.FirstOrDefault(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue));

            int targetSceneIndex;
            do
            {
                targetSceneIndex = Random.Range(1, sceneCount);
            } while (
                targetSceneIndex == originScene.SceneIndex ||
                !SceneTeleporterRelations.Any(r => r.SceneIndex == targetSceneIndex && r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue))
            );

            originScene.Teleporters.FirstOrDefault(t => !t.TargetSceneIndex.HasValue).TargetSceneIndex = targetSceneIndex;
            SceneTeleporterRelations.FirstOrDefault(r => r.SceneIndex == targetSceneIndex)
                .Teleporters.FirstOrDefault(t => !t.TargetSceneIndex.HasValue).TargetSceneIndex = originScene.SceneIndex;
        }
    }
}

//TODO OSW: Extract to seperate files

public class SceneTeleportersRelation
{
    public int SceneIndex { get; set; }
    public IList<TeleporterModel> Teleporters { get; set; }
}

public class TeleporterModel
{
    public int? TargetSceneIndex { get; set; }
    public CardinalDirection Location { get; set; }
}

public enum CardinalDirection
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}