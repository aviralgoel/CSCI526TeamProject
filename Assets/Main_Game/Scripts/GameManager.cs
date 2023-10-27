using UnityEngine;
using TMPro;
using static Unity.VisualScripting.Member;
using System.Collections;

public class GameManager : MonoBehaviour
{   
    public GameObject playerOne;
    public GameObject playerTwo;
    public Spawnercode spanwerManager;

    public int losePlayerNumber = 0;

    // text mesh pro text field
    public TextMeshProUGUI player1ScoreTextMeshPro;
    public TextMeshProUGUI player2ScoreTextMeshPro;

    public TextMeshProUGUI player1LivesTextMeshPro;
    public TextMeshProUGUI player2LivesTextMeshPro;

    public bool isGameOver = false;
    public bool isGameStarted = false;

    public AnalyticsCollector analyticsCollector;


    private ScoreManager player1ScoreManager;
    private ScoreManager player2ScoreManager;
    private static System.Random random = new System.Random();
    
    // player 1 variables
    [SerializeField] private bool isPlayerOneActive = false;
    [SerializeField] private bool isPlayerOneMoving = false;

    // player 2 variables
    [SerializeField] private bool isPlayerTwoActive = false;
    [SerializeField] private bool isPlayerTwoMoving = false;

    private string sessionID;
    private bool isPlayer1DataSent = false;
    private bool isPlayer2DataSent = false;
    
    // audio source
    public AudioSource CountDownAudioSource;

    void Start()
    {
        sessionID = GenerateSessionID();
        Debug.Log("Generated Session ID: " + sessionID);

        player1ScoreManager = playerOne.GetComponent<ScoreManager>();
        player2ScoreManager = playerTwo.GetComponent<ScoreManager>();
        analyticsCollector = GetComponent<AnalyticsCollector>();
        UIManager.instance.SetPlayer1PanelnText("Press L to Join");
        UIManager.instance.SetPlayer2PanelText("Press A to Join");
    }

    void Update()
    {

        // Update UI
        player1ScoreTextMeshPro.text = player1ScoreManager.GetScore().ToString();
        player2ScoreTextMeshPro.text = player2ScoreManager.GetScore().ToString();

        player1LivesTextMeshPro.text = "Lives:" + player1ScoreManager.numOfLives.ToString();
        player2LivesTextMeshPro.text = "Lives:" + player2ScoreManager.numOfLives.ToString();
        HasPlayersJoined();

        // collect player1 data when it is dead
        if (isPlayerOneActive)
        {
            if (!isPlayer1DataSent && player1ScoreManager.IsPlayerActive() == false)
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
            if (!isPlayer2DataSent && player2ScoreManager.IsPlayerActive() == false)
            {
                string player2AnalyticsData = collectPlayer2AnalyticsData();
                string player1AnalyticsData = collectPlayer1AnalyticsData();
                analyticsCollector.SendPlayer1Data(player1AnalyticsData);
                analyticsCollector.SendPlayer2Data(player2AnalyticsData);
                //Debug.Log("Analytics Data:\n" + player2AnalyticsData);
                isPlayer2DataSent = true;
            }
        }

        if(losePlayerNumber != 0)
        {
            if(losePlayerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText("You Lose!");
                UIManager.instance.SetPlayer2PowerUpText("You Win!");
                player2ScoreManager.SetPlayerActive(false);
            }
            else if(losePlayerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("You Lose!");
                UIManager.instance.SetPlayer1PowerUpText("You Win!");
                player1ScoreManager.SetPlayerActive(false);
            }
            //isGameOver = true;
            
        }
    }

    private void HasPlayersJoined()
    {   
        if(isGameStarted) { return; } // game is in session, no further needs to make these checks
        if (!isPlayerOneActive) // player 1 has not yet joined the game
        {
            if (Input.GetKeyUp(KeyCode.L)) // player has joined the game
            {
                isPlayerOneActive = true;
                player1ScoreManager.SetPlayerNumber(1);
                player1ScoreManager.SetPlayerActive(true);
                UIManager.instance.SetPlayer1PanelnText("waiting for other player to join...");
            }
        }
        if (!isPlayerTwoActive) // player 2 has not yet joined the game
        {
            if (Input.GetKeyUp(KeyCode.A)) // player has joined the game
            {
                isPlayerTwoActive = true;
                player2ScoreManager.SetPlayerNumber(2);
                player2ScoreManager.SetPlayerActive(true);
                UIManager.instance.SetPlayer2PanelText("waiting for other player to join...");
            }
        }
        if (isPlayerOneActive && isPlayerTwoActive) // both players have joined but are yet to move
        {
            UIManager.instance.SetPlayer1PanelnText("Press Space to Begin");
            UIManager.instance.SetPlayer2PanelText("Press Space to Begin");
            if(Input.GetKeyDown(KeyCode.Space)) 
            { 
                isPlayerTwoMoving = true;
                isPlayerOneMoving = true;
            }
            /*if (Input.GetKeyUp(KeyCode.L) && !isPlayerOneMoving)
            {
                isPlayerOneMoving = true;
            }
            if (Input.GetKeyUp(KeyCode.A) && !isPlayerTwoMoving)
            {
                //playerTwo.GetComponent<PlayerInputController>().SetIsMovementAllowed(true);
                isPlayerTwoMoving = true;
            }*/
            if(isPlayerTwoMoving && isPlayerOneMoving)
            {
                StartCoroutine(BeginTheGame());

            }
            if (isPlayerOneMoving && isPlayerTwoMoving)
            {
                isGameStarted = true;
                UIManager.instance.SetPlayer1PowerUpText("Collect Health & Powerups to outlive your opponent");
                UIManager.instance.SetPlayer2PowerUpText("Collect Health & Powerups to outlive your oppoenent");
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

    IEnumerator BeginTheGame()
    {   
        CountDownAudioSource.Play();
        //Wait Until Sound has finished playing
        for (int i = (int)CountDownAudioSource.clip.length-1; i >= 0; i--)
        {
            UIManager.instance.SetPlayer1PanelnText("Game Begins in ..." + i.ToString());
            UIManager.instance.SetPlayer2PanelText("Game Begins in ..." + i.ToString());
            yield return new WaitForSeconds(1.0f);
        }

        playerOne.GetComponent<PlayerInputController>().SetIsMovementAllowed(true);
        playerTwo.GetComponent<PlayerInputController>().SetIsMovementAllowed(true);
        UIManager.instance.SetPlayer1PanelnText("Press L to Turn");
        UIManager.instance.SetPlayer2PanelText("Press A to Turn");

    }

}




