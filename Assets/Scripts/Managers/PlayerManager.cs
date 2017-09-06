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

public class PlayerManager : MonoBehaviour
{
    #region OldResource_TODELETE
    //private int foodResource,
    //    equipResource,
    //    specialResource;
    //public int FoodResource
    //{
    //    get { return foodResource; }
    //    set
    //    {
    //        foodResource += value;
    //        if (foodResChange != null)
    //        {
    //            foodResChange(foodResource);
    //        }
    //    }
    //}

    //public int EquipResource
    //{
    //    get { return equipResource; }
    //    set
    //    {
    //        equipResource += value;
    //        if (equipResChange != null)
    //        {
    //            equipResChange(equipResource);
    //        }
    //    }
    //}

    //public int SpecialResource
    //{
    //    get { return specialResource; }
    //    set
    //    {
    //        specialResource += value;
    //        if (specialResChange != null)
    //        {
    //            specialResChange(specialResource);
    //        }
    //    }
    //}

    //public delegate void ResourceChangeDelegate(int AddFood);

    //public ResourceChangeDelegate foodResChange, equipResChange, specialResChange;
    #endregion

    public GameResources playerResources;

    public Race playerRace { get; set; }

    public StructureFactory playerFactory { get;set; }
    
    List<Structure> playerStructures;

    List<Unit> playerUnits;

    //Color for minimap
    private Color playerColor;
    
    public GameObject StructuresPlaceHolder { get; private set; }
    public GameObject UnitsPlaceHolder { get; private set; }

    //private Vector3[] startPoints { get; set; }

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

        #region RegionForTestingSpawnUnityWithFactory
        RhiroUnitFactory ruf = new RhiroUnitFactory(this);
        playerUnits.Add(ruf.CreateUnit(new Vector3(10, 0, 27)));
        RoverUnitFactory rovuf = new RoverUnitFactory(this);
        playerUnits.Add(rovuf.CreateUnit(new Vector3(18, 0, 27)));
        FootSoldierUnitFactory fsuf = new FootSoldierUnitFactory(this);
        playerUnits.Add(fsuf.CreateUnit(new Vector3(14, 0, 27)));
        FlamerUnitFactory flameruf = new FlamerUnitFactory(this);
        playerUnits.Add(flameruf.CreateUnit(new Vector3(12, 0, 29)));

        CitizenBuilderUnitFactory citizenBuilderFactory = new CitizenBuilderUnitFactory(this);
        playerUnits.Add(citizenBuilderFactory.CreateUnit(new Vector3(20, 0, 30)));
        #endregion
    }
    #endregion

    //TODO.Better to create subclass FermerManager and CitizenManager to resolve ugly if desicion.
    #region Init
    public void Init(Race playerRace, Vector3[] startPoints)
    {
        switch (playerRace)
        {
            case Race.Citizen:
                InitCitizen(startPoints[0]);
                break;
            case Race.Fermer:
                InitFermer(startPoints);
                break;
            default:
                Debug.Log("Not supported");
                break;
        }
    }

    private void InitCitizen(Vector3 startPoint)
    {
        //playerFactory.SpawnBaseStructure(startPoints[0]);
        CitizenBuilderUnitFactory citizenBuilderFactory = new CitizenBuilderUnitFactory(this);
        playerUnits.Add(citizenBuilderFactory.CreateUnit(startPoint));
        playerUnits.Last().transform.SetParent(UnitsPlaceHolder.transform);

        playerResources = GameManager.getStartupInitResources(Race.Citizen);

        playerFactory = new CitizenStructureFactory(this);
    }

    private void InitFermer(Vector3[] startPoints)
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
        playerResources = GameManager.getStartupInitResources(Race.Fermer);

        playerFactory = new FermersStructureFactory(this);
    }
    #endregion

    #region SpawnStructures
    public void SpawnStructure(StructuresTypes type,Vector3 position)
    {
        switch (type)
        {
            case StructuresTypes.BaseStructure:
                if (this.playerResources >= playerFactory.GetStructurePrice(type))
                {
                    this.playerResources -= playerFactory.GetStructurePrice(type);
                    playerStructures.Add(playerFactory.SpawnBaseStructure(position));
                }
                else
                {
                    GameManager.GetGameManager().PrintError("Not enough resource");
                }
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

    public void InitResourceHUD(GameObject foodRes, GameObject equipRes, GameObject specRes)
    {
        foodRes.GetComponentInChildren<ResourceHUD>().Init(ref playerResources.foodResource);
        equipRes.GetComponentInChildren<ResourceHUD>().Init(ref playerResources.equipResource);
        specRes.GetComponentInChildren<ResourceHUD>().Init(ref playerResources.specialResource);
    }
}
