using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class interactive_hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor = Color.red;

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
}
