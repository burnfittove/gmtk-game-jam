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
        CheckStateChange();  // Check conditions
        
        movementCooldownTimerBuffer -= Time.deltaTime;  // Decrease cooldown timer
        if (movementCooldownTimerBuffer > 0) return;    // If the timer is not 0, return
        
        if (ec.animator) ec.animator?.SetBool("isRoaming", true);
        movementTimerBuffer -= Time.deltaTime;  // Decrease the movement timer
        if (movementTimerBuffer > 0) return;    // If the timer is above 0, return and keep moving
        Initialize();    // Once it's done, reset both timers
    }

    public override void OnFixedUpdateState()
    {
        if (movementCooldownTimerBuffer > 0) return;
        ec.MoveEnemy(randomDirection, movementSpeed);   // Move in the previously chosen direction
    }

    public override void OnExitState()
    {
        return;
    }

    public override void CheckStateChange()
    {
        if (!ec.IsPlayerInRange()) return;
        if (ec.chaseEmptyPlayer || !SouvenirManager.instance.IsArrayEmpty()) esc.ChangeState(esc.chasingState); // Chase after player either if the enemy can chase after a player with an empty inventory
                                                                                                                // or the inventory isn't empty
    }

    private void Initialize()
    {
        if (ec.animator) ec.animator?.SetBool("isRoaming", false);
        randomDirection = GetRandomDirection(); // Set the random direction beforehand, so that it doesn't constantly change later
        var randomCooldownBonus = Random.Range(ec.randomAddedRange.x, ec.randomAddedRange.y); // Add/Remove anywhere from range
        var randomBonus = Random.Range(ec.randomAddedRange.x, ec.randomAddedRange.y); // Add/Remove anywhere from range
        movementCooldownTimerBuffer = movementCooldownTimer + randomCooldownBonus;
        movementTimerBuffer = movementTimer + randomBonus;
    }

    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
