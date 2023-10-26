using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool player1Joined = false;
    private bool player2Joined = false;

    public float timeToWait = 5f; // Time interval before transitioning to the new scene
    private float timer;

    public string nextSceneName = "NextScene"; // Name of the scene you want to load

    void Start()
    {
        if (player1 != null) player1.SetActive(false);
        if (player2 != null) player2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !player1Joined)
        {
            JoinPlayer(1);
        }

        if (Input.GetKeyDown(KeyCode.A) && !player2Joined)
        {
            JoinPlayer(2);
        }

        if (player1Joined && player2Joined)
        {
            timer += Time.deltaTime;
            if (timer >= timeToWait)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void JoinPlayer(int playerNumber)
    {
        if (playerNumber == 1)
        {
            if (player1 != null)
            {
                player1.SetActive(true);
                player1Joined = true;
                Debug.Log("Player 1 has joined the game!");
            }
        }
        else if (playerNumber == 2)
        {
            if (player2 != null)
            {
                player2.SetActive(true);
                player2Joined = true;
                Debug.Log("Player 2 has joined the game!");
            }
        }
    }
}