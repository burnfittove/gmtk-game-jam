using UnityEngine;

namespace Code.Enemies.States
{
    public class ChasingState : BaseState
    {
        private readonly float movementSpeed;
        private float _movementSpeedBuffer;
        
        public ChasingState(EnemyController _ec, EnemyStateController _esc, float _movementSpeed) : base(_ec, _esc)
        {
            movementSpeed = _movementSpeed;
        }

        public override void OnEnterState()
        {
            if (!ec.animator) return;
            ec.animator.SetBool("isChasing", true);
        }

        public override void OnUpdateState()
        {
            CheckStateChange();
            
            
            if (ec.isSlowedDown) _movementSpeedBuffer = movementSpeed - ec.chasingCrowdSlowDown;
            else _movementSpeedBuffer = movementSpeed;
            
            ec.detectionRadiusBuffer -= Time.deltaTime / ec.detectionRadiusDecreaseDivider;

            // if (!ec.particles) return;
            // if (!ec.loopParticles) return;
            // if (ec.particles.isPlaying) return;
            // ec.particles.Play();
        }

        public override void OnFixedUpdateState()
        {
            ec.MoveEnemy(ec.GetPlayerPosition(), _movementSpeedBuffer);
        }

        public override void OnExitState()
        {
            if (!ec.animator) return;
            ec.animator.SetBool("isChasing", false);
        }

        public override void CheckStateChange()
        {
            if (!ec.IsPlayerInRange()) esc.ChangeState(esc.roamingState);
        }
    }
}