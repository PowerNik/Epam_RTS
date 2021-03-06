﻿using System;
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

	public StructureFactory playerFactory { get; set; }

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
		Vector3 citBase = GameManager.GetGameManager().MapManagerInstance.GetCitizenBasePoint();


		FlamerUnitFactory flameruf = new FlamerUnitFactory(this);
		playerUnits.Add(flameruf.CreateUnit(citBase + new Vector3(-8, 0, -8)));

		FlamerUnitFactory flameruf1 = new FlamerUnitFactory(this);
		playerUnits.Add(flameruf1.CreateUnit(citBase + new Vector3(-9, 0, -8)));


		FootSoldierUnitFactory fsuf = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf.CreateUnit(citBase + new Vector3(7, 0, 7)));

		FootSoldierUnitFactory fsuf1 = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf1.CreateUnit(citBase + new Vector3(7, 0, -7)));

		FootSoldierUnitFactory fsuf2 = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf2.CreateUnit(citBase + new Vector3(-7, 0, 7)));

		FootSoldierUnitFactory fsuf11 = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf11.CreateUnit(citBase + new Vector3(5, 0, 5)));

		FootSoldierUnitFactory fsuf12 = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf12.CreateUnit(citBase + new Vector3(5, 0, -5)));

		FootSoldierUnitFactory fsuf13 = new FootSoldierUnitFactory(this);
		playerUnits.Add(fsuf13.CreateUnit(citBase + new Vector3(-5, 0, 5)));

		int i = 0;
		foreach (var item in GameManager.GetGameManager().MapManagerInstance.GetPeacefulNeutralPoitns())
		{
			RhiroUnitFactory ruf = new RhiroUnitFactory(this);
			playerUnits.Add(ruf.CreateUnit(item + new Vector3(i * 2, 0, i * 1)));
			i++;
		}

		foreach (var item in GameManager.GetGameManager().MapManagerInstance.GetPeacefulNeutralPoitns())
		{
			RhiroUnitFactory ruf = new RhiroUnitFactory(this);
			playerUnits.Add(ruf.CreateUnit(item + new Vector3(i * (-2), 0, i * 1)));
			i++;
		}

		i = 0;
		foreach (var item in GameManager.GetGameManager().MapManagerInstance.GetAgressiveNeutralPoitns())
		{
			SpiderUnitFactory spideruf = new SpiderUnitFactory(this);
			playerUnits.Add(spideruf.CreateUnit(item + new Vector3(i * 5, 0, i * 3)));
			i++;
		}

		foreach (var item in GameManager.GetGameManager().MapManagerInstance.GetFermerBasePoints())
		{
			FootSoldierUnitFactory fsuf10 = new FootSoldierUnitFactory(this);
			playerUnits.Add(fsuf10.CreateUnit(item + new Vector3(i * 5, 0, i * 3)));
			i++;
		}

		var s = GameManager.GetGameManager().MapManagerInstance.GetFermerBasePoints()[0];
		RoverUnitFactory rovuf = new RoverUnitFactory(this);
		playerUnits.Add(rovuf.CreateUnit(s + new Vector3(10, 0, 10)));

		FlamerUnitFactory flameruf10 = new FlamerUnitFactory(this);
		playerUnits.Add(flameruf10.CreateUnit(s + new Vector3(15, 0, 15)));

		CitizenBuilderUnitFactory citizenBuilderFactory = new CitizenBuilderUnitFactory(this);
		playerUnits.Add(citizenBuilderFactory.CreateUnit(citBase + new Vector3(5, 0, 5)));	
		

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
	public void SpawnStructure(StructuresTypes type, Vector3 position)
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
