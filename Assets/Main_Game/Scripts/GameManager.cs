using UnityEngine;
using TMPro;
using static Unity.VisualScripting.Member;
using System.Collections;

public struct PlayerAnalyticsData
{
    public string SessionID;
    public string Winner;
    public float TimeActive;
    public int TotalCollectibles;

    public int Score;
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

    public int losePlayerNumber = 0;

    // text mesh pro text field
    // public TextMeshProUGUI player1ScoreTextMeshPro;
    // public TextMeshProUGUI player2ScoreTextMeshPro;

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
    //private bool isPlayer1DataSent = false;
    //private bool isPlayer2DataSent = false;
    
    // audio source
    public AudioSource CountDownAudioSource;

    void Start()
    {
        sessionID = GenerateSessionID();
        Debug.Log("Generated Session ID: " + sessionID);

        player1ScoreManager = playerOne.GetComponent<ScoreManager>();
        player2ScoreManager = playerTwo.GetComponent<ScoreManager>();
       //analyticsCollector = GetComponent<AnalyticsCollector>();
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
            }
            else if(losePlayerNumber == 2)
            {
                UIManager.instance.SetPlayer2PowerUpText("You Lose!");
                UIManager.instance.SetPlayer1PowerUpText("You Win!");
                player1ScoreManager.SetPlayerActive(false);
            }
            isGameOver = true;
            
        }

        // if (isGameOver)
        // {
        //     Debug.Log("---- 1 ----");
        //     PlayerAnalyticsData player1Data;
        //     //PlayerAnalyticsData player2Data;

        //     void SetPlayer1AnalyticsData()
        //     {
        //         player1Data = new PlayerAnalyticsData
        //         {
        //             SessionID = sessionID,
        //             Score = 1000,
        //             TimeActive = 3600.5f,
        //             KilledByBlackHole = 5,
        //             KilledByPlayer = 3,
        //             KilledByObstacle = 2,
        //             GoodCollectiblesCollected = 50,
        //             BadCollectiblesCollected = 10,
        //             TotalCollectibles = 60,
        //             TotalPowerups = 8,
        //             CollectedPowerups = 3
        //         };
        //         analyticsCollector.SendPlayerData(player1Data);
        //     }
        //     if (!isPlayer2DataSent)
        //     { 
        //         SetPlayer1AnalyticsData();
        //         isPlayer2DataSent = true;
        //         Debug.Log("---- 2 ----");
        //     }
        // }

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
            if(isPlayerTwoMoving && isPlayerOneMoving)
            {
                StartCoroutine(BeginTheGame());
                isGameStarted = true;
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

    }

}




