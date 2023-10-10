using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    public float playeerOneMovementSpeed = 10f; // Adjust this to control the force strength.
    public float playeerTwoMovementSpeed = 10f; // Adjust this to control the force strength.
    private Vector3 playerOneDirection;
    private Vector3 playerTwoDirection;
    public GameObject blackhole;
    private Quaternion playerOneTargetRotation;
    private Quaternion playerTwoTargetRotation;
    public float angleToTurn = 10f;

    

    // Start is called before the first frame update
    void Start()
    {
        playerOne.SetActive(false);
        playerTwo.SetActive(false);
        rb1 = playerOne.GetComponent<Rigidbody2D>();
        rb2 = playerTwo.GetComponent<Rigidbody2D>();
        rb1.velocity = Vector3.zero;
        rb2.velocity = Vector3.zero;
    }

    void Update()
    {
        if (playerOne.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                playerOne.SetActive(true);
            }
        }
        if (playerTwo.activeInHierarchy == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                playerTwo.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        // Manage Player Inputs
        if(playerOne.activeInHierarchy)
        {
            PlayerOneInputController();
        }
        if(playerTwo.activeInHierarchy)
        {
            PlayerTwoInputController();
        }
    }


    private void PlayerOneInputController()
    {
        rb1.velocity = playerOne.transform.up * playeerOneMovementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            playerOneDirection = Quaternion.Euler(3, 5, 0.5f) * playerOne.transform.up * Time.deltaTime;
            //playerOneTargetRotation = Quaternion.LookRotation(Vector3.forward, playerOneDirection);
        }
        else
        {
            playerOneDirection = Time.deltaTime * (blackhole.transform.position - playerOne.transform.position);
            playerOneDirection = Quaternion.Euler(0, 0, angleToTurn) * playerOneDirection;

        }
        playerOneTargetRotation = Quaternion.LookRotation(Vector3.forward, playerOneDirection);
        playerOne.transform.rotation = Quaternion.Lerp(playerOne.transform.rotation, playerOneTargetRotation, 5f * Time.deltaTime);
    }
    private void PlayerTwoInputController()
    {
        rb2.velocity = playerTwo.transform.up * playeerTwoMovementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.L))
        {
            playerTwoDirection = Quaternion.Euler(3, 5, 0.5f) * playerTwo.transform.up * Time.deltaTime;
            //playerTwoTargetRotation = Quaternion.LookRotation(Vector3.forward, playerTwoDirection);
        }
        else
        {
            playerTwoDirection = Time.deltaTime * (blackhole.transform.position - playerTwo.transform.position);
            playerTwoDirection = Quaternion.Euler(0, 0, angleToTurn) * playerTwoDirection;
        }
        playerTwoTargetRotation = Quaternion.LookRotation(Vector3.forward, playerTwoDirection);
        playerTwo.transform.rotation = Quaternion.Lerp(playerTwo.transform.rotation, playerTwoTargetRotation, 5f * Time.deltaTime);
    }
}
