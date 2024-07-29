public class MonsterDieStateExample : DieState
{
    private MonsterStateExample monsterState;

    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);

        monsterState = stateController as MonsterStateExample;
        monsterState.Die();
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
