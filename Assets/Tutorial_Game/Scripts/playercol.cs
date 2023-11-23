using UnityEngine;
using TMPro;

public class playercol : MonoBehaviour
{
    public TextMeshProUGUI collectText;
    public float popUpDuration = 3.0f; // Set the duration to 3 seconds

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            collected = true;
            collectText.text = "+4 You Killed";
            collectText.enabled = true; // Show the TextMeshProUI component
            gameObject.SetActive(false);

            FindObjectOfType<SoundManager>().Play("playerdeath");

            // Schedule the method to disable the TextMeshProUI component after popUpDuration seconds
            Invoke("DisableCollectText", popUpDuration);
        }
    }

    private void DisableCollectText()
    {
        collectText.enabled = false; // Hide the TextMeshProUI component
    }
}
