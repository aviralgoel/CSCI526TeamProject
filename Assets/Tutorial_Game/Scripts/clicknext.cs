using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;


public class clicknext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public string sceneToLoad;
    public float clickScaleMultiplier = 1.2f;
    public float animationDuration = 0.2f;

    private Vector3 originalScale;

    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        // Store the original scale of the button.
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play a click sound.
        FindObjectOfType<SoundManager>().Play("button");

        // Trigger the click animation.
        StartCoroutine(ClickAnimationCoroutine());
    }

    private IEnumerator ClickAnimationCoroutine()
    {
        float timer = 0f;
        Vector3 targetScale = originalScale * clickScaleMultiplier;

        while (timer < animationDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / animationDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;

        // Load the specified scene when the animation is complete.
        SceneManager.LoadScene(sceneToLoad);
    }
}
