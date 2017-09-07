using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaceNeutralAI : MonoBehaviour
{

	[SerializeField]
	private float defaultAgroRadius = 10;
	private float curArgoRadius;

	private Vector3 mainTarget;
	private bool isDestination = false;

	private Attack attack;
	private Movable movable;
	private Unit myUnit;
	private bool findEnemy;

	private float timer = 0.5f;
	private float curTime = 0;
	private float previousHealth;

	private void Start()
	{
		MapManager mapManager = GameManager.GetGameManager().MapManagerInstance;
		mainTarget = transform.position;

		attack = GetComponent<Attack>();
		movable = GetComponent<Movable>();
		myUnit = GetComponent<Unit>();

		previousHealth = myUnit.Health;
		curTime = timer;
		curArgoRadius = defaultAgroRadius;
	}

	private void Update()
	{
		curTime -= Time.deltaTime;
		if (curTime <= 0)
		{
			curTime = timer;
			FindAttacker();
			MoveToTarget();
		}
	}

	private void FindAttacker()
	{
		if (attack.isAttacking)
		{ return; }

		if (findEnemy == false)
			if (previousHealth == myUnit.Health)
			{ return; }

		previousHealth = myUnit.Health;

		foreach (Collider item in Physics.OverlapSphere(transform.position, curArgoRadius))
		{
			if (item.gameObject == gameObject)
			{ continue; }

			Unit unit = item.gameObject.GetComponent<Unit>();
			if (unit == null)
			{ continue; }

			if (unit.IsEnemy == false)
			{
				findEnemy = true;
				attack.EnableAttack(unit);
				return;
			}
		}

		findEnemy = false;
		curArgoRadius *= 2f;
	}

	private void MoveToTarget()
	{
		if ((mainTarget - transform.position).magnitude < defaultAgroRadius)
		{ isDestination = true; }
		else { isDestination = false; }

		if (isDestination)
		{ return; }

		if (findEnemy)
		{ return; }

		curArgoRadius = defaultAgroRadius;
		movable.MoveToTarget(mainTarget);
	}
}
