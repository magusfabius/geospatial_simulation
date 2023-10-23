using UnityEngine;

public class FlyingCarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float boostMultiplier = 2.0f; // Speed multiplier when boosting
    public float turnSpeed = 100.0f;
    public float tiltAmount = 20.0f;   // Amount to tilt the car and camera when moving horizontally
    public float verticalSpeed = 5.0f; // Speed for moving up and down
    public float smoothness = 0.125f;  // Smoothness for vertical movement animation
    public Transform cameraTransform;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;

    private Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position;
    }

    private void Update()
    {
        // Car (or airplane) movement
        float horizontalInput = Input.GetAxis("Horizontal"); // Left and Right Arrows or A and D keys
        float verticalInput = Input.GetAxis("Vertical");     // Up and Down Arrows or W and S keys

        // Add tilt when moving horizontally
        float tiltAngle = horizontalInput * -tiltAmount; 
        transform.rotation = Quaternion.Euler(new Vector3(tiltAngle, transform.eulerAngles.y, 0));

        float currentSpeed = speed;

        // Boost speed with Z key
        if (Input.GetKey(KeyCode.Z))
        {
            currentSpeed *= boostMultiplier;
        }

        transform.Translate(Vector3.forward * verticalInput * currentSpeed * Time.deltaTime); // Move forward and backward based on vertical input
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime); // Rotate based on horizontal input


        // Vertical movement with Q and E keys
        if (Input.GetKey(KeyCode.Q))
        {
            desiredPosition -= Vector3.up * verticalSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            desiredPosition += Vector3.up * verticalSpeed * Time.deltaTime;
        }

        // Smooth vertical transition for natural flow
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness);

        // Camera follow and tilt
        if (cameraTransform != null)
        {
            Vector3 cameraPosition = transform.position - (transform.forward * cameraDistance) + (Vector3.up * cameraHeight);
            cameraTransform.position = cameraPosition;
            cameraTransform.LookAt(transform);
            cameraTransform.rotation = Quaternion.Euler(new Vector3(tiltAngle, cameraTransform.eulerAngles.y, 0)); // Apply tilt to the camera
        }
    }
}
