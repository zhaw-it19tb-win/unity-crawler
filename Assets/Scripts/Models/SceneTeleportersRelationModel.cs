using System.Collections.Generic;

public class SceneTeleportersRelationModel
{
    public string SceneName { get; set; }
    public List<TeleporterModel> Teleporters { get; set; }

    public bool IsChallenge { get; set; }
}