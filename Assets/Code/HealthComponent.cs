using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth;
    protected float _health;

    /// <summary>
    /// Set health to value.
    /// </summary>
    /// <param name="health">Value to set health to.</param>
    public virtual void SetHealth(float health)
    {
        _health = Mathf.Clamp(health, 0, maxHealth);
    }

    /// <summary>
    /// Updates health by value.
    /// </summary>
    /// <param name="healthDelta">Health change. Positive by default; increases health.</param>
    public virtual void UpdateHealth(float healthDelta)
    {
        _health = Mathf.Clamp(_health + healthDelta, 0, maxHealth);

        if (_health <= 0) Die();
    }


    /// <summary>
    /// Modified UpdateHealth. Removes health by default.
    /// </summary>
    /// <param name="damage">Amount of health to remove.</param>
    public virtual void TakeDamage(float damage)
    {
        UpdateHealth(-damage);
    }
    
    /// <summary>
    /// Should trigger when the component owner's health reaches zero.
    /// </summary>
    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
