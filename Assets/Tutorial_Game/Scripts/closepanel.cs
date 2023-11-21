using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class closepanel : MonoBehaviour, IPointerClickHandler
{
    public GameObject settingsPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (settingsPanel != null)
        {
            // Toggle the active state of the settings panel
            settingsPanel.SetActive(!settingsPanel.activeSelf);

            // Pause or resume the game based on the panel's active state
            Time.timeScale = settingsPanel.activeSelf ? 0f : 1f;

            FindObjectOfType<SoundManager>().Play("button");
        }
    }
}
