using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MapStructureGenerator
{
    public static void GenerateMapStructure(List<SceneTeleportersRelationModel> sceneTeleportersRelations)
    {
        var sceneNames = sceneTeleportersRelations.Select(r => r.SceneName).ToArray();

        while (sceneTeleportersRelations.Count(r => r.Teleporters.Any(t => string.IsNullOrEmpty(t.TargetSceneName))) >= 2)
        {
            var originScene = sceneTeleportersRelations.FirstOrDefault(r => r.Teleporters.Any(t => string.IsNullOrEmpty(t.TargetSceneName)));

            int targetSceneIndex;
            do
            {
                targetSceneIndex = Random.Range(1, sceneNames.Count());
            } while (
                sceneNames[targetSceneIndex] == originScene.SceneName ||
                !sceneTeleportersRelations.Any(r => r.SceneName == sceneNames[targetSceneIndex] && r.Teleporters.Any(t => string.IsNullOrEmpty(t.TargetSceneName)))
            );

            originScene.Teleporters.FirstOrDefault(t => string.IsNullOrEmpty(t.TargetSceneName)).TargetSceneName = sceneNames[targetSceneIndex];
            sceneTeleportersRelations.FirstOrDefault(r => r.SceneName == sceneNames[targetSceneIndex])
                .Teleporters.FirstOrDefault(t => string.IsNullOrEmpty(t.TargetSceneName)).TargetSceneName = originScene.SceneName;
        }
    }
}
