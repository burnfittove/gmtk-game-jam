using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    public string nextScene;

    public void ChangeScene()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.sceneEvents.OnLoadScene(nextScene);
    }
}
