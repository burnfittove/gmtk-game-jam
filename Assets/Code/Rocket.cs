using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    public UnityEvent OnAnimationEnd;
    public string nextScene;

    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }

    public void ChangeScene(string scene)
    {
        nextScene = scene;
    }
}
