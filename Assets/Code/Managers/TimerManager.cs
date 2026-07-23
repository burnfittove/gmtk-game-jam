using UnityEngine;

namespace Code.Managers
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager instance;
        public float levelTimer;
        public float Timer { get; private set; }
        public AudioClip tenSecondWarning;
        private bool _isTimerActive;
        private bool _isTimerExpired;
        private bool _isWarned;

        private void Awake()
        {
            if (instance && instance != this)
            {
                Debug.LogWarning("Multiple instances of TimerManager!");
                gameObject.SetActive(false);
                return;
            }
            instance = this;
            
            Timer = levelTimer;
        }

        private void Start()
        {
            GameEventManager.instance.timerEvents.timerStart += StartTimer;
            GameEventManager.instance.timerEvents.timerUpdate += UpdateTimer;
            // ##### DEBUG #####
            GameEventManager.instance.inputEvents.Move += _ => _isTimerActive = true;
        }

        private void Update()
        {
            Timer = Mathf.Clamp(Timer, 0, levelTimer);
            
            if (!_isTimerActive) return;
            if (_isTimerExpired) return;
            
            Timer -= Time.deltaTime;

            if (Mathf.FloorToInt(Timer) % 10 == 0 && !_isWarned) PlayTenSecondWarning();
            
            if (Timer > 0) return;
            GameEventManager.instance.timerEvents.OnTimerExpired();
            _isTimerExpired = true;
        }

        private void UpdateTimer(float deltaTime)
        {
            Timer += deltaTime;
        }

        public void StartTimer() => _isTimerActive = true;

        private void PlayTenSecondWarning()
        {
            if (!GameEventManager.instance) return;
            GameEventManager.instance.audioEvents.OnPlay(tenSecondWarning);
            _isWarned = true;
        }
    }
}