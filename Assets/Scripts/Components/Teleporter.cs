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
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneTeleportersRelationModel relation = GameUtil.SceneTeleporterRelations
                .Single(r => r.SceneIndex == activeSceneIndex);
            int sceneIndex = relation.Teleporters.Single(t => t.Location == Location).TargetSceneIndex.Value;

            GameUtil.TargetTeleporterLocation = GameUtil.SceneTeleporterRelations.Single(r => r.SceneIndex == sceneIndex)
                .Teleporters.FirstOrDefault(t => t.TargetSceneIndex == activeSceneIndex).Location;
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
