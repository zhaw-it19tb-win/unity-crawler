using System.Collections.Generic;

public static class LevelStructureController
{
    public static List<SceneTeleportersRelationModel> GetLevelOneSceneTeleporterRelations()
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
                SceneName = "HorizontalPath",
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
                        Location = CardinalDirection.North
                    }
                }
            },
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
            }
        };
    }

    public static List<SceneTeleportersRelationModel> GetLevelIntroSceneTeleporterRelations()
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