using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;

public class ScoreManager : MonoBehaviour
{   
    [SerializeField] private float score = 100;
    [SerializeField] private float timeActive = 0;
    [SerializeField] private int playerNumber;
    [SerializeField] private bool isPlayerActive;
    private Spawnercode spawner;
    // [SerializeField] public int numOfLives = 3;
    [HideInInspector] public int numOfTimesKilledByBlackHole = 0;
    [HideInInspector] public int numOfTimesKilledByPlayer = 0;
    [HideInInspector] public int numOfCollectiblesCollected = 0;
    [HideInInspector] public int numOfGoodCollectiblesCollected = 0;
    [HideInInspector] public int numOfBadCollectiblesCollected = 0;
    [SerializeField] private GameObject floatingText;
    public List<GameObject> walls = new List<GameObject>();
    public PowerUpManager PowerUpManagerPlayer1;
    public PowerUpManager PowerUpManagerPlayer2;

    public float myFirstDeathTime = 0;
    private bool isMyFirstDeath = false;

    public PlayerInputController player;


    public Image HealthBar;
    public float TotalHealth;
    
    [SerializeField]
    private GameManager gameManager;

    Vector3 respawnPosition;
    [Header("Mechanic: ChargeUp Area")]
    [SerializeField] private bool isInsideSpeedUp = false;
    [SerializeField] private float healingAmountPerSecond = 2.0f;
    [SerializeField] private float damageAmountPerSecond = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerActive = false;
        respawnPosition = transform.position;
        spawner = FindObjectOfType<Spawnercode>();
        score = TotalHealth;
        player = GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerActive)
        {
            UpdateTime();
        }

        if(gameManager.isGameStarted) {
            score -= (Time.deltaTime/1.2f);
            HealthBar.fillAmount = score / TotalHealth;
            if(score <= 0)
            {
                score = 0;

                if(!gameManager.isGameOver)
                {
                    GameOver();
                }
                
            }
            isPlayerActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "SpeedUp" && !isInsideSpeedUp)
        {   

            isInsideSpeedUp = true;
            if(collision.gameObject.GetComponent<SpeedUp>().beginHealing)
            {
                InvokeRepeating("HealOverTime", 0.1f, 1.0f);
            }
            else if(collision.gameObject.GetComponent<SpeedUp>().beginDamage)
            {
                InvokeRepeating("DamageOverTime", 0.1f, 1.0f);
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((PowerUpManagerPlayer1.fireWallActive || PowerUpManagerPlayer2.fireWallActive) && walls.Contains(collision.gameObject))
        {
            IncrementScore(-1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SpeedUp" && isInsideSpeedUp)
        {
            isInsideSpeedUp = false;
            if (collision.gameObject.GetComponent<SpeedUp>().beginHealing)
            {
                CancelInvoke("HealOverTime");
            }
            else if (collision.gameObject.GetComponent<SpeedUp>().beginDamage)
            {
                CancelInvoke("DamageOverTime");
            }
            
        }
    }

    void showDamage(string text)
    {
        if(floatingText)
        {
            GameObject prefab = Instantiate(floatingText, transform.position + new Vector3(0.5f, 1.5f, 0), Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }

    private void UpdateTime()
    {
        timeActive += Time.deltaTime;
    }
    
    public void SetPlayerActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
        //Debug.Log("Player " + playerNumber + " has joined the game");
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
        string damage;
        if(amount < 0) damage = amount.ToString();
        else damage = "+" +  amount.ToString();
        showDamage(damage);
        score += amount;
        HealthBar.fillAmount = score / TotalHealth;
    }
    public void RespawnPlayer(string tagOfKiller)
    {
        // analytics collector

        if(tagOfKiller == "Blackhole")
        {   
            if(!isMyFirstDeath) 
            {
                myFirstDeathTime = Time.time;
                isMyFirstDeath = true;
            }

        if (tagOfKiller == "Blackhole")
        {

            numOfTimesKilledByBlackHole++;
            StartCoroutine(RespawnAfterDelay(5f));
            return;
        }
        else if (tagOfKiller == "OtherPlayer")
        {
            numOfTimesKilledByPlayer++;
            StartCoroutine(RespawnAfterDelay(5f));
            return;
            //ReducePlayerLife();
        }
        else if (tagOfKiller == "Good" || tagOfKiller == "Bad")
        {
            numOfCollectiblesCollected++;
            if (tagOfKiller == "Good")
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

    IEnumerator RespawnAfterDelay(float delay)
    {
        transform.position = respawnPosition;
        player.isMovementAllowed = false;
        yield return new WaitForSeconds(delay);
        player.isMovementAllowed = true;

    }


    public void GameOver() 
    {   
        // sstop the player
        isPlayerActive = false;
        this.gameObject.SetActive(false);
        gameManager.isGameOver = true;
        gameManager.losePlayerNumber = playerNumber;
        spawner.StopSpawning();

        FindObjectOfType<SoundManager>().Play("GameOver");

    }

    private void HealOverTime()
    {
        FindObjectOfType<SoundManager>().Play("good");
        IncrementScore((int)healingAmountPerSecond);
    }
    private void DamageOverTime()
    {
        FindObjectOfType<SoundManager>().Play("bad");
        IncrementScore((int)(damageAmountPerSecond)*-1);
    }



}
