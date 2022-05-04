using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{
    public CardinalDirection Location;
    public bool IsThemedScenesStartTeleporter;

    private PlayerInput input;
    private bool teleportPressed;

    void OnEnable() {
        input.Enable();
    }

    void OnDisable() {
        input.Disable();
    } 

    void Awake() {
        input = new PlayerInput();
        input.Player.Teleport.performed += OnTeleportPerformed;
        input.Player.Teleport.canceled += OnTeleportCanceled;
    }

    private void OnTeleportCanceled(InputAction.CallbackContext obj)
    {
        teleportPressed = false;
    }

    private void OnTeleportPerformed(InputAction.CallbackContext obj)
    {
        teleportPressed = true;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && teleportPressed)
        {
            if (IsThemedScenesStartTeleporter)
            {
                ActivateLevel();
                var activeLevel = GameUtil.LevelModels.Single(m => m.IsActive);

                if (IsChallengeScene(activeLevel))
                {
                    TeleportPlayer(activeLevel.ChallengeSceneName, CardinalDirection.West);
                }
                else
                {
                    var entranceLevel = GetEntranceLevel();
                    var entranceTeleporter = entranceLevel.Teleporters.Single(t => t.IsEntrance);

                    TeleportPlayer(entranceLevel.SceneName, entranceTeleporter.Location);
                }
            }
            else
            {
                var activeLevel = GameUtil.LevelModels.Single(m => m.IsActive);

                if (IsChallengeScene(activeLevel))
                {
                    TeleportPlayer("MainScene", activeLevel.StartLocation);
                }

                string activeSceneName = SceneManager.GetActiveScene().name;                
                var originTeleporter = GetOriginTeleporterModel(activeLevel, activeSceneName);

                if (!originTeleporter.IsEntrance && !originTeleporter.IsExit)
                {
                    var targetTeleporter = activeLevel.SceneTeleporterRelations.Single(r => r.SceneName == originTeleporter.TargetSceneName)
                        .Teleporters.FirstOrDefault(t => t.TargetSceneName == activeSceneName);

                    TeleportPlayer(originTeleporter.TargetSceneName, targetTeleporter.Location);
                } 
                else if (originTeleporter.IsEntrance || (originTeleporter.IsExit && activeLevel.IsBossDefeated))
                {
                    TeleportPlayer(originTeleporter.TargetSceneName, activeLevel.StartLocation);
                }
            }
        }
    }

    private bool IsChallengeScene(LevelModel activeLevel)
    {
        return activeLevel.LevelSolvedCounter != 0 && activeLevel.LevelSolvedCounter % 5 == 0;
    }

    private TeleporterModel GetOriginTeleporterModel(LevelModel level, string activeSceneName)
    {
        SceneTeleportersRelationModel relation = level.SceneTeleporterRelations.Single(r => r.SceneName == activeSceneName);

        return relation.Teleporters.Single(t => t.Location == Location);
    }

    private SceneTeleportersRelationModel GetEntranceLevel()
    {
        var level = GameUtil.LevelModels.Single(m => m.IsActive);
        var entranceLevel = level.SceneTeleporterRelations.Single(r => r.Teleporters.Any(t => t.IsEntrance));

        return entranceLevel;
    }

    private void ActivateLevel()
    {
        var level = GameUtil.LevelModels.Single(m => m.StartLocation == this.Location);

        GameUtil.LevelModels.ForEach(m => m.IsActive = false);
        level.IsActive = true;
    }

    private void TeleportPlayer(string sceneName, CardinalDirection targetTeleporterLocation)
    {
        GameUtil.TargetTeleporterLocation = targetTeleporterLocation;
        GameUtil.IsPlayerTeleported = true;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
