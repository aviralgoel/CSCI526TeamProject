using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{   
    public float score = 10;
    public float timeActive = 0;
    public int playerNumber;
    public bool isPlayerActive;

    public int numOfLives = 2;
    public int numOfTimesKilledByBlackHole = 0;
    public int numOfTimesKilledByPlayer = 0;
    public int numOfTimesKilledByCollectible = 0;

    
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
        if(Input.GetKeyDown(KeyCode.S)) 
        {
            GetComponent<PlayerInputController>().speedMultiplier *= 2;
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
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
    public float GetScore()
    {
        return score;
    }
    public float GetTimeActive() 
    {
        return timeActive;
    }
    public void ChangeScore(int amount)
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
        }
        else if(tagOfKiller == "OtherPlayer")
        {
            numOfTimesKilledByPlayer++;
        }
        else if(tagOfKiller == "Bad")
        {
            numOfTimesKilledByCollectible++;
        }

        // player lives handler
        if(numOfLives > 0) 
        {
            transform.position = respawnPosition;
            numOfLives--;
        }
        else
        {
            PlayerIsDead();
        }
        

    }
    private void PlayerIsDead() 
    {
        isPlayerActive = false;
        this.gameObject.SetActive(false);
    }

   
   
}
