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
    // Start is called before the first frame update
    void Start()
    {
        isPlayerActive = false;
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

   
}
