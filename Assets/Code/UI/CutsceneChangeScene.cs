using UnityEngine;

public class CutsceneChangeScene : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timer = 5f;

    [Header("Continue UI")]
    public GameObject continueButton;

    private float _timer;
    private bool _timerFinished;
    private bool _sceneTransitionStarted;

    private void Start()
    {
        _timer = timer;

        if (continueButton) return;
        Debug.LogError("Continue Button is not assigned!");
        enabled = false;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0) return;
        
        continueButton.SetActive(true);
    }
}