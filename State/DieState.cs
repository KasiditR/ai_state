public class DieState : BaseState
{
    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);
        sc.SetTarget(null);
        sc.ResetPath();
        sc.SetEnableAgent(false);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override BaseState GetNextState()
    {
        return base.GetNextState();
    }
}
