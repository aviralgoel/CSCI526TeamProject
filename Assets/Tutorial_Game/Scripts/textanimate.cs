using UnityEngine;
using TMPro;
using System.Collections;

public class textanimate : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Reference to your TextMeshPro Text element

    void Start()
    {
        StartCoroutine(UpdateTextAfterDelay(2.0f, "First Text", 3.0f, "Second Text"));
    }

    IEnumerator UpdateTextAfterDelay(float delay1, string text1, float delay2, string text2)
    {
        yield return new WaitForSeconds(delay1);

        // Update the text with the first message
        displayText.text = "Last Man Standing wins";

        yield return new WaitForSeconds(delay2);

        // Update the text with the second message
        displayText.text = "Collect health to keep on going";
    }
}
