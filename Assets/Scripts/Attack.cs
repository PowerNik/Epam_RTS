using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{

    public enum AttackType { Melee, Range, Area, Siege };
    [SerializeField]
    AttackType attackType;
    [SerializeField]
    float CurrentAttackRadius;
    [SerializeField]
    float CurrentDPS;
    public float currentDPS { get { return CurrentDPS; } }
    [SerializeField]
    float RangeAttackRadius;
    [SerializeField]
    float MeleeAttackRadius;
    [SerializeField]
    float AreaAttackRadius;
    [SerializeField]
    float SiegeAttackRadius;
    [SerializeField]
    float MeleeDPS;
    [SerializeField]
    float RangeDPS;
    [SerializeField]
    float AreaDPS;
    [SerializeField]
    float SiegeDPS;



    public bool isAttacking = false;
    private bool approachingTarget = false;
    public Unit enemy;

    float timeWaited = 0;

    public void EnableAttack(Unit enemy)
    {
        this.enemy = enemy;
        if (GetComponent<Movable>() != null && Vector3.Distance(transform.position, enemy.transform.position) > CurrentAttackRadius)
        {
            GetComponent<Movable>().MoveToTarget(enemy.transform.position);
            approachingTarget = true;
            print(Vector3.Distance(transform.position, enemy.transform.position));
            return;
        }
        if (Vector3.Distance(transform.position, enemy.transform.position) <= CurrentAttackRadius)
            isAttacking = true;

    }
    public void DisableAttack()
    {
        isAttacking = false;
    }
    public void SwitchAttackType(AttackType attackType)
    {
        this.attackType = attackType;
        switch (attackType)
        {
            case AttackType.Range:
                CurrentAttackRadius = RangeAttackRadius;
                CurrentDPS = RangeDPS;
                break;
            case AttackType.Melee:
                CurrentAttackRadius = MeleeAttackRadius;
                CurrentDPS = MeleeDPS;
                break;
            case AttackType.Area:
                CurrentAttackRadius = AreaAttackRadius;
                CurrentDPS = AreaDPS;
                break;
            case AttackType.Siege:
                CurrentAttackRadius = SiegeAttackRadius;
                CurrentDPS = SiegeDPS;
                break;
        }
    }
    private void Update()
    {
        if (enemy == null)
            DisableAttack();

        if (approachingTarget)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > CurrentAttackRadius)
                return;
            else
            {
                approachingTarget = false;
                isAttacking = true;
                timeWaited = 1f;
            }
        }
        if (isAttacking)
        {
            GetComponent<Unit>().currentAction = Unit.CurrentAction.Attacking;
            if (GetComponent<NavMeshAgent>() != null)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                transform.LookAt(enemy.transform);
                transform.Rotate(Vector3.right, transform.rotation.eulerAngles.x);
            }
            if (enemy.Health <= 0)
            {
                isAttacking = false;
                GetComponent<Unit>().currentAction = Unit.CurrentAction.DoingNothing;
                return;
            }
            switch (attackType)
            {

                case AttackType.Range:
                    GetComponent<Animator>().SetBool(attackType.ToString(), true);

                    if (timeWaited >= 1f)
                    { 
                        print(attackType.ToString());
                        enemy.Health -= CurrentDPS;
                        timeWaited = 0;
                    }
                    else
                        timeWaited += Time.deltaTime;

                    break;

                case AttackType.Melee:
                    GetComponent<Animator>().SetBool(attackType.ToString(), true);
                    if (timeWaited < 1)
                    {
                        timeWaited += Time.deltaTime;
                    }
                    else
                    {
                        print(attackType.ToString());
                        enemy.Health -= CurrentDPS;
                        timeWaited = 0;
                    }
                    break;
                case AttackType.Area:
                    GetComponent<Animator>().SetBool(attackType.ToString(), true);
                    EnableFlameThrower();
                    print(attackType.ToString());
                    break;
                case AttackType.Siege:
                    break;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool(attackType.ToString(), false);
            if (GetComponentInChildren<Flamethrower>() != null)
                DisableFlameThrower();
        }
    }
    void EnableFlameThrower()
    {
        if (GetComponentInChildren<Flamethrower>() != null)
            GetComponentInChildren<Flamethrower>().Enable();
    }
    void DisableFlameThrower()
    {
            GetComponentInChildren<Flamethrower>().Disable();
    }
}

