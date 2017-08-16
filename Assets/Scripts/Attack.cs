using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{

    public enum AttackType { Melee, Range, Area, Rocket };
    [SerializeField]
    AttackType attackType;
    [SerializeField]
    float CurrentAttackRadius;
    [SerializeField]
    float CurrentDPS;
    [SerializeField]
    float RangeAttackRadius;
    [SerializeField]
    float MeleeAttackRadius;
    [SerializeField]
    float AreaAttackRadius;
    [SerializeField]
    float RocketAttackRadius;
    [SerializeField]
    float MeleeDPS;
    [SerializeField]
    float RangeDPS;
    [SerializeField]
    float AreaDPS;
    [SerializeField]
    float RocketDPS;



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
            case AttackType.Rocket:
                CurrentAttackRadius = RocketAttackRadius;
                CurrentDPS = RocketDPS;
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
            }
        }
        if (isAttacking)
        {
            if (GetComponent<NavMeshAgent>() != null)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                // изменить transform.rotation;
            }


            switch (attackType)
            {

                case AttackType.Range:

                    if (timeWaited < 1)
                    {
                        timeWaited += Time.deltaTime;
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(attackType.ToString(), true);
                        print(attackType.ToString());
                        enemy.Health -= CurrentDPS;
                        timeWaited = 0;
                    }
                    break;

                case AttackType.Melee:
                    if (timeWaited < 1)
                    {
                        timeWaited += Time.deltaTime;
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool(attackType.ToString(), true);
                        print(attackType.ToString());
                        enemy.Health -= CurrentDPS;
                        timeWaited = 0;
                    }
                    break;
                case AttackType.Area:
                    break;
                case AttackType.Rocket:
                    break;
            }
        }
        else
        {
            GetComponent<Animator>().SetBool(attackType.ToString(), false);
        }
    }
}

