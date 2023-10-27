using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    public TextMeshProUGUI player1PanelText;
    public TextMeshProUGUI player2PanelText;
    public TextMeshProUGUI player1PowerUpText;
    public TextMeshProUGUI player2PowerUpText;
    //public TextMeshProUGUI StartGamePanelText;
    // Start is called before the first frame update
    // make this a singleton
    public static UIManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void SetPlayer1PanelnText(string text)
    {
        player1PanelText.text = text;
    }
    public void SetPlayer2PanelText(string text)
    {
        player2PanelText.text = text;
    }
    public void SetPlayer1PowerUpText(string text)
    {
        player1PowerUpText.text = text;
    }
    public void SetPlayer2PowerUpText(string text)
    {
        player2PowerUpText.text = text;
    }

    
}
