public abstract class BaseState
{
    protected BaseState(EnemyController _ec, EnemyStateController _esc)
    {
        ec = _ec;
        esc = _esc;
    }

    protected EnemyController ec;
    protected EnemyStateController esc;
    
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnExitState();
    public abstract void CheckStateChange();
}
