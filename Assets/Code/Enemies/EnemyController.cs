using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    private EnemyStateController _esc;
    public float detectionRadius;
    public LayerMask detectionMask;
    public UnityEvent OnTouchPlayer;
    private Rigidbody2D _rb;
    private Collider2D[] _colliders;

    private void Awake()
    {
        _esc = GetComponent<EnemyStateController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
       _colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionMask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return; // If not player, return
        OnTouchPlayer?.Invoke();
    }

    public void MoveEnemy(Vector2 direction, float movementSpeed)
    {
        _rb.MovePosition(_rb.position + direction.normalized * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetPlayerPosition()
    {
        if (_colliders.Length == 0) return Vector2.zero;
        return _colliders[0].transform.position - transform.position;
    }

    public bool IsPlayerInRange()
    {
        return _colliders.Length > 0;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
