using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{   
    public GameObject playerOne;
    public GameObject playerTwo;

    //public Text playerOneScoreText; //sharan
    //public Text playerTwoScoreText; // sharan

    private ScoreManager player1ScoreManager;
    private ScoreManager player2ScoreManager;
    public Spawnercode spanwerManager;

    // text mesh pro text field
   public TextMeshProUGUI player1ScoreTextMeshPro;
    public TextMeshProUGUI player2ScoreTextMeshPro;

    public TextMeshProUGUI player1LivesTextMeshPro;
    public TextMeshProUGUI player2LivesTextMeshPro;

    public bool isGameOver = false;




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

        //int totalCollectiblesSpawned = spanwerManager.numOfCollectiblesSpawned;


        // Sharan
        //playerOneScoreText = GameObject.Find("PlayerOneScoreText").GetComponent<Text>();
        //playerTwoScoreText = GameObject.Find("PlayerTwoScoreText").GetComponent<Text>();
    }

    void Update()
    {
        //analyticsCollector = GetComponent<AnalyticsCollector>();

        //sharan
        //if (playerOneScoreText != null && player1ScoreManager != null)
        
            //playerOneScoreText.text = "Player 1 Score: " + player1ScoreManager.GetScore().ToString();

            //sharan
            player1ScoreTextMeshPro.text = player1ScoreManager.GetScore().ToString();
            player2ScoreTextMeshPro.text = player2ScoreManager.GetScore().ToString();

            player1LivesTextMeshPro.text = "Lives:" + player1ScoreManager.numOfLives.ToString();
            player2LivesTextMeshPro.text = "Lives:" + player2ScoreManager.numOfLives.ToString();
            //sharan

        //if (playerTwoScoreText != null && player2ScoreManager != null)
        //{
          //  playerTwoScoreText.text = "Player 2 Score: " + player2ScoreManager.GetScore().ToString();
        //}

        // sharan
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
                string player2AnalyticsData = collectPlayer2AnalyticsData();
                analyticsCollector.SendPlayer1Data(player1AnalyticsData);
                analyticsCollector.SendPlayer2Data(player2AnalyticsData);
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
                string player1AnalyticsData = collectPlayer1AnalyticsData();
                analyticsCollector.SendPlayer1Data(player1AnalyticsData);
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
        int totalCollectibles = spanwerManager.numOfCollectiblesSpawned;

        string player1AnalyticsData = 
            "Player 1 Session: " + sessionID + "\n" +
            "Player 1 Key: L"  + "\n" +
            "Player 1 Score: " + player1Score + "\n" +
            "Player 1 Time: " + player1Time + "\n" +
            "Player 1 Killed by Black Hole: " + player1KilledByBlackHole + "\n" +
            "Player 1 Killed by Player: " + player1KilledByPlayer + "\n" +
            "Player 1 Good Collectibles: " + player1GoodCollectibles + "\n" +
            "Player 1 Bad Collectibles: " + player1BadCollectibles + "\n" +
            "Total Collectibles: " + totalCollectibles;
        
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
        int totalCollectibles = spanwerManager.numOfCollectiblesSpawned;

        string player2AnalyticsData = 
            "Player 2 Session: " + sessionID + "\n" +
            "Player 2 Key: A"  + "\n" +
            "Player 2 Score: " + player2Score + "\n" +
            "Player 2 Time: " + player2Time + "\n" +
            "Player 2 Killed by Black Hole: " + player2KilledByBlackHole + "\n" +
            "Player 2 Killed by Player: " + player2KilledByPlayer + "\n" +
            "Player 2 Good Collectibles: " + player2GoodCollectibles + "\n" +
            "Player 2 Bad Collectibles: " + player2BadCollectibles + "\n" +
            "Total Collectibles: " + totalCollectibles;
        
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
    //sharan
     public void UpdatePlayerScoreUI(ScoreManager scoreManager)
    {
        if (scoreManager == player1ScoreManager)
        {
            player1ScoreTextMeshPro.text = "Player 1 Score: " + scoreManager.GetScore().ToString();
        }
        else if (scoreManager == player2ScoreManager)
        {
            player2ScoreTextMeshPro.text = "Player 2 Score: " + scoreManager.GetScore().ToString();
        }
    }

   
    public void UpdatePlayerLivesUI(ScoreManager scoreManager)
    {
        if (scoreManager == player1ScoreManager)
        {
            player1LivesTextMeshPro.text = "Lives: " + scoreManager.numOfLives.ToString();
        }
        else if (scoreManager == player2ScoreManager)
        {
            player2LivesTextMeshPro.text = "Lives: " + scoreManager.numOfLives.ToString();
        }
    }

  
}

