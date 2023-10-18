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
        // game began
        if (isPlayerOneActive && isPlayerTwoActive)
        {   
            // then both player died
            if(playerOne.GetComponent<ScoreManager>().isPlayerActive== false && playerTwo.GetComponent<ScoreManager>().isPlayerActive==false)
            {
                CollectEndGameAnalytics();
            }
        }
    }

    private void CollectEndGameAnalytics()
    {
        int player1Score = (int)playerOne.GetComponent<ScoreManager>().GetScore();
        int player2Score = (int)playerTwo.GetComponent<ScoreManager>().GetScore();
        float player1Time = playerOne.GetComponent<ScoreManager>().GetTimeActive();
        float player2Time = playerTwo.GetComponent<ScoreManager>().GetTimeActive();
        int player1KilledByBlackHole = playerOne.GetComponent<ScoreManager>().numOfTimesKilledByBlackHole;
        int player2KilledByBlackHole = playerTwo.GetComponent<ScoreManager>().numOfTimesKilledByBlackHole;
        int player1KilledByPlayer = playerOne.GetComponent<ScoreManager>().numOfTimesKilledByPlayer;
        int player2KilledByPlayer = playerTwo.GetComponent<ScoreManager>().numOfTimesKilledByPlayer;
        int player1KilledByCollectible = playerOne.GetComponent<ScoreManager>().numOfTimesKilledByCollectible;
        int player2KilledByCollectible = playerTwo.GetComponent<ScoreManager>().numOfTimesKilledByCollectible;
    }
}
