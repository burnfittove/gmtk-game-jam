using UnityEngine;

namespace Code.UI
{
    public class Quit : MonoBehaviour
    {
        public void QuitGame()
        {
            GameEventManager.instance.miscellaneousEvents.OnQuitGame();
        }
    }
}