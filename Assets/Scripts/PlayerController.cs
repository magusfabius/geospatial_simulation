using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public string playerName;
    public string playerSurname;
    public int age;
    public List<string> inventory = new List<string>(); // Dynamic inventory
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Space)) HandleJump();
        if (Input.GetKeyDown(KeyCode.T)) HandleTimeSwitch();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(moveDirection * Time.deltaTime * 5f);  // Multiplied by 5 for speed factor
    }

    private void HandleJump()
    {
        if (IsGrounded())  // Check if the player is on the ground
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);  // Jump force
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded to allow jumping
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void HandleTimeSwitch()
    {
        // Switch scene logic - this will be delegated to the TimeSwitcher script
        GetComponent<TimeSwitcher>().SwitchTime();
    }
}
