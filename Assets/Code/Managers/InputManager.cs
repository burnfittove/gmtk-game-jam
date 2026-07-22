using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class InputManager : MonoBehaviour
    {
        public void Move(InputAction.CallbackContext ctx)
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.inputEvents.OnMove(ctx);
        }

        public void Pause(InputAction.CallbackContext ctx)
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.inputEvents.OnPause(ctx);
        }
    }
}