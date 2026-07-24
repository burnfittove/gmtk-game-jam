using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementDirection;
    public float speed;
    private float _speed;
    public float crowdSlowDown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.inputEvents.Move += Move;
        GameEventManager.instance.miscellaneousEvents.SlowDown += SlowDown;
        GameEventManager.instance.miscellaneousEvents.SpeedUp += ResetSpeed;
    }

    private void FixedUpdate()
    {
        if (_movementDirection != Vector2.zero) _rb.MovePosition(_rb.position + _movementDirection * (_speed * Time.fixedDeltaTime));
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _movementDirection = ctx.ReadValue<Vector2>();
    }

    public void SlowDown()
    {
        _speed = speed - crowdSlowDown;
    }

    public void ResetSpeed()
    {
        _speed = speed;
    }
}
