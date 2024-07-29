public class MonsterAttackStateExample : AttackState
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

        monsterState.Attack();
    }
    public override BaseState GetNextState()
    {
        if (sc.GetDistance() > monsterState.AttackRange)
        {
            return sc.GetState(StateKey.MOVING);
        }

        return base.GetNextState();
    }
}
