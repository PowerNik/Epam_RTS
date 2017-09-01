using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movable : MonoBehaviour {

    [SerializeField]
    float Speed;

    NavMeshAgent agent;

    bool isMoving;

    public bool ApproachingContainer;
    public UnitContainer Container;

    float StoppingDistance = 1f;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
	}

	void Update () {
        if (GetComponent<Unit>()!=null)
        GetComponent<Unit>().UnitAnimator.SetFloat("Speed", agent.velocity.magnitude);
        if (isMoving && Vector3.Distance(agent.destination, transform.position) < StoppingDistance)
        {
            agent.isStopped = true;
            isMoving = false;
            GetComponent<Unit>().currentAction = Unit.CurrentAction.DoingNothing;
        }

        if (ApproachingContainer && Vector3.Distance(Container.transform.position, transform.position) < 2)
        {
            Container.LoadUnit(GetComponent<Unit>());
            Container = null;
            ApproachingContainer = false;
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        if (GetComponent<Attack>() != null && GetComponent<Attack>().isAttacking == true)
            GetComponent<Attack>().isAttacking = false;
        agent.isStopped = false;
        agent.SetDestination(target);
        isMoving = true;
        GetComponent<Unit>().currentAction = Unit.CurrentAction.MovingToTarget;
    }

    public void ApproachContainer(UnitContainer container)
    {
        MoveToTarget(container.transform.position);
        ApproachingContainer = true;
        Container = container;
    }
}
