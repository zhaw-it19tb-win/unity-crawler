using System.Collections.Generic;

public static class LevelStructureController
{
    public static List<SceneTeleportersRelationModel> GetLevelOneSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
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
            new SceneTeleportersRelationModel
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
            new SceneTeleportersRelationModel
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
            new SceneTeleportersRelationModel
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
            new SceneTeleportersRelationModel
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
    }

    public static List<SceneTeleportersRelationModel> GetLevelIntroSceneTeleporterRelations()
    {
        return new List<SceneTeleportersRelationModel>
        {
            new SceneTeleportersRelationModel
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
            new SceneTeleportersRelationModel
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
            }
        };
    }
}