using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Race
{
    Spectator = 0,
    Citizen = 1,
    Fermer = 2,
    Nature = 3
}

#region Resource
public struct GameResources
{
}
#endregion

public class PlayerManager : MonoBehaviour
{
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

    public Race playerRace { get; set; }

    public StructureFactory playerFactory { get;set; }
    
    List<Structure> playerStructures;

    List<Unit> playerUnits;

    //Color for minimap
    private Color playerColor;
    
    public GameObject StructuresPlaceHolder { get; private set; }
    public GameObject UnitsPlaceHolder { get; private set; }

    public Vector3[] startPoints { get; set; }

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

    void Start()
    {
        //TODO.Create scriptable object or json,to store init resources - Each race has own pre-set
        #region InitResource_TODELETE

        FoodResource = 500;
        EquipResource = 500;
        SpecialResource = 500;

        #endregion

        #region RegionForTestingSpawnUnityWithFactory
        RhiroUnitFactory ruf = new RhiroUnitFactory(this);
        playerUnits.Add(ruf.CreateUnit(new Vector3(10, 0, 27)));
        RoverUnitFactory rovuf = new RoverUnitFactory(this);
        playerUnits.Add(rovuf.CreateUnit(new Vector3(18, 0, 27)));
        FootSoldierUnitFactory fsuf = new FootSoldierUnitFactory(this);
        playerUnits.Add(fsuf.CreateUnit(new Vector3(14, 0, 27)));
        FlamerUnitFactory flameruf = new FlamerUnitFactory(this);
        playerUnits.Add(flameruf.CreateUnit(new Vector3(12, 0, 29)));
        SpiderUnitFactory spideruf = new SpiderUnitFactory(this);
        playerUnits.Add(spideruf.CreateUnit(new Vector3(20, 0, 31)));
        CitizenBuilderUnitFactory citizenBuilderFactory = new CitizenBuilderUnitFactory(this);
        playerUnits.Add(citizenBuilderFactory.CreateUnit(new Vector3(20, 0, 30)));
        #endregion
    }
    #endregion


    //TODO.Better to create subclass FermerManager and CitizenManager to resolve ugly if desicion.
    #region Init
    public void Init()
    {
        switch (playerRace)
        {
            case Race.Citizen:
                InitCitizen();
                break;
            case Race.Fermer:
                InitFermer();
                break;
            default:
                Debug.Log("Not supported");
                break;
        }
    }

    private void InitCitizen()
    {
        //playerFactory.SpawnBaseStructure(startPoints[0]);
        CitizenBuilderUnitFactory citizenBuilderFactory = new CitizenBuilderUnitFactory(this);
        playerUnits.Add(citizenBuilderFactory.CreateUnit(startPoints[0]));
        playerUnits.Last().transform.SetParent(UnitsPlaceHolder.transform);
    }

    private void InitFermer()
    {
        //foreach (Vector3 point in startPoints)
        //{
        //    playerFactory.SpawnBaseStructure(point);
        //}
        FermerBuilderUnitFactory fermerBuilderFactory = new FermerBuilderUnitFactory(this);
        foreach (Vector3 point in startPoints)
        {
            playerUnits.Add(fermerBuilderFactory.CreateUnit(point));
            playerUnits.Last().transform.SetParent(UnitsPlaceHolder.transform);
        }
    }
    #endregion

    #region SpawnStructures
    public void SpawnStructure(StructuresTypes type,Vector3 position)
    {
        switch (type)
        {
            case StructuresTypes.BaseStructure:
                playerStructures.Add(playerFactory.SpawnBaseStructure(position));
                break;
            case StructuresTypes.ExtractStucture:
                break;
            case StructuresTypes.ScientificStructure:
                break;
            case StructuresTypes.MilitaryStructure:
                break;
        }
    }
    #endregion
}
