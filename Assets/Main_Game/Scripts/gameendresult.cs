using UnityEngine;
using TMPro;

public class gameendresult : MonoBehaviour
{
    public TMP_Text resultText;

    void Start()
    {
        int winningPlayer = PlayerPrefs.GetInt("WinningPlayer", 0);
        Color textColor = Color.white;

        if (winningPlayer == 1)
        {
            resultText.text = "Player 1 Wins!";
       
            ColorUtility.TryParseHtmlString("#FF00AC", out textColor);
        }
        else if (winningPlayer == 2)
        {
            resultText.text = "Player 2 Wins!";
    
            ColorUtility.TryParseHtmlString("#0616F8", out textColor);
        }

        // Assign the col
        resultText.color = textColor;

     
        PlayerPrefs.DeleteKey("WinningPlayer");
    }
}
