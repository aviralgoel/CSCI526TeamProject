using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public GameObject playerOne;
    public GameObject playerTwo;

    private ScoreManager player1ScoreManager;
    private ScoreManager player2ScoreManager;
    
    // player 1 variables
    bool isPlayerOneActive = false;

    // player 2 variables
    bool isPlayerTwoActive = false;

    string player1StartTime;
    string player2StartTime;

    public AnalyticsCollector analyticsCollector;

    

    // Start is called before the first frame update
    void Start()
    {
        AnalyticsCollector analyticsCollector = GetComponent<AnalyticsCollector>();
        player1ScoreManager = playerOne.GetComponent<ScoreManager>();
        player2ScoreManager = playerTwo.GetComponent<ScoreManager>();
        analyticsCollector = GetComponent<AnalyticsCollector>();
    }

    void Update()
    {

        if (!isPlayerOneActive) // player has not yet joined the game
        {
            if (Input.GetKeyDown(KeyCode.L)) // player has joined the game
            {
                float temp = Time.time;
                player1StartTime = temp.ToString(); 
                isPlayerOneActive=true;
                analyticsCollector.SendPlayer1("A", player1StartTime);
                player1ScoreManager.SetPlayerActive(true);
                player1ScoreManager.SetPlayerNumber(1);
                playerOne.GetComponent<PlayerInputController>().isMovementAllowed = true; 
            }
        }
        if (!isPlayerTwoActive) // player has not yet joined the game
        {
            if (Input.GetKeyDown(KeyCode.A)) // player has joined the game
            {
                float temp = Time.time;
                player2StartTime = temp.ToString(); 
                isPlayerTwoActive=true;
                analyticsCollector.SendPlayer2("L", player2StartTime);
                player2ScoreManager.SetPlayerActive(true);
                player2ScoreManager.SetPlayerNumber(2);
                playerTwo.GetComponent<PlayerInputController>().isMovementAllowed = true;
            }
        }

        // two playerW game began
        if (isPlayerOneActive && isPlayerTwoActive)
        {   
            // then both player died
            if(player1ScoreManager.IsPlayerActive ()== false && player1ScoreManager.IsPlayerActive() ==false)
            {
                CollectEndGameAnalytics();
            }
        }
    }

    private void CollectEndGameAnalytics()
    {
        int player1Score = (int)player1ScoreManager.GetScore();
        int player2Score = (int)player2ScoreManager.GetScore();
        float player1Time = player1ScoreManager.GetTimeActive();
        float player2Time = player2ScoreManager.GetTimeActive();
        int player1KilledByBlackHole = player1ScoreManager.numOfTimesKilledByBlackHole;
        int player2KilledByBlackHole = player2ScoreManager.numOfTimesKilledByBlackHole;
        int player1KilledByPlayer = player1ScoreManager.numOfTimesKilledByPlayer;
        int player2KilledByPlayer = player2ScoreManager.numOfTimesKilledByPlayer;
        int player1KilledByCollectible = player1ScoreManager.numOfCollectiblesCollected;
        int player2KilledByCollectible = player2ScoreManager.numOfCollectiblesCollected;
    }
}
