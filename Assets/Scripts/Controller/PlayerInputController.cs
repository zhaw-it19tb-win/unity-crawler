using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WitchShooting))]
[RequireComponent(typeof(RoundAttack))]
public class PlayerInputController : MonoBehaviour {

  [SerializeField]
  private Rigidbody2D _rigidBody;

  public float moveSpeed = 1.0f;
  public float rotationSpeed = 280.0f;

  private PlayerInput input;
  private WitchShooting witchShooting;
  private RoundAttack roundAttack;


    void Awake() {
    input = new PlayerInput();
    input.Player.Move.started += OnMovement;
    input.Player.Move.performed += OnMovement;
    input.Player.Move.canceled += OnMovement;
  }

  void Start() {
        witchShooting = GetComponent<WitchShooting>();
        roundAttack = GetComponent<RoundAttack>();
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
    }
    FindObjectOfType<PlayerAnimation>().SetDirection(currentMovement);

  }

  private Vector2 currentMovement;
  bool isMovePressed;
  public void OnMovement(InputAction.CallbackContext context) {
    currentMovement = context.ReadValue<Vector2>();
    isMovePressed = currentMovement.x != 0 || currentMovement.y != 0;

    float currentSpeed = isMovePressed ? moveSpeed : 0.0f;
  }

}
