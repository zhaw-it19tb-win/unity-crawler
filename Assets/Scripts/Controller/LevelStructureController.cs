using System.Collections.Generic;

public static class LevelStructureController
{
    public static List<SceneTeleportersRelationModel> GetLevelOneSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
            {
                SceneIndex = 0,
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West,
                        TargetSceneIndex = 5
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneIndex = 5,
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.North,
                        TargetSceneIndex = 0
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneIndex = 1,
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
                SceneIndex = 2,
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
                SceneIndex = 3,
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
                SceneIndex = 4, 
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
                SceneIndex = 0,
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.East
                    },
                    new TeleporterModel
                    {
                        Location = CardinalDirection.West,
                        TargetSceneIndex = 5
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneIndex = 5,
                Teleporters = new List<TeleporterModel>
                {
                    new TeleporterModel
                    {
                        Location = CardinalDirection.North,
                        TargetSceneIndex = 0
                    }
                }
            },
            new SceneTeleportersRelationModel
            {
                SceneIndex = 3, 
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