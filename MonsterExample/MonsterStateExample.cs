using UnityEngine;
using System.Collections.Generic;

public class MonsterStateExample : StateController
{
    [SerializeField] private float attackRange = 0;
    private MonsterIdleStateExample idleState = new MonsterIdleStateExample();
    private MonsterMovingStateExample movingState = new MonsterMovingStateExample();
    private MonsterAttackStateExample attackState = new MonsterAttackStateExample();
    private MonsterDieStateExample dieState = new MonsterDieStateExample();

    public float AttackRange { get => attackRange; }

    private void Start()
    {
        Transform player = GameObject.Find("Player").transform;
        if (player != null)
        {
            this.Initialize(player);
        }
        else
        {
            Debug.LogError("Player Not Found");
        }
    }

    protected override void InitializeState(Dictionary<string, BaseState> states)
    {
        states.Add(StateKey.IDLE, idleState);
        states.Add(StateKey.MOVING, movingState);
        states.Add(StateKey.ATTACK, attackState);
        states.Add(StateKey.DIE, dieState);
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }
    public void Die()
    {
        Debug.Log("Die");
        this.gameObject.SetActive(false);
    }
}
