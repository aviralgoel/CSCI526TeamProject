
using UnityEngine;
using TMPro;

public class gameendresult : MonoBehaviour
{
    public TMP_Text resultText;

    void Start()
    {
       
        int winningPlayer = PlayerPrefs.GetInt("WinningPlayer", 0);

     
        if (winningPlayer == 1)
        {
            resultText.text = "Player 1 Wins!";
        }
        else if (winningPlayer == 2)
        {
            resultText.text = "Player 2 Wins!";
        }
      

        // Clear PlayerPrefs for cleanliness (optional)
        PlayerPrefs.DeleteKey("WinningPlayer");
    }
}
