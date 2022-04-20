using UnityEngine;
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
        GenerateMapStructure();
    }

    // TODO Refactor.... hard to read and performance? --> unit tests?
    private void GenerateMapStructure()
    {
        var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        //teleporters are always paired... if there are odd count, one will be faxe teleporter
        while (SceneTeleporterRelations.Count(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue)) >= 2)
        {
            var originScene = SceneTeleporterRelations.FirstOrDefault(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue));

            // TODO OSW Better solution for getting the an empty teleporter on another scene
            int targetSceneIndex;
            do
            {
                targetSceneIndex = Random.Range(1, sceneCount);
            } while (
                targetSceneIndex == originScene.SceneIndex && 
                SceneTeleporterRelations.Where(r => r.SceneIndex == targetSceneIndex).Any(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue))
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