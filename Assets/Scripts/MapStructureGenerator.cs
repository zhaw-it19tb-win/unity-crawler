using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MapStructureGenerator
{
    public static void GenerateMapStructure(List<SceneTeleportersRelationModel> sceneTeleportersRelations)
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;

        while (sceneTeleportersRelations.Count(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue)) >= 2)
        {
            var originScene = sceneTeleportersRelations.FirstOrDefault(r => r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue));

            int targetSceneIndex;
            do
            {
                targetSceneIndex = Random.Range(1, sceneCount);
            } while (
                targetSceneIndex == originScene.SceneIndex ||
                !sceneTeleportersRelations.Any(r => r.SceneIndex == targetSceneIndex && r.Teleporters.Any(t => !t.TargetSceneIndex.HasValue))
            );

            originScene.Teleporters.FirstOrDefault(t => !t.TargetSceneIndex.HasValue).TargetSceneIndex = targetSceneIndex;
            sceneTeleportersRelations.FirstOrDefault(r => r.SceneIndex == targetSceneIndex)
                .Teleporters.FirstOrDefault(t => !t.TargetSceneIndex.HasValue).TargetSceneIndex = originScene.SceneIndex;
        }
    }
}
