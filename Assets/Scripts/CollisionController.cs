using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    
    //public ScoreManager scoreManager;
    public int scoreOnKill = 4;
    Vector3 respawnLocation = new Vector3(1.5f, 1.4f, 0f);
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
            this.gameObject.GetComponentInParent<ScoreManager>().ChangeScore(scoreOnKill); // + score
            collision.gameObject.GetComponentInParent<ScoreManager>().ChangeScore(-scoreOnKill); // - score
            collision.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("OtherPlayer");
        }
        else if (this.gameObject.CompareTag("Player2Blade") && collision.gameObject.CompareTag("Player1"))
        {
            this.gameObject.GetComponentInParent<ScoreManager>().ChangeScore(scoreOnKill);
            collision.gameObject.GetComponentInParent<ScoreManager>().ChangeScore(-scoreOnKill);
            collision.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("OtherPlayer");
        }
        else if(collision.gameObject.CompareTag("Blackhole"))
        {
            this.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("Blackhole");
        }
    }

}