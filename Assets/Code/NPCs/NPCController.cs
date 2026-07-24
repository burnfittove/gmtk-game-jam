using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
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
    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Initialize();
    }
    
    private void Update()
    {
        rb.linearVelocity = Vector2.zero;
        
        movementCooldownTimerBuffer -= Time.deltaTime;  // Decrease cooldown timer
        if (movementCooldownTimerBuffer > 0) return;    // If the timer is not 0, return
        
        movementTimerBuffer -= Time.deltaTime;  // Decrease the movement timer
        animator.SetBool("isWalking", true);
        if (movementTimerBuffer > 0) return;    // If the timer is above 0, return and keep moving
        Initialize();    // Once it's done, reset both timers
    }

    private void FixedUpdate()
    {
        if (movementCooldownTimerBuffer > 0) return;
        rb.MovePosition((Vector2)transform.position + randomDirection * (movementSpeed * Time.fixedDeltaTime));
    }

    private void Initialize()
    {         
        animator.SetBool("isWalking", false);
        
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
