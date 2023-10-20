using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restartgame : MonoBehaviour
{
    // Attach this script to the UI Image GameObject.

    private Button button;

    void Start()
    {
        // Get the Button component from the UI Image GameObject.
        button = GetComponent<Button>();

        // Add an onClick listener to reload the current scene.
        button.onClick.AddListener(ReloadCurrentScene);
    }

    void ReloadCurrentScene()
    {
        // Get the name of the current scene and reload it.
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
