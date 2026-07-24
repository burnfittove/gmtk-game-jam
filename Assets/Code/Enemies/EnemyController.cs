using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStateController))]
public class EnemyController : MonoBehaviour
{
    private EnemyStateController _esc;
    [Header("Detection Radius")]
    public float detectionRadius;
    [HideInInspector] public float detectionRadiusBuffer;
    [Tooltip("This value divides Time.deltaTime, so the bigger it is, the smaller the decrease /s")] public int detectionRadiusDecreaseDivider;
    public LayerMask detectionMask;
    [Header("Roaming State Values")]    // Most values here and bellow are used by states
    public float roamingMovementSpeed;
    public float roamingCrowdSlowDown;
    public float movementCooldownTimer;
    public float movementTimer;
    public Vector2 randomAddedRange;
    [Header("Chasing State Values")]
    public float chasingMovementSpeed;
    public float chasingCrowdSlowDown;
    public bool chaseEmptyPlayer;
    [HideInInspector] public bool isSlowedDown;
    [Header("OnTouch Action")]
    public UnityEvent OnTouchPlayer;    // In the inspector, add a method from another script to this field
    private Rigidbody2D _rb;
    public Collider2D[] _colliders = Array.Empty<Collider2D>();
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        _esc = GetComponent<EnemyStateController>();
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        detectionRadiusBuffer = detectionRadius;
    }

    private void FixedUpdate()
    {
       _colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadiusBuffer, detectionMask);
    }

    private void Update()
    {
        if (_esc.currentState == _esc.chasingState) return;
        detectionRadiusBuffer += Time.deltaTime;
        detectionRadiusBuffer = Mathf.Clamp(detectionRadiusBuffer, detectionRadius / 2, detectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return; // If not player, return
        if (_esc.currentState !=_esc.chasingState) return;
        _esc.ChangeState(_esc.celebratingState);
        OnTouchPlayer?.Invoke();
    }

    public void MoveEnemy(Vector2 direction, float movementSpeed)
    {
        _rb.MovePosition(_rb.position + direction.normalized * (movementSpeed * Time.fixedDeltaTime));
    }

    public Vector2 GetPlayerPosition()
    {
        if (_colliders == null || _colliders.Length == 0) return Vector2.zero;
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
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadiusBuffer);
    }

    public void SlowDown()
    {
        if (_esc.currentState == _esc.celebratingState) return;
        isSlowedDown = true;
    }

    public void ResetSpeed()
    {
        isSlowedDown = false;
    }
}
