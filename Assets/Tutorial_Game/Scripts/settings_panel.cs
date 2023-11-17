using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class settings_panel : MonoBehaviour, IPointerClickHandler
{
    public GameObject popupPanel;

    private void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Show the popup panel
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);

            // Pause the game
            Time.timeScale = 0f;
        }
    }

    public void ClosePopup()
    {
        // Hide the popup panel
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);

            // Resume the game
            Time.timeScale = 1f;
        }
    }
}
