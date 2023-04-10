using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    public InputActions inputActions;
    public InputActions.MovementActions movementActions;
    public PlayerCam playerCam;
    public GameObject ingameUI;
    public GameObject menuUI;
    private PlayerMotor playerMotor;

    void Awake() {
        inputActions = new InputActions();
        movementActions = inputActions.Movement;
    }

    void Start() {
        playerMotor = GetComponent<PlayerMotor>();
        movementActions.Escape.performed += ctx => Escape();
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

    public void Escape() {
        ingameUI.SetActive(!ingameUI.activeSelf);
        menuUI.SetActive(!menuUI.activeSelf);
        Time.timeScale = menuUI.activeSelf ? 0 : 1;

        Cursor.visible = menuUI.activeSelf;
        Cursor.lockState = menuUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }
}