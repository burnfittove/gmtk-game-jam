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
    }

    private void Start()
    {
        roamingState = new RoamingState(ec, this, ec.roamingMovementSpeed, ec.movementCooldownTimer, ec.movementTimer);
        chasingState = new ChasingState(ec, this, ec.chasingMovementSpeed);
        celebratingState = new CelebratingState(ec, this);
        
        if (currentState == null) ChangeState(roamingState);
        if (!GameEventManager.instance) return;
        GameEventManager.instance.timerEvents.timerExpired += DisableStateMachine;
    }

    private void Update()
    {
        currentState?.OnUpdateState();
    }

    private void FixedUpdate()
    {
        currentState?.OnFixedUpdateState();
    }

    public void ChangeState(BaseState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState?.OnEnterState();
    }

    private void DisableStateMachine()
    {
        ChangeState(roamingState);
        enabled = false;
    }
}
