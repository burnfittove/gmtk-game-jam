using UnityEngine;

namespace Code
{
    public static class Extensions
    {
        public static string TimerFormat(this float time)
        {
            Debug.Log(time.ToString());
            return time > 9 ? time.ToString() : $"0{time}";
        }

        public static string TimerMillisecondFormat(this float time)
        {
            return time switch
            {
                > 99 => time.ToString(),
                > 9 => $"0{time}",
                _ => $"00{time}"
            };
        }
    }
}