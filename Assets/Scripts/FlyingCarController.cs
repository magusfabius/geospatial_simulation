using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlyingCarController : MonoBehaviour
{
    public float speed = 10.0f;
    public float boostMultiplier = 2.0f; 
    public float turnSpeed = 100.0f;
    public float tiltAmount = 20.0f;
    public float rollAmount = 30.0f;
    public float verticalSpeed = 5.0f;
    public float smoothness = 0.125f; 
    public Transform cameraTransform;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;
    public ParticleSystem trailParticles;
    public AudioClip engineSound;

    private Vector3 desiredPosition;
    private float currentSpeed;
    private AudioSource audioSource;
    private float originalFOV;

    private void Start()
    {
        desiredPosition = transform.position;
        currentSpeed = speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true;
        audioSource.Play();

        if (cameraTransform != null)
            originalFOV = cameraTransform.GetComponent<Camera>().fieldOfView;
    }

    private void Update()
    {
        HandleMovement();
        HandleVerticalMovement();
        HandleCameraFollow();
        HandleAudio();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Mathf.Sign(Input.GetAxis("Vertical"));

        float tiltAngle = horizontalInput * -tiltAmount;
        float rollAngle = horizontalInput * -rollAmount;
        transform.rotation = Quaternion.Euler(new Vector3(tiltAngle, transform.eulerAngles.y, rollAngle));

        if (Input.GetKey(KeyCode.Z))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed * boostMultiplier, Time.deltaTime * 2);
            if (trailParticles != null)
                trailParticles.emissionRate = Mathf.Lerp(trailParticles.emissionRate, 200, Time.deltaTime * 2);
            if (cameraTransform != null)
                cameraTransform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cameraTransform.GetComponent<Camera>().fieldOfView, originalFOV + 10, Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime * 2);
            if (trailParticles != null)
                trailParticles.emissionRate = Mathf.Lerp(trailParticles.emissionRate, 50, Time.deltaTime * 2);
            if (cameraTransform != null)
                cameraTransform.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cameraTransform.GetComponent<Camera>().fieldOfView, originalFOV, Time.deltaTime);
        }

        transform.Translate(Vector3.forward * verticalInput * currentSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
    }

    private void HandleVerticalMovement()
    {
        if (Input.GetKey(KeyCode.Q))
            desiredPosition -= Vector3.up * verticalSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            desiredPosition += Vector3.up * verticalSpeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothness);
    }

    private void HandleCameraFollow()
    {
        if (cameraTransform != null)
        {
            Vector3 cameraPosition = transform.position - (transform.forward * cameraDistance) + (Vector3.up * cameraHeight);
            cameraTransform.position = cameraPosition;
            cameraTransform.LookAt(transform);
            cameraTransform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, cameraTransform.eulerAngles.y, 0));
        }
    }

    private void HandleAudio()
    {
        if (audioSource != null)
            audioSource.pitch = Mathf.Lerp(0.8f, 1.2f, (currentSpeed - speed) / (speed * boostMultiplier - speed));
    }
}
