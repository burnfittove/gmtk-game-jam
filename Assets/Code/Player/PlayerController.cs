using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem dustParticles;
    private Vector2 _movementDirection;
    public float speed;
    private float _speed;
    public float crowdSlowDown;
    private Vector2 particlesStartPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _speed = speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.inputEvents.Move += Move;
        GameEventManager.instance.miscellaneousEvents.SlowDown += SlowDown;
        GameEventManager.instance.miscellaneousEvents.SpeedUp += ResetSpeed;
        GameEventManager.instance.timerEvents.timerExpired += DisableMovementOnTimerExpiry;

        particlesStartPos = dustParticles.transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (_movementDirection != Vector2.zero) _rb.MovePosition(_rb.position + _movementDirection * (_speed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        _animator.SetBool("isWalking", _movementDirection.magnitude > 0);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _movementDirection = ctx.ReadValue<Vector2>();
        _spriteRenderer.flipX = _movementDirection.x < 0;

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
        GameEventManager.instance.inputEvents.Move -= Move;
    }
}
