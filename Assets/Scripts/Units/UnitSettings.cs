using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSettings {

    // Игровые переменные
    [SerializeField]
    private UnitType type;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float cost;
    [SerializeField]
    private Fraction fraction;
    [SerializeField]
    private bool isLoadable;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string unitName;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private string[] AvaliableActions;


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

    public float MaxHealth
    {
        get
        {
            return maxHealth;
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
            return unitName;
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
