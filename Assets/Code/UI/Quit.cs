using UnityEngine;

namespace Code.UI
{
    public class Quit : MonoBehaviour
    {
        public void QuitGame()
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.miscellaneousEvents.OnQuitGame();
        }
    }
}