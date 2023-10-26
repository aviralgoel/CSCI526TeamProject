using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomFadeInAndOut : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration of each fade (in seconds)
    public float startDelay = 0.0f;   // Delay before the first fade (in seconds)

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Fade in
            yield return Fade(1.0f);

            // Fade out
            yield return Fade(0.0f);
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        Color currentColor = image.color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            image.color = Color.Lerp(currentColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        // Ensure the final alpha is set
        image.color = targetColor;
    }
}
