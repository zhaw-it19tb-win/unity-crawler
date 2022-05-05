using System;
using System.Collections.Generic;

public static class LevelStructureController
{
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
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_2", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_5", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.North, "SkyEntry", CardinalDirection.South, "SkyBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "SkyBoss", CardinalDirection.North, "SkyEntry")
        };
    }

    private static List<SceneTeleportersRelationModel> GetUnderwaterLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_2", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_4", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_5", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "UnderwaterEntry", CardinalDirection.East, "UnderwaterBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "UnderwaterBoss", CardinalDirection.East, "UnderwaterEntry")
        };
    }

    private static List<SceneTeleportersRelationModel> GetDessertLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_2", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_5", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "DesertEntry", CardinalDirection.East, "DesertBoss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "DesertBoss", CardinalDirection.North, "DesertEntry")
        };
    }

    private static List<SceneTeleportersRelationModel> GetFireLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.North, "Path_2", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "Path_0", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.South, "Path_1", CardinalDirection.North),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_3", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_4", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "Path_5", CardinalDirection.South),
            CreatePreExitTeleporterRelation(CardinalDirection.West, "FireLevel_Entry", CardinalDirection.East, "FireLevel_Boss"),
            CreateExitTeleporterRelation(CardinalDirection.West, "FireLevel_Boss", CardinalDirection.North, "FireLevel_Entry")
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
                        TargetSceneName = "MainScene"
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
                        TargetSceneName = "MainScene",
                        IsExit = true
                    }
                }
        };
    }
}