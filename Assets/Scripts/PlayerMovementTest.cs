using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float movementSpeed = 10f; // Adjust this to control the force strength.
    private Rigidbody2D rb;
    public GameObject blackhole;
    public float angleToTurn = 10f;
    Vector3 direction;
    Quaternion targetRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
       
        rb.velocity = transform.up * movementSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
        {
            direction = Quaternion.Euler(3, 5, 0.5f) * transform.up * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        else
        {
            direction = Time.deltaTime * (blackhole.transform.position - transform.position);
            direction = Quaternion.Euler(0, 0, angleToTurn) * direction;
            
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }
}
