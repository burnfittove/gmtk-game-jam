using System;
using UnityEngine;
using UnityEngine.Events;

public class Puddle : MonoBehaviour
{
    public UnityEvent OnContact;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        OnContact?.Invoke();
    }
}
