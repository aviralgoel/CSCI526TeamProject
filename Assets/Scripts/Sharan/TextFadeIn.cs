using UnityEngine;
using UnityEngine.UI;

public class TextFadeIn : MonoBehaviour
{
    public Text text;
    public float fadeDuration = 2.0f;
    
    private float timer = 0;

    private void Start()
    {
        // Set the text's alpha to 0 to make it completely transparent.
        Color textColor = text.color;
        textColor.a = 0;
        text.color = textColor;
    }

    private void Update()
    {
        if (timer < fadeDuration)
        {
            // Calculate the alpha value based on the timer and fade duration.
            Color textColor = text.color;
            textColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            text.color = textColor;

            timer += Time.deltaTime;
        }
    }
}
