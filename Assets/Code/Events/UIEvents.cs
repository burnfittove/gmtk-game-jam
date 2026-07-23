using System;
using UnityEngine;

namespace Code.Events
{
    public class UIEvents
    {
        public event Action<SpriteRenderer> AddSouvenirToList;
        public void OnAddSouvenirToList(SpriteRenderer sr)
        {
            AddSouvenirToList?.Invoke(sr);
        }
        
        public event Action<SpriteRenderer> RemoveSouvenirFromList;
        public void OnRemoveSouvenirFromList(SpriteRenderer sr)
        {
            RemoveSouvenirFromList?.Invoke(sr);
        }
    }
}