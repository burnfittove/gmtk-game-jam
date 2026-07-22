using System;
using UnityEngine;
using UnityEngine.Events;

public class DebugThiefTest : MonoBehaviour
{
    public UnityEvent OnRemoveSouvenir;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        OnRemoveSouvenir?.Invoke();
    }
}
