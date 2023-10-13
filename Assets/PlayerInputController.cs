using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    int playerNumber;
    Rigidbody2D rb;
    Vector3 direction;
    Quaternion targetRotation;
    public bool isMovementAllowed;
    public float movementSpeed;
    public float angleToTurn = 10f;
    UnityEngine.KeyCode controllingKey; 

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player1")
        {
            playerNumber = 1;
        }
        else if (gameObject.tag == "Player2")
        {
            playerNumber = 2;
        }
        rb = GetComponent<Rigidbody2D>();
        controllingKey = playerNumber == 1 ? KeyCode.A : KeyCode.L;
        rb.velocity = Vector3.zero;
        isMovementAllowed = false;  
    }

    // Update is called once per frame

    private void FixedUpdate()
    {   
        if(isMovementAllowed)
        {
            InputController();
        }
    }


    private void InputController()
    {
        rb.velocity = transform.up * movementSpeed * Time.deltaTime;
        if (Input.GetKey(controllingKey))
        {
            direction = Quaternion.Euler(3, 5, angleToTurn) * transform.up * Time.deltaTime;
        }
        else
        {
            direction = Quaternion.Euler(3, 5, -angleToTurn) * transform.up * Time.deltaTime;
        }
        targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }
   
}
