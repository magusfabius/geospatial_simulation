using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMove : MonoBehaviour {
    public float speed = 5.0f; // Movement speed
    public float flySpeed = 2.0f; // Speed of flying up or down
    public float rotationSpeed = 15.0f; // Speed of rotation
    public float groundCheckDistance = 0.1f; // Distance to check if object is grounded
    public LayerMask groundLayer; // Layer to check against for ground

    private Rigidbody rb;
    private bool isGrounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        CheckGroundStatus();

        // Movement with arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force for movement
        rb.AddForce(movement * speed);

        // Rotate the object in the direction of movement
        if (movement != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Fly up or down based on keys
        if (!isGrounded) {
            if (Input.GetKey(KeyCode.Z)) {
                rb.AddForce(Vector3.down * flySpeed, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.X)) {
                rb.AddForce(Vector3.up * flySpeed, ForceMode.Acceleration);
            }
        }
    }

    private void CheckGroundStatus() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
