using UnityEngine;

namespace Code.Enemies
{
    public class RemoveTime : MonoBehaviour
    {
        public float timeDelta;

        public void UpdateTimer()
        {
            GameEventManager.instance.timerEvents.OnTimerUpdate(-timeDelta);
        }
    }
}