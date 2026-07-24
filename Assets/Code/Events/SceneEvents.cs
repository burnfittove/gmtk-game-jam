using System;

public class SceneEvents
{
    public event Action<string> LoadScene;
    public void OnLoadScene(string sceneName)
    {
        LoadScene?.Invoke(sceneName);
    }
    
    // public event Action<string> FadeIn;
    // public void OnFadeIn()
    // {
    //     FadeIn?.Invoke(null);
    // }
    //
    // public event Action<string> FadeOut;
    // public void OnFadeOut()
    // {
    //     FadeOut?.Invoke(null);
    // }
}
