using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{   
    [SerializeField] private float score = 10;
    [SerializeField] private float timeActive = 0;
    [SerializeField] private int playerNumber;
    [SerializeField] private bool isPlayerActive;
    [SerializeField] public int numOfLives = 3;
    [SerializeField] public int numOfTimesKilledByBlackHole = 0;
    [SerializeField] public int numOfTimesKilledByPlayer = 0;
    [SerializeField] public int numOfCollectiblesCollected = 0;
    [SerializeField] public int numOfGoodCollectiblesCollected = 0;
    [SerializeField] public int numOfBadCollectiblesCollected = 0;

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
    public float GetScore()
    {
        return score;
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
    numOfLives--;
    gameManager.UpdatePlayerLivesUI(this);//sharan
    if(numOfLives == 0)
    {
        GameOver();
    }
}
   
   
}
