using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class StateController : MonoBehaviour
{
    private Transform player;
    [SerializeField] protected NavMeshAgent agent;
    private Dictionary<string, BaseState> states = new Dictionary<string, BaseState>();
    protected BaseState currentState;
    protected bool isTransition;
    private bool isStopState;
    private const float DEFAULT_DISTANCE = 2;
    private const float ROTATE_SPEED = 5;
    private NavMeshHit navHit;

    public bool IsStopState
    {
        get => isStopState;
        set
        {
            isStopState = value;
            if (value)
            {
                ChangeState(states[StateKey.IDLE]);
            }
        }
    }

    public void Initialize(Transform player)
    {
        states.Clear();
        isStopState = false;
        agent = GetComponent<NavMeshAgent>();
        if (states.Count == 0)
        {
            InitializeState(states);
        }

        SetTarget(player);
        currentState = states[StateKey.IDLE];
        ChangeState(currentState);
        SetEnableAgent(true);
    }

    private void Update()
    {
        if (player == null || currentState == null || isStopState)
        {
            return;
        }

        BaseState nextState = currentState.GetNextState();
        if (!isTransition && currentState.Equals(nextState))
        {
            currentState.OnUpdate();
        }
        else if (!isTransition)
        {
            ChangeState(nextState);
        }
    }

    public void ChangeState(BaseState newState)
    {
        isTransition = true;
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter(this);
        isTransition = false;

    }
    public BaseState GetState(string stateKey)
    {
        return states[stateKey];
    }

    protected abstract void InitializeState(Dictionary<string, BaseState> states);

    public Transform GetTarget()
    {
        return player.transform;
    }

    public void SetTarget(Transform value)
    {
        player = value;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public void SetVelocity(Vector3 vector)
    {
        if (agent.enabled)
        {
            agent.velocity = vector;
        }
    }

    public void ToggleAgent()
    {
        SetEnableAgent(!agent.enabled);
    }

    public void SetEnableAgent(bool value)
    {
        agent.enabled = value;
    }

    public void ResetPath()
    {
        if (agent.enabled && gameObject.activeSelf)
        {
            agent.ResetPath();
        }
    }

    public Vector3 GetTargetPosition()
    {
        return player.transform.position;
    }

    public float GetDistance()
    {
        if (agent == null || player == null)
        {
            return float.NaN;
        }

        return GetDistance(GetTargetPosition());
    }

    public float GetDistance(Vector3 targetPos)
    {
        return Vector3.Distance(agent.transform.position, targetPos);
    }

    public void MoveToTarget(Vector3 targetPosition, float stopDistance = -1)
    {
        if (!agent.enabled || !gameObject.activeSelf)
        {
            return;
        }

        if (NavMesh.SamplePosition(targetPosition, out navHit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
        }
        else
        {
            Debug.LogError("Destination is not on the NavMesh!");
        }

        agent.stoppingDistance = stopDistance == -1 ? DEFAULT_DISTANCE : stopDistance;
    }

    public void MoveToTarget(float stopDistance = -1)
    {
        MoveToTarget(GetTargetPosition(), stopDistance);
    }

    public void LookAtTarget()
    {
        LookAtTarget(GetTargetPosition());
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 targetDirection = target - agent.transform.position;
        if (targetDirection != Vector3.zero && targetDirection.sqrMagnitude > 0)
        {
            targetDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, ROTATE_SPEED * Time.deltaTime);
        }
    }

    public void LookAtTargetImmediate()
    {
        Vector3 targetDirection = GetTargetPosition() - agent.transform.position;
        if (targetDirection != Vector3.zero && targetDirection.sqrMagnitude > 0)
        {
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            agent.transform.rotation = targetRotation;
        }
    }

}
