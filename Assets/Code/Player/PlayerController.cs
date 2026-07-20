using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _movementDirection;
    public float speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameEventManager.instance.inputEvents.Move += Move;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_movementDirection != Vector2.zero) _rb.MovePosition(_rb.position + _movementDirection * (speed * Time.deltaTime));
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _movementDirection = ctx.ReadValue<Vector2>();
    }
}
