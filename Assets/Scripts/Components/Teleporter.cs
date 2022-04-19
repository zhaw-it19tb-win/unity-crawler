using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{    
    public string teleporterId;
    public string targetScene;
    public string targetTeleporterId;

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
            GameUtil.targetTeleporterId = targetTeleporterId;
            LoadNewScene(targetScene);
        }
    }

    private void LoadNewScene(string scenePath)
    {
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
    }
}
