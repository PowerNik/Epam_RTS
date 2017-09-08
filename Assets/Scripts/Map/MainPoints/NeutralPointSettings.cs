using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeutralPointSettings : IMainPointSettings
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private int aggressiveNeuntralPointCount = 3;

	[SerializeField]
	private Material aggressiveNeuntralPointMaterial;

	[SerializeField]
	private DomainSettings aggressiveNeuntralPointDomainSets;

	[Space(10)]
	[SerializeField]
	private int peacefulNeuntralPointCount = 6;

	[SerializeField]
	private Material peacefulNeuntralPointMaterial;

	[SerializeField]
	private DomainSettings peacefulNeuntralPointDomainSets;


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

		tileDict.Add(TileType.AggressiveNeuntralsPoint, GetMainPoint(Race.Citizen).GetTile());
		tileDict.Add(TileType.PeacefulNeuntralsPoint, GetMainPoint(Race.Fermer).GetTile());

		return tileDict;
	}

	public MainPointTile GetMainPoint(Race race)
	{
		if (race == Race.Citizen)
		{
			MainPointTile tile = new MainPointTile(TileType.AggressiveNeuntralsPoint);
			tile.SetMaterial(aggressiveNeuntralPointMaterial);
			tile.SetDomainSettings(aggressiveNeuntralPointDomainSets);

			return tile;
		}
		else
		{
			MainPointTile tile = new MainPointTile(TileType.PeacefulNeuntralsPoint);
			tile.SetMaterial(peacefulNeuntralPointMaterial);
			tile.SetDomainSettings(peacefulNeuntralPointDomainSets);

			return tile;
		}
	}

	public int GetMainPointCount(Race race)
	{
		if (race == Race.Citizen)
		{
			return aggressiveNeuntralPointCount;
		}
		else
		{
			return peacefulNeuntralPointCount;
		}
	}
}
