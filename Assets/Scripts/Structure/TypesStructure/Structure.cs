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
public class Structure
{
    [SerializeField]
    protected string structureName;

    [SerializeField]
    protected StructuresTypes structureType;

    [SerializeField]
    protected Race structureRace;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected float timeToBuild;

    protected GameObject structureGameObject;

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

}
