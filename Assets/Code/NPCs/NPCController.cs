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
    private float _movementCooldownTimerBuffer;
    private float _movementTimerBuffer;
    private Vector2 _randomDirection;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    public ParticleSystem _particleSystem;
    private Vector2 _particlesStartPos;

    private void Awake()
    {
        if (_particleSystem) _particlesStartPos = _particleSystem.transform.localPosition;
        
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Initialize();
        var chanceToMoveImmediately = Random.Range(0f, 1f);
        if (chanceToMoveImmediately < .5f) _movementCooldownTimerBuffer = 0;
    }
    
    private void Update()
    {
        _movementCooldownTimerBuffer -= Time.deltaTime;  // Decrease cooldown timer
        if (_movementCooldownTimerBuffer > 0) return;    // If the timer is not 0, return
        
        _movementTimerBuffer -= Time.deltaTime;  // Decrease the movement timer
        _animator.SetBool("isWalking", true);
        FlipSprite();
        if (_movementTimerBuffer > 0) return;    // If the timer is above 0, return and keep moving
        Initialize();    // Once it's done, reset both timers
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = _randomDirection.x < 0;

        if (!_particleSystem) return;
        if (_randomDirection.x > 0)
        {
            _particleSystem.transform.position = (Vector2)transform.position + _particlesStartPos;
            _particleSystem.transform.rotation = Quaternion.identity;
        }
        else
        {
            _particleSystem.transform.position = (Vector2)transform.position - new Vector2(_particlesStartPos.x, -_particlesStartPos.y);
            _particleSystem.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        if (_movementCooldownTimerBuffer > 0) return;
        _rb.MovePosition((Vector2)transform.position + _randomDirection * (movementSpeed * Time.fixedDeltaTime));
    }

    private void Initialize()
    {         
        _animator.SetBool("isWalking", false);
        
        _randomDirection = GetRandomDirection(); // Set the random direction beforehand, so that it doesn't constantly change later
        var randomCooldownBonus = Random.Range(randomAddedRange.x, randomAddedRange.y); // Add Remove anywhere from range
        var randomBonus = Random.Range(randomAddedRange.x, randomAddedRange.y); // Add/Remove anywhere from range
        _movementCooldownTimerBuffer = movementCooldownTimer + randomCooldownBonus;
        _movementTimerBuffer = movementTimer + randomBonus;
    }
    
    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _movementTimerBuffer = 0;
    }
}
