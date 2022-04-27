public class TeleporterModel
{
    public string TargetSceneName { get; set; }

    public CardinalDirection Location { get; set; }

    public bool IsEntrance { get; set; }

    public bool IsExit { get; set; }
}