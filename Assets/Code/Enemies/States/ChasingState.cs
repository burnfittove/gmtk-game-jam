namespace Code.Enemies.States
{
    public class ChasingState : BaseState
    {
        private readonly float movementSpeed;
        
        public ChasingState(EnemyController _ec, EnemyStateController _esc, float _movementSpeed) : base(_ec, _esc)
        {
            movementSpeed = _movementSpeed;
        }

        public override void OnEnterState()
        {
            return;
        }

        public override void OnUpdateState()
        {
            ChangeState();
            ec.MoveEnemy(ec.GetPlayerPosition(), movementSpeed);
        }

        public override void OnExitState()
        {
            return;
        }

        public override void ChangeState()
        {
            if (!ec.IsPlayerInRange()) esc.UpdateState(esc.roamingState);
        }
    }
}