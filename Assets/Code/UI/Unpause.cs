using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.UI
{
    public class Unpause : MonoBehaviour
    {
        public void UnpauseGame()
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.inputEvents.OnPause(new InputAction.CallbackContext());
        }
    }
}