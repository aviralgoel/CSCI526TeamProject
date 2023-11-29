using UnityEngine;

public class PlayerAnalytics : MonoBehaviour
{   
    public int playerNumber;
    [SerializeField]int numOfKillAttemptsByOpponents = 0;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        // find script on parent game object
        scoreManager = GetComponentInParent<ScoreManager>();
        playerNumber = scoreManager.GetPlayerNumber();
    }

        // Update is called once per frame

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(playerNumber == 1 && collision.CompareTag("Player2Blade"))
       {
           numOfKillAttemptsByOpponents++;
       }
       if(playerNumber == 2 && collision.CompareTag("Player1Blade"))
       {
            numOfKillAttemptsByOpponents++;
       }
    }

    public int GetNumOfKillAttemptsByOpponents()
    {
        return numOfKillAttemptsByOpponents;
    }
}
