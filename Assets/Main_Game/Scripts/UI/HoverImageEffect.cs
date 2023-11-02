using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverImageEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public string sceneToLoad;

    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
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
        // Load the specified scene when the image is clicked.
       // FindObjectOfType<SoundManager>().Play("button");
        SceneManager.LoadScene(sceneToLoad);
    }
}
