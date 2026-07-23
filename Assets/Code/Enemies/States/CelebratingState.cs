using UnityEngine;

public class CelebratingState : BaseState
{
    private float timer = 2;
    private float timerBuffer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CelebratingState(EnemyController _ec, EnemyStateController _esc) : base(_ec, _esc)
    {
    }

    public override void OnEnterState()
    {
        timerBuffer = timer;
    }

    public override void OnUpdateState()
    {
        CheckStateChange();
        timerBuffer -= Time.deltaTime;
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
        if (timerBuffer <= 0) esc.ChangeState(esc.roamingState);
    }
}
