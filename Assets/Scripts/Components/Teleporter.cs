using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour {
  public Transform TeleporterIndicatorEffect;
  public CardinalDirection Location;
  public bool IsThemedScenesStartTeleporter;

  private PlayerInput input;
  private bool teleportPressed;
  private readonly int CHALLENGE_SCENE_APPEARANCE_COUNT = 4;

  private GameObject instanceOfEffect;

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

  private void OnTeleportCanceled(InputAction.CallbackContext obj) {
    teleportPressed = false;
  }

  private void OnTeleportPerformed(InputAction.CallbackContext obj) {
    teleportPressed = true;
  }
  void OnTriggerExit2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      if (instanceOfEffect != null) {
        Destroy(instanceOfEffect);
      }
    }
  }
  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      if (instanceOfEffect == null && TeleporterIndicatorEffect != null && !IsTeleporterDisabled()) {
        instanceOfEffect = GameObject.Instantiate(TeleporterIndicatorEffect, this.transform.position - new Vector3(0, 0.19f, 0), Quaternion.identity).gameObject;
      }
    }
  }

  void OnTriggerStay2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player" && teleportPressed) {
      var targetSceneName = "";
      var targetLocation = CardinalDirection.West;
      var isEnabled = true;

      if (IsThemedScenesStartTeleporter) {
        ActivateLevel();

        var entranceLevel = GetEntranceLevel();

        targetLocation = entranceLevel.Teleporters.Single(t => t.IsEntrance).Location;
        targetSceneName = entranceLevel.SceneName;
      }
      else {
        var activeLevel = GameUtil.LevelModels.Single(m => m.IsActive);
        string activeSceneName = SceneManager.GetActiveScene().name;
        var currentSceneRelation = GetActiveSceneTeleportersRelationModel(activeLevel, activeSceneName);
        var originTeleporter = GetOriginTeleporterModel(activeLevel, activeSceneName);

        targetSceneName = originTeleporter.TargetSceneName;

        SetTargetForInLevelSceneTeleporter(ref targetSceneName, ref targetLocation, originTeleporter, activeLevel, activeSceneName);

        if (IsTeleporterDisabled(originTeleporter, activeLevel)) {
          isEnabled = false;
        }
      }

      if (isEnabled) {
        TeleportPlayer(targetSceneName, targetLocation);
      }
    }
  }

  private bool IsTeleporterDisabled()
  {
    var activeLevel = GameUtil.LevelModels.SingleOrDefault(m => m.IsActive);

    if (activeLevel == null)
    {
      return false;
    }

    var originTeleporter = GetOriginTeleporterModel(activeLevel, SceneManager.GetActiveScene().name);

    return IsTeleporterDisabled(originTeleporter, activeLevel);
  }

  private bool IsTeleporterDisabled(TeleporterModel teleporter, LevelModel activeLevel)
  {
    return teleporter.IsExit && !activeLevel.IsBossDefeated;
  }

  private void SetTargetForInLevelSceneTeleporter(ref string targetSceneName, ref CardinalDirection targetLocation, TeleporterModel originTeleporter, LevelModel activeLevel, string activeSceneName) {
    if (IsChallengeScene(activeLevel) && (originTeleporter.IsExit || originTeleporter.IsChallengeSceneTeleporter)) {
      if (originTeleporter.IsExit && !originTeleporter.IsChallengeSceneTeleporter) {
        targetSceneName = activeLevel.ChallengeSceneName;
      }
      else if (originTeleporter.IsExit) {
        targetSceneName = originTeleporter.TargetSceneName;
        targetLocation = activeLevel.StartLocation;
      }
      else {
        targetSceneName = originTeleporter.TargetSceneName;
        var exitTelporter = activeLevel.SceneTeleporterRelations.Single(r => r.SceneName == originTeleporter.TargetSceneName).Teleporters.FirstOrDefault(t => t.IsExit);
        targetLocation = exitTelporter.Location;
      }
    }
    else if (originTeleporter.IsEntrance || originTeleporter.IsExit) {
      targetLocation = activeLevel.StartLocation;
    }
    else {
      var targetTeleporter = activeLevel.SceneTeleporterRelations.Single(r => r.SceneName == originTeleporter.TargetSceneName)
          .Teleporters.FirstOrDefault(t => t.TargetSceneName == activeSceneName);

      targetLocation = targetTeleporter.Location;
    }
  }

  private bool IsChallengeScene(LevelModel activeLevel) {
    return activeLevel.LevelSolvedCounter == CHALLENGE_SCENE_APPEARANCE_COUNT;
  }

  private SceneTeleportersRelationModel GetActiveSceneTeleportersRelationModel(LevelModel level, string activeSceneName) {
    return level.SceneTeleporterRelations.Single(r => r.SceneName == activeSceneName);
  }

  private TeleporterModel GetOriginTeleporterModel(LevelModel level, string activeSceneName) {
    var relation = GetActiveSceneTeleportersRelationModel(level, activeSceneName);

    return relation.Teleporters.Single(t => t.Location == Location);
  }

  private SceneTeleportersRelationModel GetEntranceLevel() {
    var level = GameUtil.LevelModels.Single(m => m.IsActive);
    var entranceLevel = level.SceneTeleporterRelations.Single(r => r.Teleporters.Any(t => t.IsEntrance));

    return entranceLevel;
  }

  private void ActivateLevel() {
    var level = GameUtil.LevelModels.Single(m => m.StartLocation == this.Location);

    GameUtil.LevelModels.ForEach(m => m.IsActive = false);
    level.IsActive = true;
  }

  private void TeleportPlayer(string sceneName, CardinalDirection targetTeleporterLocation) {
    GameUtil.TargetTeleporterLocation = targetTeleporterLocation;
    GameUtil.IsPlayerTeleported = true;

    StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, sceneName));
  }
}
