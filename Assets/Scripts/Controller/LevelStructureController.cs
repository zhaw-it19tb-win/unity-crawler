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
            CreateEntryTeleporterRelation(CardinalDirection.North, "SkyEntry", CardinalDirection.South),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "HorizontalPath", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "VerticalPath", CardinalDirection.South),
            CreateExitTeleporterRelation(CardinalDirection.West, "SkyBoss", CardinalDirection.North)
        };
    }

    private static List<SceneTeleportersRelationModel> GetUnderwaterLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.West, "UnderwaterEntry", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "HorizontalPath", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "VerticalPath", CardinalDirection.South),
            CreateExitTeleporterRelation(CardinalDirection.West, "UnderwaterBoss", CardinalDirection.East)
        };
    }

    private static List<SceneTeleportersRelationModel> GetDessertLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.West, "DesertEntry", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "HorizontalPath", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "VerticalPath", CardinalDirection.South),
            CreateExitTeleporterRelation(CardinalDirection.West, "DesertBoss", CardinalDirection.East)
        };
    }

    private static List<SceneTeleportersRelationModel> GetFireLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            CreateEntryTeleporterRelation(CardinalDirection.West, "FireLevel_Entry", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.West, "HorizontalPath", CardinalDirection.East),
            CreateSimpleTeleporterRelation(CardinalDirection.North, "VerticalPath", CardinalDirection.South),
            CreateExitTeleporterRelation(CardinalDirection.West, "FireLevel_Boss", CardinalDirection.North)
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

    private static SceneTeleportersRelationModel CreateExitTeleporterRelation(CardinalDirection entranceLocation, string sceneName, CardinalDirection exitLocation)
    {
        return new SceneTeleportersRelationModel
        {
            SceneName = sceneName,
            Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East,
                        TargetSceneName = "MainScene",
                        IsExit = true
                    }
                }
        };
    }
}