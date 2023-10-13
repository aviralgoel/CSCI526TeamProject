using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    
    // player 1 variables
    bool isPlayerOneActive = false;

    // player 2 variables
    bool isPlayerTwoActive = false;

    string player1StartTime;
    string player1EndTime;
    string player2StartTime;
    string player2EndTime;
    AnalyticsCollector analyticsCollector;

    

    // Start is called before the first frame update
    void Start()
    {
        AnalyticsCollector analyticsCollector = GetComponent<AnalyticsCollector>();
    }

    void Update()
    {
        if(!analyticsCollector)
        {
            analyticsCollector = GetComponent<AnalyticsCollector>();
        }
        if (!isPlayerOneActive)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                float temp = Time.time;
                player1StartTime = temp.ToString(); 
                isPlayerOneActive=true;
                analyticsCollector.SendPlayer1("A", player1StartTime);
                playerOne.GetComponent<ScoreManager>().SetPlayerActive(true);
                playerOne.GetComponent<ScoreManager>().SetPlayerNumber(1);
                playerOne.GetComponent<PlayerInputController>().isMovementAllowed = true; 
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
                playerTwo.GetComponent<ScoreManager>().SetPlayerActive(true);
                playerTwo.GetComponent<ScoreManager>().SetPlayerNumber(2);
                playerTwo.GetComponent<PlayerInputController>().isMovementAllowed = true;
            }
        }
    }

}
