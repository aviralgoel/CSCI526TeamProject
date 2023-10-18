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

    private static System.Random random = new System.Random();
    
    // player 1 variables
    bool isPlayerOneActive = false;

    // player 2 variables
    bool isPlayerTwoActive = false;

    string sessionID;
    private bool isPlayer1DataSent = false;
    private bool isPlayer2DataSent = false;

   public AnalyticsCollector analyticsCollector;

    

    // Start is called before the first frame update
    void Start()
    {
        sessionID = GenerateSessionID();
        Debug.Log("Generated Session ID: " + sessionID);
        AnalyticsCollector analyticsCollector = GetComponent<AnalyticsCollector>();
        player1ScoreManager = playerOne.GetComponent<ScoreManager>();
        player2ScoreManager = playerTwo.GetComponent<ScoreManager>();
        analyticsCollector = GetComponent<AnalyticsCollector>();
    }

    void Update()
    {
        //analyticsCollector = GetComponent<AnalyticsCollector>();

        if (!isPlayerOneActive) // player has not yet joined the game
        {
            if (Input.GetKeyDown(KeyCode.L)) // player has joined the game
            {
                isPlayerOneActive=true;
                player1ScoreManager.SetPlayerActive(true);
                player1ScoreManager.SetPlayerNumber(1);
                playerOne.GetComponent<PlayerInputController>().isMovementAllowed = true; 
                
            }
        }
        if (!isPlayerTwoActive) // player has not yet joined the game
        {
            if (Input.GetKeyDown(KeyCode.A)) // player has joined the game
            {
                isPlayerTwoActive=true;
                player2ScoreManager.SetPlayerActive(true);
                player2ScoreManager.SetPlayerNumber(2);
                playerTwo.GetComponent<PlayerInputController>().isMovementAllowed = true;
            }
        }

        // collect player1 data when it is dead
        if (isPlayerOneActive)
        { 
            if(!isPlayer1DataSent && player1ScoreManager.IsPlayerActive ()== false)
            {
                string player1AnalyticsData = collectPlayer1AnalyticsData();
                analyticsCollector.SendPlayer1Data(player1AnalyticsData);
                //Debug.Log("Analytics Data:\n" + player1AnalyticsData);
                isPlayer1DataSent = true;
            }

        }

        // collect player2 data when it is dead
        if (isPlayerTwoActive)
        { 
            if(!isPlayer2DataSent && player2ScoreManager.IsPlayerActive ()== false)
            {
                string player2AnalyticsData = collectPlayer2AnalyticsData();
                analyticsCollector.SendPlayer2Data(player2AnalyticsData);
                //Debug.Log("Analytics Data:\n" + player2AnalyticsData);
                isPlayer2DataSent = true;
            }
        }
    }

    private string collectPlayer1AnalyticsData()
    {
        int player1Score = (int)player1ScoreManager.GetScore();
        float player1Time = player1ScoreManager.GetTimeActive();
        int player1KilledByBlackHole = player1ScoreManager.numOfTimesKilledByBlackHole;
        int player1KilledByPlayer = player1ScoreManager.numOfTimesKilledByPlayer;
        int player1GoodCollectibles = player1ScoreManager.numOfGoodCollectiblesCollected;
        int player1BadCollectibles = player1ScoreManager.numOfBadCollectiblesCollected;

        string player1AnalyticsData = 
            "Player 1 Session: " + sessionID + "\n" +
            "Player 1 Key: L"  + "\n" +
            "Player 1 Score: " + player1Score + "\n" +
            "Player 1 Time: " + player1Time + "\n" +
            "Player 1 Killed by Black Hole: " + player1KilledByBlackHole + "\n" +
            "Player 1 Killed by Player: " + player1KilledByPlayer + "\n" +
            "Player 1 Good Collectibles: " + player1GoodCollectibles + "\n" +
            "Player 1 Bad Collectibles: " + player1BadCollectibles;

        return player1AnalyticsData;
    }

    private string collectPlayer2AnalyticsData()
    {
        int player2Score = (int)player2ScoreManager.GetScore();
        float player2Time = player2ScoreManager.GetTimeActive();
        int player2KilledByBlackHole = player2ScoreManager.numOfTimesKilledByBlackHole;
        int player2KilledByPlayer = player2ScoreManager.numOfTimesKilledByPlayer;
        int player2GoodCollectibles = player2ScoreManager.numOfGoodCollectiblesCollected;
        int player2BadCollectibles = player2ScoreManager.numOfBadCollectiblesCollected;

        string player2AnalyticsData = 
            "Player 2 Session: " + sessionID + "\n" +
            "Player 2 Key: A"  + "\n" +
            "Player 2 Score: " + player2Score + "\n" +
            "Player 2 Time: " + player2Time + "\n" +
            "Player 2 Killed by Black Hole: " + player2KilledByBlackHole + "\n" +
            "Player 2 Killed by Player: " + player2KilledByPlayer + "\n" +
            "Player 2 Good Collectibles: " + player2GoodCollectibles + "\n" +
            "Player 2 Bad Collectibles: " + player2BadCollectibles;

        return player2AnalyticsData;
    }

    private string GenerateSessionID()
    {
        const string allowedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] sessionID = new char[5];

        for (int i = 0; i < 5; i++)
        {
            sessionID[i] = allowedCharacters[random.Next(0, allowedCharacters.Length)];
        }

        return new string(sessionID);
    }
}
