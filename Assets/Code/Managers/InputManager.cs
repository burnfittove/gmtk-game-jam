using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Managers
{
    public class InputManager : MonoBehaviour
    {
        public void Move(InputAction.CallbackContext ctx)
        {
            GameEventManager.instance.inputEvents.OnMove(ctx);
        }

        public void Pause(InputAction.CallbackContext ctx)
        {
            GameEventManager.instance.inputEvents.OnPause(ctx);
        }
    }
}