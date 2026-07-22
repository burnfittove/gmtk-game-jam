using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused;
    public GameObject pauseCanvas;
    
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
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
