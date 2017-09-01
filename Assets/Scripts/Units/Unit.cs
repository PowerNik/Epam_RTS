
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour {

    private UnitSettings settings;
    public UnitSettings Settings { get { return settings; } set { settings = value; } }

    public PlayerManager playerOwner { get; set; }

    private float health;
    public float Health { get { if (health <= 0)
            {
                if (GetComponent<Animator>() != null)
                    unitAnimator.Play("Death");
                if (GetComponent<Attack>() != null)
                {
                    Destroy(GetComponent<Attack>());
                }
                if (GetComponent<Movable>() != null)
                {
                    Destroy(GetComponent<Movable>());
                }
                Destroy(gameObject, 3f);
            } return health; } set { health = value; } }

    public enum CurrentAction
    {
        Attacking, MovingToTarget, DoingNothing, Building
    }
    public CurrentAction currentAction;

    public Queue<UnityAction> ActionsQueue = new Queue<UnityAction>();

    [SerializeField]                        //убрать
    bool isEnemy;                           //убрать
    public bool IsEnemy { get { return isEnemy; } }

    // Анимация

    private Animator unitAnimator;
    public Animator UnitAnimator { get { return unitAnimator; } }

    protected void Start () {
        if(GetComponent<Animator>()!=null)
            unitAnimator = GetComponent<Animator>();

        currentAction = CurrentAction.DoingNothing;

        health = settings.MaxHealth;
	}

    protected void Update()
    {
        if (ActionsQueue.Count > 0)
                if (currentAction == CurrentAction.DoingNothing)
                    ActionsQueue.Dequeue().Invoke();
    }
    private void OnDestroy()
    {
        GetComponent<Selectable>().Deselect();
    }
}


public enum AttackType { Melee, Range, Siege, Area };

public enum Fraction { Citizen, Farmer, Nature }

public enum UnitType
{
    FootSoldier_Basic = 0,
    FootSoldier_Flamer = 1,
    Rover = 2,
    Rhiro = 3,
    CitizenBuilder = 4,
    FermerBuilder = 5
}
