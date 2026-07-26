using UnityEngine;

namespace Code.UI
{
    public class ChangeSceneButton : MonoBehaviour
    {
        public string nextSceneName;
        public AudioClip clickSound;

        public void OnClick()
        {
            if (!GameEventManager.instance) return; 
            GameEventManager.instance.sceneEvents.OnLoadScene(nextSceneName);
            GameEventManager.instance.audioEvents.OnPlay(clickSound);
        }
    }
}