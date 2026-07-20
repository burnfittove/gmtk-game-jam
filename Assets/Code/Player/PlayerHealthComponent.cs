using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthComponent : HealthComponent
{
    private void Start()
    {
        // Initialize health
        SetHealth(maxHealth);
    }

    private void Update()
    {
        Debug.Log(_health);
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame) TakeDamage(1);
    }
}
