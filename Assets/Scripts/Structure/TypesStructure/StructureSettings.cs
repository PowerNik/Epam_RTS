using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StructuresTypes
{
    BaseStructure = 1,
    ExtractStucture = 2,
    MilitaryStructure = 3,
    ScientificStructure = 4
}

[System.Serializable]
public class StructureSettings
{
    [SerializeField]
    protected string structureName;

    //Problem with show properties in Editor
    //[SerializeField]
    public StructuresTypes structureType;// { get; protected set; }

    //[SerializeField]
    public Race structureRace;// { get; protected set; }

    [SerializeField]
    protected int health;

    [SerializeField]
    protected float timeToBuild;

    [SerializeField]
    protected GameResources price;

    public GameResources structurePrice
    {
        get
        {
            return price;
        }
    }

    [SerializeField]
    protected GameObject structureGameObject;

    [SerializeField]
    protected GameObject structureWhileBuilding;

    public GameObject StructureGameObject
    {
        get
        {
            return structureGameObject;
        }

        set
        {
            structureGameObject = value;
        }
    }

    //Bad architecture
    public void Init(StructureSettings settings)
    {
        this.health = settings.health;
        this.structureGameObject = settings.structureGameObject;
        this.timeToBuild = settings.timeToBuild;
        this.price = settings.price;
        this.structureName = settings.structureName;
        this.structureType = settings.structureType;
        this.structureRace = settings.structureRace;
    }
}
