using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{   
    public TextMeshProUGUI player1PanelText;
    public TextMeshProUGUI player2PanelText;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer1PanelnText(string text)
    {
        player1PanelText.text = text;
    }
    public void SetPlayer2PanelText(string text)
    {
        player2PanelText.text = text;
    }
    
}
