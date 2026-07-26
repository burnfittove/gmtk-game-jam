using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused;
    public GameObject pauseObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.inputEvents.Pause += ChangePauseState;
    }

    private void ChangePauseState(InputAction.CallbackContext ctx)
    {
        _isPaused = !_isPaused;

        if (_isPaused) Pause();
        else Resume();
    }

    private void Pause()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
    }
}
