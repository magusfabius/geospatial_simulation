using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target; // The player or object the camera will follow
    public Vector3 offset = new Vector3(0, 2, -5); // Offset from the target's position
    public float zoomSpeed = 2.0f; // Zoom speed
    public float minDistance = 2.0f; // Minimum zoom distance
    public float maxDistance = 10.0f; // Maximum zoom distance
    public float rotationSpeed = 70.0f; // Speed of camera rotation
    public float followSpeed = 5.0f; // Speed at which the camera will follow the player

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    private void Start() {
        // Set the initial offset based on the starting positions of the camera and the target
        offset = transform.position - target.position;
    }

    void Update() {
        // Get mouse input for rotation
        currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Clamp the y rotation to prevent flipping
        currentY = Mathf.Clamp(currentY, -45, 45);

        // Adjust the offset distance based on scroll wheel for zooming
        float desiredDistance = offset.magnitude;
        desiredDistance = Mathf.Clamp(desiredDistance - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, minDistance, maxDistance);
        offset = offset.normalized * desiredDistance;
    }

    void LateUpdate() {
        // Calculate the new position based on the target's position and the offset
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target.position);
    }
}
