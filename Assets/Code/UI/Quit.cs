using UnityEngine;

namespace Code.UI
{
    public class Quit : MonoBehaviour
    {
        public AudioClip clickSound;
        
        public void QuitGame()
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.audioEvents.OnPlay(clickSound);
            GameEventManager.instance.miscellaneousEvents.OnQuitGame();
        }
    }
}