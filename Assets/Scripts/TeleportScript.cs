using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public float teleportRange = 10f; // The range within which the object can teleport

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        // Generate a random position within the defined range
        Vector3 randomPosition = new Vector3(
            Random.Range(-teleportRange, teleportRange),
            Random.Range(-teleportRange, teleportRange),
            Random.Range(-teleportRange, teleportRange)
        );

        // Update the object's position
        transform.position += randomPosition;
    }
}
