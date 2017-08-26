using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Spectator = 0,
    Citizen = 1,
    Fermer = 2,
    Nature = 3
}


public class PlayerManager : MonoBehaviour
{
    #region ResourceRegion
    private int foodResource,
        equipResource,
        specialResource;
    public int FoodResource
    {
        get { return foodResource; }
        set
        {
            foodResource += value;
            if (foodResChange != null)
            {
                foodResChange(foodResource);
            }
        }
    }

    public int EquipResource
    {
        get { return equipResource; }
        set
        {
            equipResource += value;
            if (equipResChange != null)
            {
                equipResChange(equipResource);
            }
        }
    }

    public int SpecialResource
    {
        get { return specialResource; }
        set
        {
            specialResource += value;
            if (specialResChange != null)
            {
                specialResChange(specialResource);
            }
        }
    }

    public delegate void ResourceChangeDelegate(int AddFood);

    public ResourceChangeDelegate foodResChange, equipResChange, specialResChange;
    #endregion

    public Race playerRace { get; set; }

    public StructureFactory playerFactory { get;set; }
    
    List<Structure> playerStructures;

    List<Unit> playerUnits;

    //Color for minimap
    private Color playerColor;
    
    public static GameObject StructuresPlaceHolder { get; private set; }
    public static GameObject UnitsPlaceHolder { get; private set; }

    #region MonoBehaviour
    void Awake()
    {
        playerStructures = new List<Structure>();
        playerUnits = new List<Unit>();
        StructuresPlaceHolder = new GameObject("Structures");
        StructuresPlaceHolder.transform.SetParent(transform);
        UnitsPlaceHolder = new GameObject("Units");
        UnitsPlaceHolder.transform.SetParent(transform);
    }
    #endregion

    void Start()
    {
        //TODO.Create scriptable object or json,to store init resources - Each race has own pre-set
        #region InitResource_TODELETE

        FoodResource = 500;
        EquipResource = 500;
        SpecialResource = 500;

        #endregion
    }
}
