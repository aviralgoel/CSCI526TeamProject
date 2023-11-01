using UnityEngine;
using TMPro;
using System.Collections;

public class CollectibleBehavior : MonoBehaviour
{
    public TextMeshProUGUI popupText;
    public float popUpDuration = 2.0f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
        {
            return; // If the collectible has already been collected, exit early.
        }

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            collected = true;
            popupText.text = "Collected! +1";
            StartCoroutine(ShowAndHidePopUpCoroutine());
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowAndHidePopUpCoroutine()
    {
        popupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(popUpDuration);

        popupText.gameObject.SetActive(false);
    }
}
