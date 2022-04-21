using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour {

  [SerializeField]
  private Rigidbody2D _rigidBody;

  public float moveSpeed = 1.0f;
  public float rotationSpeed = 280.0f;

  private PlayerInput input;
  private Animator animator;

  void Awake() {
    input = new PlayerInput();
    input.Player.Move.started += OnMovement;
    input.Player.Move.performed += OnMovement;
    input.Player.Move.canceled += OnMovement;
  }

  void Start() {
    animator = GetComponentInChildren<Animator>();
  }

  private void OnEnable() {
    input.Enable();
  }

  private void OnDisable() {
    input.Disable();
  }

  private void FixedUpdate() {
    if (isMovePressed) {
      _rigidBody.MovePosition(_rigidBody.position + currentMovement * moveSpeed * Time.deltaTime);
      Vector3 moveDirection = Vector3.right * currentMovement.x + Vector3.up * currentMovement.y;
      transform.rotation = Quaternion.LookRotation(moveDirection.ToIso(), Vector3.up);
    }
  }

  private Vector2 currentMovement;
  bool isMovePressed;
  public void OnMovement(InputAction.CallbackContext context) {
    currentMovement = context.ReadValue<Vector2>();
    isMovePressed = currentMovement.x != 0 || currentMovement.y != 0;

    float currentSpeed = isMovePressed ? moveSpeed : 0.0f;
    animator.SetFloat("Speed", currentSpeed);
  }

}
