using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DroneController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float altitudeSpeed = 2f;
    public float speedBoostMultiplier = 2f;
    public float rotationSpeed = 2f;
    public float maxAltitude = 50f;
    public float minAltitude = 2f;
    public float gravity = 9.81f;

    private Rigidbody rb;
    private bool applyGravity = false;
    private float currentSpeedBoost = 1f;
    private Vector3 targetVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        // WASD for plane movement
        if (Input.GetKey(KeyCode.W))
            movement += transform.forward;
        if (Input.GetKey(KeyCode.S))
            movement -= transform.forward;
        if (Input.GetKey(KeyCode.A))
            movement -= transform.right;
        if (Input.GetKey(KeyCode.D))
            movement += transform.right;

        // Z and X for altitude control
        if (Input.GetKey(KeyCode.Z) && transform.position.y < maxAltitude)
            movement += transform.up;
        if (Input.GetKey(KeyCode.X) && transform.position.y > minAltitude)
            movement -= transform.up;

        // F to speed up
        if (Input.GetKey(KeyCode.F))
            currentSpeedBoost = speedBoostMultiplier;
        else
            currentSpeedBoost = 1f;

        // Q to apply gravity
        if (Input.GetKey(KeyCode.Q))
        {
            applyGravity = true;
            rb.useGravity = true;
        }
        else if (movement != Vector3.zero) // Any movement key pressed
        {
            applyGravity = false;
            rb.useGravity = false;
        }

        // Calculate target velocity
        targetVelocity = movement.normalized * moveSpeed * currentSpeedBoost;

        // Smoothly transition to the target velocity
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * 5f);

        if (!applyGravity)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Cancel any vertical velocity if not applying gravity
        }

        // Rotate the drone when moving laterally for a more dynamic visual effect
        if (movement.x != 0)
        {
            float rotationAngle = Mathf.Clamp(movement.x * rotationSpeed, -45, 45);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, -rotationAngle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
    }
}
