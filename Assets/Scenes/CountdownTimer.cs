using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public Text instructionTitleText;
    public Text instructionContentText;
    public Image instructionBackground;
    public float countdownDuration = 3.0f;
    public float instructionDuration = 3.0f;
    private bool isCounting = false;

    void Awake()
    {
        // Set the instruction elements to inactive initially.
        HideInstruction();
    }

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "Go!";

        yield return new WaitForSeconds(1);

        DisplayInstruction();
        yield return new WaitForSeconds(instructionDuration);
        HideInstruction();

        LoadNextScene();
    }

    void DisplayInstruction()
    {
        instructionBackground.gameObject.SetActive(true);
        instructionTitleText.gameObject.SetActive(true);
        instructionContentText.gameObject.SetActive(true);
    }

    void HideInstruction()
    {
        instructionBackground.gameObject.SetActive(false);
        instructionTitleText.gameObject.SetActive(false);
        instructionContentText.gameObject.SetActive(false);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Aviral");
    }
}
