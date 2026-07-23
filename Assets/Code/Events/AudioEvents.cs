using System;
using UnityEngine;

namespace Code.Events
{
    public class AudioEvents
    {
        public event Action<AudioClip> Play;
        public void OnPlay(AudioClip clip)
        {
            Play?.Invoke(clip);
        }
    }
}