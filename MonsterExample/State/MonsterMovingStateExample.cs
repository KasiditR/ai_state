public class MonsterMovingStateExample : MovingState
{
    private MonsterStateExample monsterState;

    public override void OnEnter(StateController stateController)
    {
        base.OnEnter(stateController);

        monsterState = stateController as MonsterStateExample;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        monsterState.MoveToTarget(monsterState.AttackRange);
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public override BaseState GetNextState()
    {
        if (sc.GetDistance() <= monsterState.AttackRange)
        {
            return sc.GetState(StateKey.ATTACK);
        }

        return base.GetNextState();
    }
}
