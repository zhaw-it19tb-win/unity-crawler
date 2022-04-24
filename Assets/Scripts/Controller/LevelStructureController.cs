using System;
using System.Collections.Generic;

public static class LevelStructureController
{
    public static LevelModel GetGrassLevelModel()
    {
        var grassSceneTeleporterRelations = LevelStructureController.GetGrassLevelSceneTeleporterRelations();
        MapStructureGenerator.GenerateMapStructure(grassSceneTeleporterRelations);

        return new LevelModel
        {
            Name = "Grass",
            SceneTeleporterRelations = grassSceneTeleporterRelations,
            StartLocation = CardinalDirection.East
        };
    }

    public static LevelModel GetUnderwaterLevelMode()
    {
        var underwaterSceneTeleporterRelations = GetUnderwaterLevelSceneTeleporterRelations();
        MapStructureGenerator.GenerateMapStructure(underwaterSceneTeleporterRelations);

        return new LevelModel
        {
            Name = "Underwater",
            SceneTeleporterRelations = underwaterSceneTeleporterRelations,
            StartLocation = CardinalDirection.North
        };
    }

    public  static LevelModel GetRandomDungeonLevelModel()
    {
        var randomDungeonSceneTeleporterRelations = GetRandomDungeonLevelSceneTeleporterRelations();
        MapStructureGenerator.GenerateMapStructure(randomDungeonSceneTeleporterRelations);

        return new LevelModel
        {
            Name = "RandomDungeon",
            SceneTeleporterRelations = randomDungeonSceneTeleporterRelations,
            StartLocation = CardinalDirection.West
        };
    }

    private static List<SceneTeleportersRelationModel> GetRandomDungeonLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
            {
                SceneName = "ProcDungeon_1",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.North,
                        IsEntrance = true,
                        TargetSceneName = "MainScene"
                    }
                }
            }
        };
    }

    private static List<SceneTeleportersRelationModel> GetGrassLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
            {
                SceneName = "HorizontalPath",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West,
                        IsEntrance = true,
                        TargetSceneName = "MainScene"
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneName = "VerticalPath",
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
            new SceneTeleportersRelationModel
            {
                SceneName = "DungeonLevel_1",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.North,
                        TargetSceneName = "MainScene",
                        IsExit = true
                    }
                }
            }/*,
            new SceneTeleportersRelationModel
            {
                SceneName = "DungeonLevel_2", 
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
            }*/
        };
    }

    private static List<SceneTeleportersRelationModel> GetUnderwaterLevelSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
            {
                SceneName = "SeaCoast",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West,
                        TargetSceneName = "MainScene",
                        IsEntrance = true
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneName = "Underwater",
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
            new SceneTeleportersRelationModel
            {
                SceneName = "UnderwaterBoss",
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
            }
        };
    }

    private static List<SceneTeleportersRelationModel> GetLevelIntroSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
            {
                SceneName = "MainScene",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West,
                        TargetSceneName = "ProcDungeon_1"
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneName = "ProcDungeon_1",
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.North,
                        TargetSceneName = "MainScene"
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneName = "DungeonLevel_1", 
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
            }
        };
    }
}