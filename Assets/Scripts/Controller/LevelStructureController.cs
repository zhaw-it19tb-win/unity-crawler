using System;
using System.Collections.Generic;

public static class LevelStructureController
{
    private static readonly string MAIN_SCENE_NAME = "MainScene";

    public static LevelModel GetSkyLevelModel()
    {
        return new LevelModel
        {
            Name = "Sky",
            SceneTeleporterRelations = GetSkyLevelRandomizedSceneTeleporterRelations(),
            StartLocation = CardinalDirection.East,
            ChallengeSceneName = "Sky"
        };
    }

    public static LevelModel GetUnderwaterLevelMode()
    {

        return new LevelModel
        {
            Name = "Underwater",
            SceneTeleporterRelations = GetUnderwaterLevelRandomizedSceneTeleporterRelations(),
            StartLocation = CardinalDirection.North,
            ChallengeSceneName = "Underwater"
        };
    }

    public  static LevelModel GetDessertLevelModel()
    {
        return new LevelModel
        {
            Name = "Dessert",
            SceneTeleporterRelations = GetDessertLevelRandomizedSceneTeleporterRelations(),
            StartLocation = CardinalDirection.West,
            ChallengeSceneName = "Desert"
        };
    }

    public static LevelModel GetFireLevelModel()
    {
        return new LevelModel
        {
            Name = "Fire",
            SceneTeleporterRelations = GetFireLevelRandomizedSceneTeleporterRelations(),
            StartLocation = CardinalDirection.South,
            ChallengeSceneName = "FireLevel"
        };
    }

    public static List<SceneTeleportersRelationModel> GetSkyLevelRandomizedSceneTeleporterRelations()
    {
        var relationModel = GetSkyLevelSceneTeleporterRelations();

        MapStructureGenerator.GenerateMapStructure(relationModel);

        return relationModel;
    }

    public static List<SceneTeleportersRelationModel> GetUnderwaterLevelRandomizedSceneTeleporterRelations()
    {
        var relationModel = GetUnderwaterLevelSceneTeleporterRelations();

        MapStructureGenerator.GenerateMapStructure(relationModel);

        return relationModel;
    }

    public static List<SceneTeleportersRelationModel> GetDessertLevelRandomizedSceneTeleporterRelations()
    {
        var relationModel = GetDessertLevelSceneTeleporterRelations();

        MapStructureGenerator.GenerateMapStructure(relationModel);

        return relationModel;
    }

    public static List<SceneTeleportersRelationModel> GetFireLevelRandomizedSceneTeleporterRelations()
    {
        var relationModel = GetFireLevelSceneTeleporterRelations();

        MapStructureGenerator.GenerateMapStructure(relationModel);

        return relationModel;
    }

    private static List<SceneTeleportersRelationModel> GetSkyLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "ProcDungeon_1", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.North, "SkyEntry", CardinalDirection.South, "SkyBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "SkyBoss", CardinalDirection.North, "SkyEntry"),
            CreateChallengeTeleporterRelation(CardinalDirection.West, "Sky", CardinalDirection.North, "SkyBoss")
        };
    }

    private static List<SceneTeleportersRelationModel> GetUnderwaterLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "ProcDungeon_1", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_4", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_5", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.East, "Path_6", CardinalDirection.West),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "UnderwaterEntry", CardinalDirection.East, "UnderwaterBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "UnderwaterBoss", CardinalDirection.East, "UnderwaterEntry"),
            CreateChallengeTeleporterRelation(CardinalDirection.West, "Underwater", CardinalDirection.East, "UnderwaterBoss")
        };
    }

    private static List<SceneTeleportersRelationModel> GetDessertLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "ProcDungeon_1", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_7", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_8", CardinalDirection.East),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "DesertEntry", CardinalDirection.East, "DesertBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "DesertBoss", CardinalDirection.North, "DesertEntry"),
            CreateChallengeTeleporterRelation(CardinalDirection.West, "Desert", CardinalDirection.East, "DesertBoss")
        };
    }

    private static List<SceneTeleportersRelationModel> GetFireLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "ProcDungeon_1", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_2", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "FireLevel_Entry", CardinalDirection.East, "FireLevel_Boss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "FireLevel_Boss", CardinalDirection.North, "FireLevel_Entry"),
            CreateChallengeTeleporterRelation(CardinalDirection.West, "FireLevel", CardinalDirection.East, "FireLevel_Boss")
        };
    }

    private static SceneTeleportersRelationModel CreateEntryTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = entranceLocation,
                    IsEntrance = true,
                    TargetSceneName = MAIN_SCENE_NAME
                },
                new TeleporterModel
                {
                    Location = exitLocation
                }
            }
        };
    }

    private static SceneTeleportersRelationModel CreateSimpleTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = entranceLocation
                    },
                    new TeleporterModel
                    {
                        Location = exitLocation
                    }
                }
        };
    }

    private static SceneTeleportersRelationModel CreatePreExitTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation, string exitSceneName = null)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = exitLocation,
                        TargetSceneName = exitSceneName
                    },
                    new TeleporterModel
                    {
                        Location = entranceLocation
                    }
                }
        };
    }

    private static SceneTeleportersRelationModel CreateExitTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation, string entranceSceneName = null)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = entranceLocation,
                    TargetSceneName = entranceSceneName
                },
                new TeleporterModel
                {
                    Location = exitLocation,
                    TargetSceneName = MAIN_SCENE_NAME,
                    IsExit = true
                }
            }
        };
    }

    private static SceneTeleportersRelationModel CreateChallengeTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation, string entranceSceneName)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
            {
                new TeleporterModel
                {
                    Location = entranceLocation,
                    TargetSceneName = entranceSceneName,
                    IsChallengeSceneTeleporter = true
                },
                new TeleporterModel
                {
                    Location = exitLocation,
                    TargetSceneName = MAIN_SCENE_NAME,
                    IsChallengeSceneTeleporter = true,
                    IsExit = true
                }
            }
        };
    }
}