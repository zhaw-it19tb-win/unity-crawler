using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RoundAttack))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Mana))]
public class PlayerInputController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rigidBody;

    public float moveSpeed = 1.0f;
    public float rotationSpeed = 280.0f;

    private PlayerInput input;
    private RoundAttack roundAttack;
    private Health health;


    void Awake()
    {
        input = new PlayerInput();
        input.Player.Move.started += OnMovement;
        input.Player.Move.performed += OnMovement;
        input.Player.Move.canceled += OnMovement;

        input.Player.Sprint.started += OnSprint;
        input.Player.Sprint.canceled += OffSprint;
    }

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDied += OnDied;

        roundAttack = GetComponent<RoundAttack>();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDied()
    {
        //Doesn't work because Main Camera is attached to this object.
        //Destroy(this.gameObject);
        Debug.Log("Player died");
    }

    private void FixedUpdate()
    {
        if (isMovePressed)
        {
            _rigidBody.MovePosition(_rigidBody.position + currentMovement * moveSpeed * Time.deltaTime);
        }
        FindObjectOfType<PlayerAnimation>().SetDirection(currentMovement);

    }

    private Vector2 currentMovement;
    bool isMovePressed;
    public void OnMovement(InputAction.CallbackContext context)
    {
        currentMovement = context.ReadValue<Vector2>();
        isMovePressed = currentMovement.x != 0 || currentMovement.y != 0;

        float currentSpeed = isMovePressed ? moveSpeed : 0.0f;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        moveSpeed *= 2;
    }

    public void OffSprint(InputAction.CallbackContext context)
    {
        moveSpeed /= 2;
    }
}
