using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneChangeScene : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float timer = 5f;

    [Header("Next Scene")]
    [SerializeField] private string nextScene;

    [Header("Continue UI")]
    [SerializeField] private TMP_Text continueText;
    [SerializeField] private Button continueButton;

    private float _timer;
    private bool _timerFinished;
    private bool _sceneTransitionStarted;

    private void Start()
    {
        _timer = timer;

        if (continueText == null)
        {
            Debug.LogError("Continue Text is not assigned!");
            enabled = false;
            return;
        }

        if (continueButton == null)
        {
            Debug.LogError("Continue Button is not assigned!");
            enabled = false;
            return;
        }

        // Show the text object, but make it completely transparent.
        continueText.gameObject.SetActive(true);
        SetTextAlpha(0f);

        // Hide the button until the timer reaches zero.
        continueButton.gameObject.SetActive(false);
        continueButton.interactable = false;

        continueButton.onClick.AddListener(ContinueToNextScene);
    }

    private void Update()
    {
        if (_timerFinished || _sceneTransitionStarted)
            return;

        _timer -= Time.deltaTime;

        // Calculate how far the countdown has progressed.
        float fadeProgress;

        if (timer <= 0f)
        {
            fadeProgress = 1f;
        }
        else
        {
            fadeProgress = 1f - (_timer / timer);
        }

        // Fade the text from transparent to fully visible.
        SetTextAlpha(Mathf.Clamp01(fadeProgress));

        if (_timer > 0f)
            return;

        _timer = 0f;
        _timerFinished = true;

        // Ensure the text is completely visible.
        SetTextAlpha(1f);

        // Reveal the Continue button.
        continueButton.gameObject.SetActive(true);
        continueButton.interactable = true;
    }

    private void ContinueToNextScene()
    {
        if (_sceneTransitionStarted)
            return;

        _sceneTransitionStarted = true;

        // Prevent multiple button presses.
        continueButton.interactable = false;

        // Hide both UI objects before starting the transition.
        continueButton.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);

        if (GameEventManager.instance == null)
        {
            Debug.LogError("GameEventManager instance is missing!");
            return;
        }

        GameEventManager.instance.sceneEvents.OnLoadScene(nextScene);
    }

    private void SetTextAlpha(float alpha)
    {
        Color textColor = continueText.color;
        textColor.a = alpha;
        continueText.color = textColor;
    }

    private void OnDestroy()
    {
        if (continueButton != null)
        {
            continueButton.onClick.RemoveListener(
                ContinueToNextScene
            );
        }
    }
}