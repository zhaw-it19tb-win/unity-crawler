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

                GameUtil.TargetTeleporterLocation = entranceLevel.Teleporters.Single(t => t.IsEntrance).Location;

                SceneManager.LoadScene(entranceLevel.SceneName, LoadSceneMode.Single);
            }
            else
            {
                var levelModel = GameUtil.LevelModels.Single(m => m.IsActive);
                var sceneNames = levelModel.SceneTeleporterRelations.Select(r => r.SceneName).ToArray();
                string activeSceneName = SceneManager.GetActiveScene().name;

                SceneTeleportersRelationModel relation = levelModel.SceneTeleporterRelations
                    .Single(r => r.SceneName == activeSceneName);
                var teleporter = relation.Teleporters.Single(t => t.Location == Location);

                if (teleporter.IsEntrance || teleporter.IsExit)
                {
                    GameUtil.TargetTeleporterLocation = levelModel.StartLocation;
                } 
                else
                {
                    GameUtil.TargetTeleporterLocation = levelModel.SceneTeleporterRelations.Single(r => r.SceneName == teleporter.TargetSceneName)
                        .Teleporters.FirstOrDefault(t => t.TargetSceneName == activeSceneName).Location;
                }

                SceneManager.LoadScene(teleporter.TargetSceneName, LoadSceneMode.Single);
            }
        }
    }
}
