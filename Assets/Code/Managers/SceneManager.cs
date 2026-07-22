using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.sceneEvents.LoadScene += LoadScene;
    }

    private void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
