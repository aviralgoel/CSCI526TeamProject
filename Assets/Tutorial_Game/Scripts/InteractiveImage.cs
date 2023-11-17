using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class InteractiveImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color hoverColor = Color.red;
    public Color clickColor = Color.green;

    private Color originalColor;
    private Image image;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;

        // Assuming the TextMeshProUGUI component is a child of the image
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;

        // Change text color on hover
        if (textComponent != null)
        {
            textComponent.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;

        // Restore original text color on exit
        if (textComponent != null)
        {
            textComponent.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = clickColor;

        // Handle click logic here
        // For example, you can add a method to be called on click
        HandleClick();
    }

    private void HandleClick()
    {
        // Your click handling logic goes here
        Debug.Log("Image Clicked!");

        // Change the scene when the image is clicked
        SceneManager.LoadScene("Aviral");
    }
}
