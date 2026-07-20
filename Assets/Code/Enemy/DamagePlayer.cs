using System;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Bullet's box collider only check the player collision layer, so the health component is almost guaranteed.
        other.TryGetComponent(out HealthComponent playerHealthComponent);
        if (!playerHealthComponent) return;
        playerHealthComponent.TakeDamage(damage);
    }

    private void Update()
    {
        transform.transform.Translate(Vector2.left* Time.deltaTime);
    }
}
