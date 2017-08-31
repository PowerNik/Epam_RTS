using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSettings {

    // Игровые переменные
    [SerializeField]
    private string name;
    [SerializeField]
    private UnitType type;
    [SerializeField]
    private float health;
    [SerializeField]
    private float cost;
    [SerializeField]
    private Fraction fraction;
    [SerializeField]
    private bool isLoadable;
    [SerializeField]
    private Sprite icon;
    
    [SerializeField]
    private GameObject prefab;

    //public List<UnityAction> AvaliableActions = new List<UnityAction>();

    [SerializeField]                        //убрать
    bool isEnemy;                           //убрать
    public bool IsEnemy { get { return isEnemy; } }

    // Анимация

    private Animator unitAnimator;
    public Animator UnitAnimator { get { return unitAnimator; } }

    public UnitType Type
    {
        get
        {
            return type;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
    }

    public float Cost
    {
        get
        {
            return cost;
        }
    }

    public Fraction Fraction
    {
        get
        {
            return fraction;
        }
    }

    public bool IsLoadable
    {
        get
        {
            return isLoadable;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
}
