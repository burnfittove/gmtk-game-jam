using System;
using Code.Enemies.States;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    private EnemyController ec;
    private BaseState currentState;
    public BaseState roamingState;
    public BaseState chasingState;
    public float roamingMovementCooldownTimer;
    public float roamingMovementTimer;
    public float roamingMovementSpeed;
    public float chasingMovementSpeed;

    private void Awake()
    {
        ec = GetComponent<EnemyController>();
        roamingState = new RoamingState(ec, this, roamingMovementSpeed, roamingMovementCooldownTimer, roamingMovementTimer);
        chasingState = new ChasingState(ec, this, chasingMovementSpeed);
    }

    private void Start()
    {
        if (currentState == null) UpdateState(roamingState);
    }

    private void Update()
    {
        currentState?.OnUpdateState();
        Debug.Log(currentState);
    }

    public void UpdateState(BaseState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState?.OnEnterState();
    }
}
