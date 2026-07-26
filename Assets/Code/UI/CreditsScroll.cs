using TMPro;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI creditsText;

    [Header("Scrolling")]
    [SerializeField] private float scrollSpeed = 100f;

    [Header("Start / End Position")]
    [SerializeField] private float startY = -800f;
    [SerializeField] private float endY = 1200f;

    [Header("Behaviour")]
    [SerializeField] private bool disableWhenFinished = true;
    [SerializeField] private bool destroyWhenFinished = false;

    public string mainMenuScene = "Main Menu";

    private RectTransform creditsRect;

    private void Awake()
    {
        if (creditsText == null)
        {
            Debug.LogError("Credits Text is not assigned!");
            enabled = false;
            return;
        }

        creditsRect = creditsText.rectTransform;
    }

    private void Start()
    {
        ResetCredits();
    }

    private void Update()
    {
        creditsRect.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);

        if (!(creditsRect.anchoredPosition.y >= endY)) return;
        if (destroyWhenFinished)
        {
            Destroy(creditsText.gameObject);
        }
        else if (disableWhenFinished)
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.sceneEvents.OnLoadScene(mainMenuScene);
        }

        enabled = false;
    }

    public void ResetCredits()
    {
        var position = creditsRect.anchoredPosition;
        position.y = startY;
        creditsRect.anchoredPosition = position;
    }
}