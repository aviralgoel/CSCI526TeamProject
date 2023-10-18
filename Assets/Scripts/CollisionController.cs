using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{

    //public ScoreManager scoreManager;
    public int scoreOnKill = 4;
    public ScoreManager scoreManagerPlayer1;
    public ScoreManager scoreManagerPlayer2;


    private void Start()
    {
        //respawnLocation = this.gameObject.transform.root.position;
    }
    // detect collision with other game bodies 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("Player1Blade") && collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player 1 hit Player 2");
            scoreManagerPlayer1.IncrementScore(scoreOnKill); // + score
            scoreManagerPlayer2.IncrementScore(-scoreOnKill); // - score
            scoreManagerPlayer2.RespawnPlayer("OtherPlayer");
        }
        else if (this.gameObject.CompareTag("Player2Blade") && collision.gameObject.CompareTag("Player1"))
        {
            scoreManagerPlayer2.GetComponentInParent<ScoreManager>().IncrementScore(scoreOnKill); // + score
            scoreManagerPlayer1.GetComponentInParent<ScoreManager>().IncrementScore(-scoreOnKill); // - score
            scoreManagerPlayer1.GetComponentInParent<ScoreManager>().RespawnPlayer("OtherPlayer"); // respawn
        }
        else if (collision.gameObject.CompareTag("Blackhole"))
        {
            this.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("Blackhole");
        }
        // detect collision with good and bad objects
        else if (collision.CompareTag("Good") || collision.CompareTag("Bad"))
        {
            if (gameObject.CompareTag("Player1Blade"))
            {
                int scoreChange = collision.gameObject.CompareTag("Good") ? 1 : -1;
                scoreManagerPlayer1.IncrementScore(scoreChange);
            }
            else if (gameObject.CompareTag("Player2Blade"))
            {
                int scoreChange = collision.gameObject.CompareTag("Good") ? 1 : -1;
                scoreManagerPlayer2.IncrementScore(scoreChange);
            }
           /* string playerName = (gameObject.CompareTag("Player1Blade")) ? "Player 1" : "Player 2";
            string collisionType = (collision.CompareTag("Good")) ? "Good" : "Bad";
            Debug.Log(playerName + " blade collided with " + collisionType);*/
            Destroy(collision.gameObject);
        }

    }
}

    
   

