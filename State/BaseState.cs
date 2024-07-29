public abstract class BaseState
{
    protected StateController sc;

    public virtual void OnEnter(StateController stateController)
    {
        sc = stateController;
    }
    public virtual void OnUpdate()
    {
    }
    public virtual void OnExit()
    {
    }
    public virtual BaseState GetNextState()
    {
        return this;
    }
}
