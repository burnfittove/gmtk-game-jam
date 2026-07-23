using System;

namespace Code.Events
{
    public class TimerEvents
    {
        public event Action timerStart;
        public void OnTimerStart()
        {
            timerStart?.Invoke();
        }
        
        public event Action timerExpired;
        public void OnTimerExpired()
        {
            timerExpired?.Invoke();
        }

        public event Action<float> timerUpdate;
        public void OnTimerUpdate(float timeDelta)
        {
            timerUpdate?.Invoke(timeDelta);
        }
    }
}