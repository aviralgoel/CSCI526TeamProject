using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{   
    [SerializeField] private float score = 10;
    [SerializeField] private float timeActive = 0;
    [SerializeField] private int playerNumber;
    [SerializeField] private bool isPlayerActive;
    [SerializeField] private int numOfLives = 3;
    [SerializeField] public int numOfTimesKilledByBlackHole = 0;
    [SerializeField] public int numOfTimesKilledByPlayer = 0;
    [SerializeField] public int numOfCollectiblesCollected = 0;

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
    }
    public void RespawnPlayer(string tagOfKiller)
    {   
        // analytics collector
        if(tagOfKiller == "Blackhole")
        {
            numOfTimesKilledByBlackHole++;
            PlayerIsDead(); return;
        }
        else if(tagOfKiller == "OtherPlayer")
        {
            numOfTimesKilledByPlayer++;
            numOfLives--;
            if(numOfLives == 0)
            {
                PlayerIsDead(); return;
            }
        }
        else if(tagOfKiller == "Collectible")
        {
            numOfCollectiblesCollected++;
            return;
        }
        transform.position = respawnPosition;

    }
    public void PlayerIsDead() 
    {   
        // sstop the player
        isPlayerActive = false;
        this.gameObject.SetActive(false);
    }

   
   
}
