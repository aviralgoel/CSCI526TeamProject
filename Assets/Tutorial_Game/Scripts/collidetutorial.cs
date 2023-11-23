using UnityEngine;
using TMPro;

public class collidetutorial : MonoBehaviour
{
    // The text message to be displayed
    public string messageToShow = "Collision Detected!";

    // Reference to the TextMeshProUGUI text element
    public TextMeshProUGUI messageText;

    private void Start()
    {
        // Ensure that the TextMeshProUGUI Text component is assigned
        if (messageText == null)
        {
            Debug.LogError("Please assign the TextMeshProUGUI component for message display in the inspector.");
        }
        else
        {
            // Hide the text initially
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player1"))
        {
            // Display the message when the player collides
            Debug.Log(messageToShow);

            // Display the message on the screen using the TextMeshProUGUI text element
            DisplayMessageOnUI(messageToShow);
        }
    }

    // Function to display a message on the TextMeshProUGUI
    private void DisplayMessageOnUI(string message)
    {
        // Ensure that the TextMeshProUGUI component is assigned
        if (messageText != null)
        {
            // Set the text of the TextMeshProUGUI element
            messageText.text = message;

            // Show the TextMeshProUGUI text element
            messageText.gameObject.SetActive(true);

            // You might want to hide the text after a certain duration
            // You can use StartCoroutine to delay the hiding or use a timer
            // For example: StartCoroutine(HideMessageAfterDelay(2.0f));
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not assigned for message display.");
        }
    }

    // Coroutine to hide the message after a delay (optional)
    private System.Collections.IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Hide the TextMeshProUGUI text element after the delay
        messageText.gameObject.SetActive(false);
    }
}
