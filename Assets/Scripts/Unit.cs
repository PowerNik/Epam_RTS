using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour {

    // Игровые переменные
    public float Health;

    public enum AttackType {Melee, Range, Siege, Area};

    public float Cost;

    public enum Fraction {Citizen, Farmer, Nature}

    Fraction fraction;
    [SerializeField]                        //убрать
    bool isEnemy;                           //убрать
    public bool IsEnemy { get { return isEnemy; } }

    // Технические переменные
    NavMeshAgent agent;
    //bool isShooting;

    // Анимация
    Animator UnitAnimator;
    string SpeedAnim = "Speed";
    string ShootingBool = "Shooting";


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        UnitAnimator = GetComponent<Animator>();
	}
    private void Update()
    {
        UnitAnimator.SetFloat(SpeedAnim, agent.velocity.magnitude);
        //UnitAnimator.SetBool(ShootingBool, isShooting);

        if (Health<=0)
        {
            UnitAnimator.Play("Death");
            Destroy(GetComponent<Unit>());
            Destroy(gameObject, 3f);
        }
    }

    private void OnDestroy()
    {
        print("i dead");
    }


}
