using System.Collections.Generic;

public class LevelModel
{
    public string Name { get; set; }

    public List<SceneTeleportersRelationModel> SceneTeleporterRelations { get; set; }

    public CardinalDirection StartLocation { get; set; }

    public bool IsBossDefeated { get; set; }

    public bool IsActive { get; set; }
}
