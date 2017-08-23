
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

    // Игровые переменные
    private float health;
    private float cost;
    private Fraction fraction;
    private bool isLoadable;
    private Sprite icon;
    private string name;

    //public List<UnityAction> AvaliableActions = new List<UnityAction>();

    [SerializeField]                        //убрать
    bool isEnemy;                           //убрать
    public bool IsEnemy { get { return isEnemy; } }

    // Анимация

    private Animator unitAnimator;
    public Animator UnitAnimator { get { return unitAnimator; } }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float Cost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value;
        }
    }

    public Fraction Fraction
    {
        get
        {
            return fraction;
        }

        set
        {
            fraction = value;
        }
    }

    public bool IsLoadable
    {
        get
        {
            return isLoadable;
        }

        set
        {
            isLoadable = value;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }



    // Use this for initialization
    protected void Start () {
        if(GetComponent<Animator>()!=null)
            unitAnimator = GetComponent<Animator>();
	}
    protected void Update()
    {
        if (health<=0)
        {
            if (GetComponent<Animator>() != null)
                unitAnimator.Play("Death");
            if (GetComponent<Attack>()!=null)
            {
                Destroy(GetComponent<Attack>());
            }
            if (GetComponent<Movable>()!=null)
            {
                Destroy(GetComponent<Movable>());
            }
            Destroy(gameObject, 3f);
        }
    }

    private void OnDestroy()
    {
    }


}


public enum AttackType { Melee, Range, Siege, Area };

public enum Fraction { Citizen, Farmer, Nature }

public enum UnitType
{
    FootSoldier_Basic = 0,
    FootSoldier_Flamer = 1,
    Rover = 2,
    Rhiro = 3
}