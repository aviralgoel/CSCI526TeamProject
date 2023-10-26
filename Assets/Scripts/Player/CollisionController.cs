/*using UnityEngine;

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
                scoreManagerPlayer1.RespawnPlayer(collision.gameObject.tag); // this will not actually respawn player, just increase count of collectible
            }
            else if (gameObject.CompareTag("Player2Blade"))
            {
                int scoreChange = collision.gameObject.CompareTag("Good") ? 1 : -1;
                scoreManagerPlayer2.IncrementScore(scoreChange);
                scoreManagerPlayer2.RespawnPlayer(collision.gameObject.tag); // this will not actually respawn player, just increase count of collectible
            }
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("Freeze"))
        {

            if (gameObject.CompareTag("Player2"))
            {
                GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
                PlayerInputController playerMve = player1.GetComponent<PlayerInputController>();
                playerMve.isMovementAllowed = false;
                Debug.Log("Player1 has stopped moving.");
            }
            else if (gameObject.CompareTag("Player1"))
            {
                GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
                PlayerInputController playerMve = player2.GetComponent<PlayerInputController>();
                playerMve.isMovementAllowed = false;
                // 

                Debug.Log("Player2 has stopped moving.");
            }

        }
    }
}

*/

using UnityEngine;
using System.Collections;
public class CollisionController : MonoBehaviour
{
    public int scoreOnKill = 4;
    public ScoreManager scoreManagerPlayer1;
    public ScoreManager scoreManagerPlayer2;

    private PlayerInputController playerMve;

    private void Start()
    {
        playerMve = GetComponent<PlayerInputController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1Blade") && gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player 1 hit Player 2");
            scoreManagerPlayer1.IncrementScore(scoreOnKill); // + score
            scoreManagerPlayer2.IncrementScore(-scoreOnKill); // - score
            scoreManagerPlayer2.RespawnPlayer("OtherPlayer");
        }
        else if (collision.CompareTag("Player2Blade") && gameObject.CompareTag("Player1"))
        {
            scoreManagerPlayer2.IncrementScore(scoreOnKill); // + score
            scoreManagerPlayer1.IncrementScore(-scoreOnKill); // - score
            scoreManagerPlayer1.RespawnPlayer("OtherPlayer"); // respawn
        }
        else if (collision.CompareTag("Blackhole"))
        {
            this.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("Blackhole");
        }
        else if (collision.CompareTag("Good") || collision.CompareTag("Bad"))
        {
            HandleGoodOrBadCollision(collision);
        }
        else if (collision.CompareTag("Freeze"))
        {
            StartCoroutine(FreezeAndUnfreeze());
        }
    }

    private void HandleGoodOrBadCollision(Collider2D collision)
    {
        int scoreChange = collision.CompareTag("Good") ? 1 : -1;
        if (gameObject.CompareTag("Player1Blade"))
        {
            scoreManagerPlayer1.IncrementScore(scoreChange);
            scoreManagerPlayer1.RespawnPlayer(collision.gameObject.tag);
        }
        else if (gameObject.CompareTag("Player2Blade"))
        {
            scoreManagerPlayer2.IncrementScore(scoreChange);
            scoreManagerPlayer2.RespawnPlayer(collision.gameObject.tag);
        }
        Destroy(collision.gameObject);
    }

    private IEnumerator FreezeAndUnfreeze()
    {
        if (gameObject.CompareTag("Player2"))
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
            PlayerInputController playerMve = player1.GetComponent<PlayerInputController>();
            playerMve.isMovementAllowed = false;
            Debug.Log("Player1 has stopped moving.");
        }
        else if (gameObject.CompareTag("Player1"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            PlayerInputController playerMve = player2.GetComponent<PlayerInputController>();
            playerMve.isMovementAllowed = false;
            Debug.Log("Player2 has stopped moving.");
        }

        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        if (gameObject.CompareTag("Player2"))
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
            PlayerInputController playerMve = player1.GetComponent<PlayerInputController>();
            playerMve.isMovementAllowed = true;
            Debug.Log("Player1 has started moving after freezing.");
        }
        else if (gameObject.CompareTag("Player1"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            PlayerInputController playerMve = player2.GetComponent<PlayerInputController>();
            playerMve.isMovementAllowed = true;
            Debug.Log("Player2 has started moving after freezing.");
        }
    }
}



/*using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour
{
    public int scoreOnKill = 4;
    public ScoreManager scoreManagerPlayer1;
    public ScoreManager scoreManagerPlayer2;
    private PlayerInputController playerMve;

    private void Start()
    {
        playerMve = GetComponent<PlayerInputController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1Blade") && gameObject.CompareTag("Player2"))
        {
            HandlePlayer1HitPlayer2();
        }
        else if (collision.CompareTag("Player2Blade") && gameObject.CompareTag("Player1"))
        {
            HandlePlayer2HitPlayer1();
        }
        else if (collision.CompareTag("Blackhole"))
        {
            HandleBlackholeCollision();
        }
        else if (collision.CompareTag("Good") || collision.CompareTag("Bad"))
        {
            HandleGoodOrBadCollision(collision);
        }
        else if (collision.CompareTag("Freeze"))
        {
            StartCoroutine(FreezeAndUnfreeze());
        }
    }

    private void HandlePlayer1HitPlayer2()
    {
        Debug.Log("Player 1 hit Player 2");
        scoreManagerPlayer1.IncrementScore(scoreOnKill); // + score
        scoreManagerPlayer2.IncrementScore(-scoreOnKill); // - score
        scoreManagerPlayer2.RespawnPlayer("OtherPlayer");
    }

    private void HandlePlayer2HitPlayer1()
    {
        scoreManagerPlayer2.IncrementScore(scoreOnKill); // + score
        scoreManagerPlayer1.IncrementScore(-scoreOnKill); // - score
        scoreManagerPlayer1.RespawnPlayer("OtherPlayer"); // respawn
    }

    private void HandleBlackholeCollision()
    {
        this.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("Blackhole");
    }

    private void HandleGoodOrBadCollision(Collider2D collision)
    {
        int scoreChange = collision.CompareTag("Good") ? 1 : -1;
        if (gameObject.CompareTag("Player1Blade"))
        {
            scoreManagerPlayer1.IncrementScore(scoreChange);
            scoreManagerPlayer1.RespawnPlayer(collision.gameObject.tag);
        }
        else if (gameObject.CompareTag("Player2Blade"))
        {
            scoreManagerPlayer2.IncrementScore(scoreChange);
            scoreManagerPlayer2.RespawnPlayer(collision.gameObject.tag);
        }
        Destroy(collision.gameObject);
    }

    private IEnumerator FreezeAndUnfreeze()
    {
        playerMve.isMovementAllowed = false; // Freeze the current player

        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        playerMve.isMovementAllowed = true; // Unfreeze the player

        Debug.Log(gameObject.tag + " has started moving after freezing.");
    }
}
*/