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
    }
}