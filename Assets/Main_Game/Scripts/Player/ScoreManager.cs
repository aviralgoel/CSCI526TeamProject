using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{   
    [SerializeField] private float score = 100;
    [SerializeField] private float timeActive = 0;
    [SerializeField] private int playerNumber;
    [SerializeField] private bool isPlayerActive;
    [SerializeField] public int numOfLives = 3;
    [SerializeField] public int numOfTimesKilledByBlackHole = 0;
    [SerializeField] public int numOfTimesKilledByPlayer = 0;
    [SerializeField] public int numOfCollectiblesCollected = 0;
    [SerializeField] public int numOfGoodCollectiblesCollected = 0;
    [SerializeField] public int numOfBadCollectiblesCollected = 0;
    public List<GameObject> walls = new List<GameObject>();
    public PowerUpManager PowerUpManagerPlayer1;
    public PowerUpManager PowerUpManagerPlayer2;

    public Image HealthBar;
    public float TotalHealth;
    //public GameObject bar;

    //sharan
    [SerializeField]
    private GameManager gameManager;

  
   //sharan


    
    Vector3 respawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerActive = false;
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerActive)
        {
            UpdateTime();
        }

        if(gameManager.isGameStarted) {
            score -= Time.deltaTime;
            HealthBar.fillAmount = score / TotalHealth;
            if(score <= 0)
            {
                score = 0;
                GameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((PowerUpManagerPlayer1.fireWallActive || PowerUpManagerPlayer2.fireWallActive) && walls.Contains(collision.gameObject)) {
            score--;
        }
    }

    private void UpdateTime()
    {
        timeActive += Time.deltaTime; 
    }
    
    public void SetPlayerActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
        // print the player number
        Debug.Log("Player " + playerNumber + " has joined the game");
        isPlayerActive = isActive;
        
    }
    public bool IsPlayerActive()
    {
        return isPlayerActive;
    }
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public int GetScore()
    {
        return (int)score;
    }
    public float GetTimeActive() 
    {
        return timeActive;
    }
    public void IncrementScore(int amount)
    {
        // Increment the score when a good collectible is collected
        score += amount;
        gameManager.UpdatePlayerScoreUI(this);
        HealthBar.fillAmount = score / TotalHealth;
    }
    public void RespawnPlayer(string tagOfKiller)
    {   
        // analytics collector
        if(tagOfKiller == "Blackhole")
        {
            numOfTimesKilledByBlackHole++;
            ReducePlayerLife();
        }
        else if(tagOfKiller == "OtherPlayer")
        {
            numOfTimesKilledByPlayer++;
            ReducePlayerLife();
        }
        else if(tagOfKiller == "Good" || tagOfKiller=="Bad")
        {
            numOfCollectiblesCollected++;
            if(tagOfKiller == "Good")
            {
                numOfGoodCollectiblesCollected++;
            }
            else
            {
                numOfBadCollectiblesCollected++;
            }
            return;
        }
        transform.position = respawnPosition;

    }
    public void GameOver() 
    {   
        // sstop the player
        isPlayerActive = false;
        this.gameObject.SetActive(false);
        gameManager.isGameOver = true; 
        //change scene to game over
        SceneManager.LoadScene("End_Scene");

    }

    //sharan



public void ReducePlayerLife()
{
    gameManager.UpdatePlayerLivesUI(this);//sharan
    if(score == 0)
    {
        GameOver();
    }
}
   
   
}
