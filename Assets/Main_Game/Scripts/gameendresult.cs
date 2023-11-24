// GameResultManager.cs
using UnityEngine;
using TMPro;

public class gameendresult : MonoBehaviour
{
    public TMP_Text resultText;

    void Start()
    {
        // Retrieve the winning player from PlayerPrefs
        int winningPlayer = PlayerPrefs.GetInt("WinningPlayer", 0);

        // Display the result in the UI
        if (winningPlayer == 1)
        {
            resultText.text = "Player 1 Wins!";
        }
        else if (winningPlayer == 2)
        {
            resultText.text = "Player 2 Wins!";
        }
        else
        {
            resultText.text = "It's a draw!";
        }

        // Clear PlayerPrefs for cleanliness (optional)
        PlayerPrefs.DeleteKey("WinningPlayer");
    }
}
