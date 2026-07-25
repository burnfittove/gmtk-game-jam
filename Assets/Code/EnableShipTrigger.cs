using UnityEngine;

namespace Code
{
    public class EnableShipTrigger : MonoBehaviour
    {
        public string transitionScene;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (!GameEventManager.instance) return;
            GameEventManager.instance.sceneEvents.OnLoadScene(transitionScene);
        }
    }
}