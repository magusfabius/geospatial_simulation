using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 100.0f;
    public Transform cameraTransform;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;

    private void Update()
    {
        // Car movement
        float horizontalInput = Input.GetAxis("Horizontal"); // Left and Right Arrows
        float verticalInput = Input.GetAxis("Vertical");     // Up and Down Arrows

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

        // Camera follow
        if (cameraTransform != null)
        {
            Vector3 cameraPosition = transform.position - (transform.forward * cameraDistance) + (Vector3.up * cameraHeight);
            cameraTransform.position = cameraPosition;
            cameraTransform.LookAt(transform);
        }
    }
}
