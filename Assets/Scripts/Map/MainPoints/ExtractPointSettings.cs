using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExtractPointSettings : IMainPointSettings
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private int citizenExtractPointCount = 3;

	[SerializeField]
	private Material citizenExtractPointMaterial;

	[SerializeField]
	private DomainSettings citizenDomainSets;

	[Space(10)]
	[SerializeField]
	private int fermerExtractPointCount = 6;

	[SerializeField]
	private Material fermerExtractPointMaterial;

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

		tileDict.Add(TileType.CitizenExtractPoint, new MainPointTile(TileType.CitizenExtractPoint).GetTile());
		tileDict.Add(TileType.FermersExtractPoint, new MainPointTile(TileType.FermersExtractPoint).GetTile());

		return tileDict;
	}

	public MainPointTile GetMainPoint(Race race)
	{
		if (race == Race.Citizen)
		{
			MainPointTile tile = new MainPointTile(TileType.CitizenExtractPoint);
			tile.SetMaterial(citizenExtractPointMaterial);
			tile.SetDomainSettings(citizenDomainSets);

			return tile;
		}
		else
		{
			MainPointTile tile = new MainPointTile(TileType.FermersExtractPoint);
			tile.SetMaterial(fermerExtractPointMaterial);
			tile.SetDomainSettings(fermerDomainSets);

			return tile;
		}
	}

	public int GetMainPointCount(Race race)
	{
		if (race == Race.Citizen)
		{
			return citizenExtractPointCount;
		}
		else
		{
			return fermerExtractPointCount;
		}
	}
}
