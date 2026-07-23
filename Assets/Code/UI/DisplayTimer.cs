using Code.Managers;
using TMPro;
using UnityEngine;

public class DisplayTimer : MonoBehaviour
{
    private TMP_Text _timerText;

    private void Awake()
    {
        _timerText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (!TimerManager.instance) return;
        var minutes = Mathf.Floor(TimerManager.instance.Timer / 60);
        var seconds = TimerManager.instance.Timer % 60;
        // var milliseconds = TimerManager.instance.Timer / 100; // idek, bro
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
