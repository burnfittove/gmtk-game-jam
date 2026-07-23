using System;
using Code.Enemies.States;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    private EnemyController ec;
    public BaseState currentState;
    public BaseState roamingState;
    public BaseState chasingState;
    public BaseState celebratingState;
    public float roamingMovementCooldownTimer;
    public float roamingMovementTimer;
    public float roamingMovementSpeed;
    public float chasingMovementSpeed;

    private void Awake()
    {
        ec = GetComponent<EnemyController>();
        roamingState = new RoamingState(ec, this, roamingMovementSpeed, roamingMovementCooldownTimer, roamingMovementTimer);
        chasingState = new ChasingState(ec, this, chasingMovementSpeed);
        celebratingState = new CelebratingState(ec, this);
    }

    private void Start()
    {
        if (currentState == null) ChangeState(roamingState);
    }

    private void Update()
    {
        currentState?.OnUpdateState();
        Debug.Log(currentState?.GetType());
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState?.OnEnterState();
    }
}
