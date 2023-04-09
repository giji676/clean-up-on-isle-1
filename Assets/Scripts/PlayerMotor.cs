using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float canDrag; // More cans you pick up, more drag you get, slower you get
    [SerializeField] private Transform orientation;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;
    }

    private void Update() {
        SpeedControl();
    }

    public void UpdateDrag(int cans) {
        rb.drag = groundDrag + (cans * canDrag);
    }

    public void ProcessMove(Vector2 input) {
        moveDirection = orientation.right * input.y;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    public void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Slip() {
        // rb.AddForce(orientation.right * 40f, ForceMode.Impulse);
    }
}