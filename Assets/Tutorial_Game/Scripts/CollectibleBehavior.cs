using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectibleBehavior : MonoBehaviour
{
    public TextMeshProUGUI collectText;
    public float popUpDuration = 3.0f; // Set the duration to 3 seconds

    private bool collected = false;
    int score = 1;
    public Image HealthBar;

    void Update()
    {
        HealthBar.fillAmount = score / 2.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            score++;
            HealthBar.fillAmount = score / 2.0f;
            collected = true;
            collectText.text = "+1 Collected!";
            collectText.enabled = true; // Show the TextMeshProUI component
            gameObject.SetActive(false);

            FindObjectOfType<SoundManager>().Play("good");

            // Schedule the method to disable the TextMeshProUI component after popUpDuration seconds
            Invoke("DisableCollectText", popUpDuration);
        }
    }

    private void DisableCollectText()
    {
        collectText.enabled = false; // Hide the TextMeshProUI component
    }
}
