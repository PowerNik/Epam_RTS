using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePointSettings : IMainPointSettings
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private Material citizenBasePointMaterial;

	[SerializeField]
	private DomainSettings citizenDomainSets;

	[Space(10)]
	[SerializeField]
	private int fermerBaseCount;

	[SerializeField]
	private Material fermerBasePointMaterial;

	[SerializeField]
	private DomainSettings fermerDomainSets;


	public void SetMainSeed(string mainSeed)
	{
		this.mainSeed = mainSeed;
	}

	public string GetSeed()
	{
		if (seed != "")
		{
			return seed;
		}
		else
		{
			return mainSeed;
		}
	}

	public bool GetIsCenter()
	{
		return isCenter;
	}

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		Dictionary<TileType, Tile> tileDict = new Dictionary<TileType, Tile>();

		MainPointTile tile = new MainPointTile(TileType.CitizenBasePoint);
		tile.SetMaterial(citizenBasePointMaterial);
		tileDict.Add(TileType.CitizenBasePoint, tile.GetTile());

		tile = new MainPointTile(TileType.FermersBasePoint);
		tile.SetMaterial(fermerBasePointMaterial);
		tileDict.Add(TileType.FermersBasePoint, tile.GetTile());

		return tileDict;
	}

	public MainPointTile GetMainPoint(Race race)
	{
		if (race == Race.Citizen)
		{
			MainPointTile tile = new MainPointTile(TileType.CitizenBasePoint);
			tile.SetMaterial(citizenBasePointMaterial);
			tile.SetDomainSettings(citizenDomainSets);

			return tile;
		}
		else
		{
			MainPointTile tile = new MainPointTile(TileType.FermersBasePoint);
			tile.SetMaterial(fermerBasePointMaterial);
			tile.SetDomainSettings(fermerDomainSets);

			return tile;
		}
	}

	public int GetMainPointCount(Race race)
	{
		if (race == Race.Citizen)
		{
			return 1;
		}
		else
		{
			return fermerBaseCount;
		}
	}
}
