using UnityEngine;

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
            CheckStateChange();
            ec.MoveEnemy(ec.GetPlayerPosition(), movementSpeed);
            ec.detectionRadiusBuffer -= Time.deltaTime / ec.detectionRadiusDecreaseDivider;
        }

        public override void OnFixedUpdateState()
        {
            return;
        }

        public override void OnExitState()
        {
            return;
        }

        public override void CheckStateChange()
        {
            if (!ec.IsPlayerInRange()) esc.ChangeState(esc.roamingState);
        }
    }
}