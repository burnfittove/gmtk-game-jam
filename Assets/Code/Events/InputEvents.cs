using System;
using UnityEngine.InputSystem;

public class InputEvents
{
    public event Action<InputAction.CallbackContext> Move;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Move?.Invoke(ctx);
    }
    
    public event Action<InputAction.CallbackContext> Pause;
    public void OnPause(InputAction.CallbackContext ctx)
    {
        Pause?.Invoke(ctx);
    }
}
