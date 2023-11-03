using UnityEngine;
using TMPro;
using static Unity.VisualScripting.Member;
using System.Collections;

public struct PlayerAnalyticsData
{
    public string SessionID;
    public int Winner;
    public float TimeActive;
    public int TotalCollectibles;

    public int SuccessRate;
    public int KilledByBlackHole;
    public int KilledByPlayer;
    public int BadCollectiblesCollected;
    public int GoodCollectiblesCollected;
    public int FirewallPowerUP;
    public int FreezePowerUP;
    public int HealthPowerUP;
}

public class GameManager : MonoBehaviour
{   
    public GameObject playerOne;
    public GameObject playerTwo;
    public Spawnercode spanwerManager;
    public PowerupSpawner powerSpanwerManager;

    public int losePlayerNumber = 0;
    public int gameWinner = 0;

    // text mesh pro text field
    // public TextMeshProUGUI player1ScoreTextMeshPro;
    // public TextMeshProUGUI player2ScoreTextMeshPro;

    public bool isGameOver = false;
    public bool isGameStarted = false;
    public bool isGameStarting = false;

    public AnalyticsCollector analyticsCollector;

    private ScoreManager player1ScoreManager;
    private ScoreManager player2ScoreManager;
    private static System.Random random = new System.Random();

    private PowerUpManager player1PowerUpManager;
    private PowerUpManager player2PowerUpManager;
    
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
        
        HasPlayersJoined();

        if(losePlayerNumber != 0)
        {
            if(losePlayerNumber == 1)
            {
                UIManager.instance.SetPlayer1PowerUpText("You Lose!");
                UIManager.instance.SetPlayer2PowerUpText("You Win!");
                player2ScoreManager.SetPlayerActive(false);
                gameWinner = 2;
            }
            else if(losePlayerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("You Lose!");
                UIManager.instance.SetPlayer1PowerUpText("You Win!");
                player1ScoreManager.SetPlayerActive(false);
                gameWinner = 1;
            }
            isGameOver = true;
            
        }

        if (isGameOver)
        {
            if (!isPlayer1DataSent)
            {
                PlayerAnalyticsData player1Data;
                int player1successRate = 0;
                if (playerTwo.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() != 0){
                    if ((playerTwo.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() - player2ScoreManager.numOfTimesKilledByPlayer) != 0){
                        player1successRate = player2ScoreManager.numOfTimesKilledByPlayer / (playerTwo.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() - player2ScoreManager.numOfTimesKilledByPlayer);
                    }
                }
                player1Data = new PlayerAnalyticsData
                {
                    SessionID = sessionID,
                    Winner = gameWinner,
                    TimeActive = player1ScoreManager.GetTimeActive(),
                    TotalCollectibles = spanwerManager.numOfCollectiblesSpawned,
                    SuccessRate = player1successRate,
                    KilledByBlackHole = player1ScoreManager.numOfTimesKilledByBlackHole,
                    KilledByPlayer = player1ScoreManager.numOfTimesKilledByPlayer,
                    GoodCollectiblesCollected = player1ScoreManager.numOfGoodCollectiblesCollected,
                    BadCollectiblesCollected = player1ScoreManager.numOfBadCollectiblesCollected,
                    FirewallPowerUP = 12, // test data
                    FreezePowerUP = 4, // test data
                    HealthPowerUP = 3 // test data
                };
                //analyticsCollector.SendPlayerData(player1Data, 1);
                isPlayer1DataSent = true;
            }
            if (!isPlayer2DataSent)
            {
                PlayerAnalyticsData player2Data;
                int player2successRate = 0;
                if (playerOne.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() != 0){
                    if ((playerOne.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() - player1ScoreManager.numOfTimesKilledByPlayer) != 0){
                        player2successRate = player1ScoreManager.numOfTimesKilledByPlayer / (playerOne.GetComponentInChildren<PlayerAnalytics>().GetNumOfKillAttemptsByOpponents() - player1ScoreManager.numOfTimesKilledByPlayer);
                    }
                }
                player2Data = new PlayerAnalyticsData
                {
                    SessionID = sessionID,
                    Winner = gameWinner,
                    TimeActive = player2ScoreManager.GetTimeActive(),
                    TotalCollectibles = spanwerManager.numOfCollectiblesSpawned,
                    SuccessRate = player2successRate,
                    KilledByBlackHole = player2ScoreManager.numOfTimesKilledByBlackHole,
                    KilledByPlayer = player2ScoreManager.numOfTimesKilledByPlayer,
                    GoodCollectiblesCollected = player2ScoreManager.numOfGoodCollectiblesCollected,
                    BadCollectiblesCollected = player2ScoreManager.numOfBadCollectiblesCollected,
                    FirewallPowerUP = 10, // test data
                    FreezePowerUP = 3, // test data
                    HealthPowerUP = 2 // test data
                };
                //analyticsCollector.SendPlayerData(player2Data, 2);
                isPlayer2DataSent = true;
            }
            
        }


    }

    private void HasPlayersJoined()
    {   
        if(isGameStarting || isGameStarted) { return; } // game is in session, no further needs to make these checks
       
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
            if(isPlayerTwoMoving && isPlayerOneMoving)
            {
                isGameStarting = true;
                StartCoroutine(BeginTheGame());
                UIManager.instance.SetPlayer1PowerUpText("Collect Health & Powerups to outlive your opponent");
                UIManager.instance.SetPlayer2PowerUpText("Collect Health & Powerups to outlive your oppoenent");

            }
        }

       
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

    IEnumerator BeginTheGame()
    {   
        CountDownAudioSource.Play();
        //Wait Until Sound has finished playing
        for (int i = (int)CountDownAudioSource.clip.length; i >= 0; i--)
        {
            UIManager.instance.SetPlayer1PanelnText("Game Begins in ..." + i.ToString());
            UIManager.instance.SetPlayer2PanelText("Game Begins in ..." + i.ToString());
            yield return new WaitForSeconds(1.0f);
        }
        playerOne.GetComponent<PlayerInputController>().SetIsMovementAllowed(true);
        playerTwo.GetComponent<PlayerInputController>().SetIsMovementAllowed(true);
        UIManager.instance.SetPlayer1PanelnText("Press L to Turn");
        UIManager.instance.SetPlayer2PanelText("Press A to Turn");
        isGameStarted = true;
        

    }

}




