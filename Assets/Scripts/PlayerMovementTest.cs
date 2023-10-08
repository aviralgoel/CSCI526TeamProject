using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust this to control the force strength.
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the force vector based on input.
        Vector2 moveForce = new Vector2(horizontalInput, verticalInput) * movementSpeed;

        // Apply the force to the Rigidbody2D.
        rb.AddForce(moveForce);
    }
}
