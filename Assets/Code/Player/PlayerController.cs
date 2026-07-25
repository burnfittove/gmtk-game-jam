using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _dustParticles;
    private Vector2 _movementDirection;
    public float speed;
    private float _speed;
    public float crowdSlowDown;
    private Vector2 _particlesStartPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _dustParticles = GetComponentInChildren<ParticleSystem>();
        _speed = speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _particlesStartPos = _dustParticles.transform.localPosition;
    
        if (!GameEventManager.instance) return;
        GameEventManager.instance.inputEvents.Move += Move;
        GameEventManager.instance.miscellaneousEvents.SlowDown += SlowDown;
        GameEventManager.instance.miscellaneousEvents.SpeedUp += ResetSpeed;
        GameEventManager.instance.timerEvents.timerExpired += DisableMovementOnTimerExpiry;
        GameEventManager.instance.sceneEvents.LoadScene += _ => DisableMovementOnTimerExpiry();   
    }

    private void FixedUpdate()
    {
        if (_movementDirection != Vector2.zero) _rb.MovePosition(_rb.position + _movementDirection * (_speed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        _animator.SetBool("isWalking", _movementDirection.magnitude > 0);

        if (_movementDirection.magnitude <= 0)
        {
            _dustParticles.Stop();
            return;
        }

        if (_dustParticles.isPlaying) return;
        _dustParticles.Play();
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _movementDirection = ctx.ReadValue<Vector2>();
        if (_movementDirection.x == 0) return;  // Disallow flipping if the player is moving just up or down
        FlipSprite();
    }

    public void SlowDown()
    {
        _speed = speed - crowdSlowDown;
    }

    public void ResetSpeed()
    {
        _speed = speed;
    }

    private void DisableMovementOnTimerExpiry()
    {
        _rb.linearVelocity = Vector2.zero;
        if (!GameEventManager.instance) return;
        GameEventManager.instance.inputEvents.Move -= Move;
    }

    private void FlipSprite()
    {
        _spriteRenderer.flipX = _movementDirection.x < 0;
        
        if (_movementDirection.x > 0)
        {
            _dustParticles.transform.position = (Vector2)transform.position + _particlesStartPos;
            _dustParticles.transform.rotation = Quaternion.identity;
        }
        else
        {
            _dustParticles.transform.position = (Vector2)transform.position - new Vector2(_particlesStartPos.x, -_particlesStartPos.y);
            _dustParticles.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
