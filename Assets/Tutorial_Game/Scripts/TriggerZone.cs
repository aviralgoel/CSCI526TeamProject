using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerZone : MonoBehaviour
{
     public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    private bool player1Inside = false;
    private bool player2Inside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) // Change "Player1" to the tag of Player 1.
        {
            player1Inside = true;
            player1Text.text = "Player 1's message here";
            player1Text.gameObject.SetActive(true);
        }
        else if (other.CompareTag("Player2")) // Change "Player2" to the tag of Player 2.
        {
            player2Inside = true;
            player2Text.text = "Player 2's message here";
            player2Text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1")) // Change "Player1" to the tag of Player 1.
        {
            player1Inside = false;
            player1Text.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Player2")) // Change "Player2" to the tag of Player 2.
        {
            player2Inside = false;
            player2Text.gameObject.SetActive(false);
        }
    }
}
