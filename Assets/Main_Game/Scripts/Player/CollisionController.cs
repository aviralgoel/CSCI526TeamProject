using UnityEngine;

public class CollisionController : MonoBehaviour
{

    //public ScoreManager scoreManager;
    public int scoreOnKill = 4;
    public int scoreOnGreen = 2;
    public int scoreOnRed = -2;

    public ScoreManager scoreManagerPlayer1;
    public ScoreManager scoreManagerPlayer2;
    public PowerUpManager powerUpManager;


    private void Start()
    {
        powerUpManager = GetComponentInParent<PowerUpManager>();
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
            UIManager.instance.SetPlayer1PowerUpText("You killed the other player");

            FindObjectOfType<SoundManager>().Play("playerdeath");

        }
        else if (this.gameObject.CompareTag("Player2Blade") && collision.gameObject.CompareTag("Player1"))
        {
            scoreManagerPlayer2.GetComponentInParent<ScoreManager>().IncrementScore(scoreOnKill); // + score
            scoreManagerPlayer1.GetComponentInParent<ScoreManager>().IncrementScore(-scoreOnKill); // - score
            scoreManagerPlayer1.GetComponentInParent<ScoreManager>().RespawnPlayer("OtherPlayer"); // respawn
            UIManager.instance.SetPlayer2PowerUpText("You killed the other player");

            FindObjectOfType<SoundManager>().Play("playerdeath");

        }
        else if (collision.gameObject.CompareTag("Blackhole"))
        {
            this.gameObject.GetComponentInParent<ScoreManager>().RespawnPlayer("Blackhole");

            FindObjectOfType<SoundManager>().Play("playerdeath");

        }
        else if(collision.gameObject.CompareTag("FireWalls"))
        {
            powerUpManager.addPowerUp(PowerUpManager.PowerUpType.FireWalls);
            Debug.Log("Power Up: Firewall Collected");
        }
        else if(collision.gameObject.CompareTag("Freeze"))
        {
            powerUpManager.addPowerUp(PowerUpManager.PowerUpType.Freeze);
            Debug.Log("Power Up: Freeze Collected");

            FindObjectOfType<SoundManager>().Play("freeze");

        }
        // detect collision with good and bad objects
        else if (collision.CompareTag("Good") || collision.CompareTag("Bad"))
        {
            if (gameObject.CompareTag("Player1Blade"))
            {
                int scoreChange = collision.gameObject.CompareTag("Good") ? scoreOnGreen : scoreOnRed;
                scoreManagerPlayer1.IncrementScore(scoreChange);
                scoreManagerPlayer1.RespawnPlayer(collision.gameObject.tag); // this will not actually respawn player, just increase count of collectible
            
                FindObjectOfType<SoundManager>().Play("bad");

            }
            else if (gameObject.CompareTag("Player2Blade"))
            {
                int scoreChange = collision.gameObject.CompareTag("Good") ? scoreOnGreen : scoreOnRed;
                scoreManagerPlayer2.IncrementScore(scoreChange);
                scoreManagerPlayer2.RespawnPlayer(collision.gameObject.tag); // this will not actually respawn player, just increase count of collectible
            
                FindObjectOfType<SoundManager>().Play("good");
            
            }
            // collectible is already being destroyed in collectible script
            //Destroy(collision.gameObject);
        }
    }
}

    
   

