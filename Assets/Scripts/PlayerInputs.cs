using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    public InputActions inputActions;
    public InputActions.MovementActions movementActions;
    public PlayerCam playerCam;
    private PlayerMotor playerMotor;

    void Awake() {
        inputActions = new InputActions();
        movementActions = inputActions.Movement;
    }

    void Start() {
        playerMotor = GetComponent<PlayerMotor>();
    }

    void OnEnable() {
        movementActions.Enable();
    }

    void OnDisable() {
        movementActions.Disable();
    }
   
    void FixedUpdate() {
        playerMotor.ProcessMove(movementActions.Move.ReadValue<Vector2>());
    }

    void Update() {
        playerCam.ProcessLook(movementActions.Look.ReadValue<Vector2>());
    }
}