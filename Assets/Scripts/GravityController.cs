using UnityEngine;

public class GravityControl : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityStrength = -9.81f;
    private bool gravityInverted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody attached to the object.");
            return;
        }

        // Initially, disable Unity's default gravity since we'll be applying our own.
        rb.useGravity = false;
    }

    private void Update()
    {
        // Switch gravity direction when the user presses the space key.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gravityInverted = !gravityInverted;
        }

        Vector3 customGravity = gravityInverted ? Vector3.up * gravityStrength : Vector3.down * gravityStrength;
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }
}
