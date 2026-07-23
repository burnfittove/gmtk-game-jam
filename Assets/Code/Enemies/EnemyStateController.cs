using Code.Enemies.States;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyStateController : MonoBehaviour
{
    private EnemyController ec;
    public BaseState currentState;
    public BaseState roamingState;
    public BaseState chasingState;
    public BaseState celebratingState;

    private void Awake()
    {
        ec = GetComponent<EnemyController>();
        roamingState = new RoamingState(ec, this, ec.roamingMovementSpeed, ec.movementCooldownTimer, ec.movementTimer);
        chasingState = new ChasingState(ec, this, ec.chasingMovementSpeed);
        celebratingState = new CelebratingState(ec, this);
    }

    private void Start()
    {
        if (currentState == null) ChangeState(roamingState);
    }

    private void Update()
    {
        currentState?.OnUpdateState();
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState?.OnEnterState();
    }
}
