using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.UI
{
    public class Unpause : MonoBehaviour
    {
        public void UnpauseGame()
        {
            GameEventManager.instance.inputEvents.OnPause(new InputAction.CallbackContext());
        }
    }
}