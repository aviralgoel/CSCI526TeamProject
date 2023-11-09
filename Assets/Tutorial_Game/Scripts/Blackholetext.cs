using UnityEngine;
using TMPro;

public class Blackholetext : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    void Start()
    {
        if (textComponent != null)
        {
            textComponent.gameObject.SetActive(false);  // Hide the text initially
            Invoke("ShowTextComponent", 5f);  // Schedule the text to be shown after 5 seconds
        }
        else
        {
            Debug.LogError("Text component not assigned!");
        }
    }

    void ShowTextComponent()
    {
        textComponent.gameObject.SetActive(true);
        //Invoke("HideTextComponent", 5f);  // Schedule the text to be hidden after 5 seconds
    }

    /*void HideTextComponent()
    {
        textComponent.gameObject.SetActive(false);
    }*/
}
