using System;

public class SceneEvents
{
    public event Action<string> LoadScene;
    public void OnLoadScene(string sceneName)
    {
        LoadScene?.Invoke(sceneName);
    }
}
