using UnityEngine;

namespace Code.UI
{
    public class ChangeSceneButton : MonoBehaviour
    {
        public string nextSceneName;

        public void OnClick()
        {
            if (!GameEventManager.instance) return; 
            GameEventManager.instance.sceneEvents.OnLoadScene(nextSceneName);
        }
    }
}