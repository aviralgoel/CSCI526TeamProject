using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // Specify the name of the scene to load in the Inspector

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
