using System;

namespace Code.Events
{
    public class MiscellaneousEvents
    {
        public event Action QuitGame;
        public void OnQuitGame()
        {
            QuitGame?.Invoke();
        }

        public event Action SlowDown;

        public void OnSlowDown()
        {
            SlowDown?.Invoke();
        }
        
        public event Action SpeedUp;

        public void OnSpeedUp()
        {
            SpeedUp?.Invoke();
        }
    }
}