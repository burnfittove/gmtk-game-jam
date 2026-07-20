using UnityEngine.InputSystem;

public class PlayerHealthComponent : HealthComponent
{
    private void Start()
    {
        // Initialize health
        SetHealth(maxHealth);
    }
}
