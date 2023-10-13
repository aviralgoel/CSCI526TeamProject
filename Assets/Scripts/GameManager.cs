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

    // player 1 variables
    bool isPlayerOneActive = false;

    // player 2 variables
    bool isPlayerTwoActive = false;

    public string player1StartTime;
    public string player1EndTime;
    public string player2StartTime;
    public string player2EndTime;

    

    // Start is called before the first frame update
    void Start()
    {
        rb1 = playerOne.GetComponent<Rigidbody2D>();
        rb2 = playerTwo.GetComponent<Rigidbody2D>();
        rb1.velocity = Vector3.zero;
        rb2.velocity = Vector3.zero;
    }

    void Update()
    {
        AnalyticsCollector analyticsCollector = GetComponent<AnalyticsCollector>();
        if (!isPlayerOneActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                float temp = Time.time;
                player1StartTime = temp.ToString(); 
                isPlayerOneActive=true;
                analyticsCollector.SendPlayer1("A", player1StartTime);
            }
        }
        if (!isPlayerTwoActive)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                float temp = Time.time;
                player2StartTime = temp.ToString(); 
                isPlayerTwoActive=true;
                analyticsCollector.SendPlayer2("L", player2StartTime);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        // Manage Player Inputs
        if(isPlayerOneActive)
        {
            PlayerOneInputController();
        }
        if(isPlayerTwoActive)
        {
            PlayerTwoInputController();
        }
    }


    private void PlayerOneInputController()
    {
        rb1.velocity = playerOne.transform.up * playeerOneMovementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            playerOneDirection = Quaternion.Euler(3, 5, angleToTurn) * playerOne.transform.up * Time.deltaTime;
            //playerOneTargetRotation = Quaternion.LookRotation(Vector3.forward, playerOneDirection);
        }
        else
        {
            playerOneDirection = Quaternion.Euler(3, 5, -angleToTurn) * playerOne.transform.up * Time.deltaTime;


        }
        playerOneTargetRotation = Quaternion.LookRotation(Vector3.forward, playerOneDirection);
        playerOne.transform.rotation = Quaternion.Lerp(playerOne.transform.rotation, playerOneTargetRotation, 5f * Time.deltaTime);
    }
    private void PlayerTwoInputController()
    {
        rb2.velocity = playerTwo.transform.up * playeerTwoMovementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.L))
        {
            playerTwoDirection = Quaternion.Euler(3, 5, angleToTurn) * playerTwo.transform.up * Time.deltaTime;
        }
        else
        {
            playerTwoDirection = Quaternion.Euler(3, 5, -angleToTurn) * playerTwo.transform.up * Time.deltaTime;
        }
        playerTwoTargetRotation = Quaternion.LookRotation(Vector3.forward, playerTwoDirection);
        playerTwo.transform.rotation = Quaternion.Lerp(playerTwo.transform.rotation, playerTwoTargetRotation, 5f * Time.deltaTime);
    }
}
