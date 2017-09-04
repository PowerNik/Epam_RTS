using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitSettings {

    // Игровые переменные
    [SerializeField]
    private string unitName;
    [SerializeField]
    private UnitType type;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float speed;
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
    [SerializeField]
    private string[] AvaliableActions;


    //public List<UnityAction> AvaliableActions = new List<UnityAction>();

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
        set
        {
            maxHealth = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
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

        set
        {
            icon = value;
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
