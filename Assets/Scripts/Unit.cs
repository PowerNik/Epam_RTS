
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

    // Игровые переменные
    public float Health;

    public enum AttackType {Melee, Range, Siege, Area};

    public float Cost;

    public enum Fraction {Citizen, Farmer, Nature}

    Fraction fraction;

    public List<UnityAction> AvaliableActions = new List<UnityAction>();

    [SerializeField]                        //убрать
    bool isEnemy;                           //убрать
    public bool IsEnemy { get { return isEnemy; } }

    // Анимация

    private Animator unitAnimator;
    public Animator UnitAnimator { get { return unitAnimator; } }

    // Use this for initialization
    void Start () {
        if(GetComponent<Animator>()!=null)
        unitAnimator = GetComponent<Animator>();
	}
    private void Update()
    {
        if (Health<=0)
        {
            if (GetComponent<Animator>() != null)
                unitAnimator.Play("Death");
            Destroy(GetComponent<Unit>());
            Destroy(gameObject, 3f);
        }
    }

    private void OnDestroy()
    {
    }


}
