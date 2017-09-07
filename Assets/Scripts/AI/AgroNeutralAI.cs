using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroNeutralAI : MonoBehaviour
{
	[SerializeField]
	private float agroRadius = 10;
	private Vector3 mainTarget;
	private bool isDestination = false;

	private Attack attack;
	private Movable movable;
	private Unit myUnit;
	private bool findEnemy;

	private float timer = 0.3f;
	private float curTime = 0;

	private void Start()
	{
		MapManager mapManager = GameManager.GetGameManager().MapManagerInstance;
		mainTarget = mapManager.GetCitizenBasePoint();

		attack = GetComponent<Attack>();
		movable = GetComponent<Movable>();
		myUnit = GetComponent<Unit>();
		curTime = timer;
	}

	private void Update()
	{
		curTime -= Time.deltaTime;
		if (curTime <= 0)
		{
			curTime = timer;
			FindEnemy();
			MoveToTarget();
		}
	}

	private void FindEnemy()
	{
		if (attack.isAttacking)
		{ return; }

		foreach (Collider item in Physics.OverlapSphere(transform.position, agroRadius))
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
	}

	private void MoveToTarget()
	{
		if ((mainTarget - transform.position).magnitude < 20)
		{ isDestination = true; }

		if (isDestination)
		{ return; }

		if (findEnemy)
		{ return; }
		movable.MoveToTarget(mainTarget);
	}
}
