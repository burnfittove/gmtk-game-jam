using UnityEngine;

namespace Code.Managers
{
    public class TimerManager : MonoBehaviour
    {
        public static TimerManager instance;
        public float levelTimer;
        public float Timer { get; private set; }
        private bool _isTimerActive;
        private bool _isTimerExpired;
        public string loseSceneName;
        private AudioSource _audioSource;
        private bool _isWarned;
        private bool _isLevelFinished;

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
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            GameEventManager.instance.timerEvents.timerStart += StartTimer;
            GameEventManager.instance.timerEvents.timerUpdate += UpdateTimer;
            // ##### DEBUG ##### oops not anymore, i kinda like this
            GameEventManager.instance.inputEvents.Move += _ => _isTimerActive = true;
            GameEventManager.instance.sceneEvents.LoadScene += _ => _isLevelFinished = true;
        }

        private void Update()
        {
            Timer = Mathf.Clamp(Timer, 0, levelTimer);
            
            if (!_isTimerActive) return;
            if (_isTimerExpired) return;
            if (_isLevelFinished) return;
            
            Timer -= Time.deltaTime;

            if (!_isWarned && Timer < 11)
            {
                _isWarned = true;
                _audioSource.volume = 0.7f;
            }
            
            if (Timer > 0) return;
            if (!GameEventManager.instance) return;
            GameEventManager.instance.timerEvents.OnTimerExpired();
            GameEventManager.instance.sceneEvents.OnLoadScene(loseSceneName);
            _isTimerExpired = true;
        }

        private void UpdateTimer(float deltaTime)
        {
            Timer += deltaTime;
        }

        public void StartTimer() => _isTimerActive = true;

        // private void PlayTenSecondWarning()
        // {
        //     if (!GameEventManager.instance) return;
        //     GameEventManager.instance.audioEvents.OnPlay(tenSecondWarning);
        //     _isWarned = true;
        // }

     
    }
}