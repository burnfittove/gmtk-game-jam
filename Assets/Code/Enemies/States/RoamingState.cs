using UnityEngine;

public class RoamingState : BaseState
{
    private readonly float movementSpeed;
    private readonly float movementCooldownTimer;
    private float movementCooldownTimerBuffer;
    private readonly float movementTimer;
    private float movementTimerBuffer;
    private Vector2 randomDirection;

    public RoamingState(EnemyController _ec, EnemyStateController _esc, float _movementSpeed, float _movementCooldownTimer, float _movementTimer) : base(_ec, _esc)
    {
        movementSpeed = _movementSpeed;
        movementCooldownTimer = _movementCooldownTimer;
        movementTimer = _movementTimer;
    }

    public override void OnEnterState()
    {
        Initialize();
    }

    public override void OnUpdateState()
    {
        ChangeState();  // Check conditions
        
        movementCooldownTimerBuffer -= Time.deltaTime;  // Decrease cooldown timer
        if (movementCooldownTimerBuffer > 0) return;    // If the timer is not 0, return
        
        ec.MoveEnemy(randomDirection, movementSpeed);   // Move in the previously chosen direction
        movementTimerBuffer -= Time.deltaTime;  // Decrease the movement timer
        if (movementTimerBuffer > 0) return;    // If the timer is above 0, return and keep moving
        Initialize();    // Once it's done, reset both timers
    }

    public override void OnExitState()
    {
        return;
    }

    public override void ChangeState()
    {
        if (ec.IsPlayerInRange()) esc.UpdateState(esc.chasingState);
    }

    private void Initialize()
    {
        randomDirection = GetRandomDirection(); // Set the random direction beforehand, so that it doesn't constantly change later
        var randomCooldownBonus = Random.Range(-.2f, 1.7f); // Add/Remove anywhere from -0.2 to 1.7 seconds of extra time
        var randomBonus = Random.Range(-.2f, 1.7f); // Add/Remove anywhere from -0.2 to 1.7 seconds of extra time
        movementCooldownTimerBuffer = movementCooldownTimer + randomCooldownBonus;
        movementTimerBuffer = movementTimer + randomBonus;
    }

    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
