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
            var sceneNames = GameUtil.SceneTeleporterRelations.Select(r => r.SceneName).ToArray();
            string activeSceneName = SceneManager.GetActiveScene().name;

            SceneTeleportersRelationModel relation = GameUtil.SceneTeleporterRelations
                .Single(r => r.SceneName == activeSceneName);
            string sceneName = relation.Teleporters.Single(t => t.Location == Location).TargetSceneName;

            GameUtil.TargetTeleporterLocation = GameUtil.SceneTeleporterRelations.Single(r => r.SceneName == sceneName)
                .Teleporters.FirstOrDefault(t => t.TargetSceneName == activeSceneName).Location;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
