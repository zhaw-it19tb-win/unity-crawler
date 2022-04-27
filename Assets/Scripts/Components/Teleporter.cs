using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{
    public string targetScene;
    public string teleporterId;
    public string targetTeleporterId;
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
                var levelModel = GameUtil.LevelModels.Single(m => m.StartLocation == this.Location);

                GameUtil.LevelModels.ForEach(m => m.IsActive = false);
                levelModel.IsActive = true;

                var entranceLevel = levelModel.SceneTeleporterRelations.Single(r => r.Teleporters.Any(t => t.IsEntrance));
                var entranceTeleporter = entranceLevel.Teleporters.Single(t => t.IsEntrance);

                TeleportPlayer(entranceLevel.SceneName, entranceTeleporter.Location);
            }
            else
            {
                var levelModel = GameUtil.LevelModels.Single(m => m.IsActive);
                var sceneNames = levelModel.SceneTeleporterRelations.Select(r => r.SceneName).ToArray();
                string activeSceneName = SceneManager.GetActiveScene().name;

                SceneTeleportersRelationModel relation = levelModel.SceneTeleporterRelations
                    .Single(r => r.SceneName == activeSceneName);
                var teleporter = relation.Teleporters.Single(t => t.Location == Location);

                if (!teleporter.IsEntrance && !teleporter.IsExit)
                {
                    var targetTeleporter = levelModel.SceneTeleporterRelations.Single(r => r.SceneName == teleporter.TargetSceneName)
                        .Teleporters.FirstOrDefault(t => t.TargetSceneName == activeSceneName);

                    TeleportPlayer(teleporter.TargetSceneName, targetTeleporter.Location);
                } 
                else if (teleporter.IsEntrance || (teleporter.IsExit && levelModel.IsBossDefeated))
                {
                    TeleportPlayer(teleporter.TargetSceneName, levelModel.StartLocation);
                }
            }
        }
    }

    private void TeleportPlayer(string sceneName, CardinalDirection targetTeleporterLocation)
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Stop("theme");
        FindObjectOfType<AudioManager>().Play("boss");
        GameUtil.TargetTeleporterLocation = targetTeleporterLocation;
        GameUtil.IsPlayerTeleported = true;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
