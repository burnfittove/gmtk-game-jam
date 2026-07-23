using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCController : MonoBehaviour
{
    [Header("Roaming State Values")]    // Most values here and bellow are used by states
    public float movementSpeed;
    public float movementCooldownTimer;
    public float movementTimer;
    public Vector2 randomAddedRange;
    private float movementCooldownTimerBuffer;
    private float movementTimerBuffer;
    private Vector2 randomDirection;
    
    private void Start()
    {
        Initialize();
    }
    
    private void Update()
    {
        movementCooldownTimerBuffer -= Time.deltaTime;  // Decrease cooldown timer
        if (movementCooldownTimerBuffer > 0) return;    // If the timer is not 0, return
        
        movementTimerBuffer -= Time.deltaTime;  // Decrease the movement timer
        transform.Translate(randomDirection * (movementSpeed * Time.deltaTime), Space.World);
        if (movementTimerBuffer > 0) return;    // If the timer is above 0, return and keep moving
        Initialize();    // Once it's done, reset both timers
    }
    
    private void Initialize()
    {
        randomDirection = GetRandomDirection(); // Set the random direction beforehand, so that it doesn't constantly change later
        var randomCooldownBonus = Random.Range(randomAddedRange.x, randomAddedRange.y); // Add Remove anywhere from range
        var randomBonus = Random.Range(randomAddedRange.x, randomAddedRange.y); // Add/Remove anywhere from range
        movementCooldownTimerBuffer = movementCooldownTimer + randomCooldownBonus;
        movementTimerBuffer = movementTimer + randomBonus;
    }
    
    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        movementTimerBuffer = 0;
    }
}
