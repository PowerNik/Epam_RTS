using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradePointSettings : IMainPointSettings
{
	[SerializeField]
	private string seed = "";
	private string mainSeed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private int tradePointCount = 3;

	[SerializeField]
	private Material tradePointMaterial;

	[SerializeField]
	private DomainSettings tradePointDomainSets;


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
		tileDict.Add(TileType.TradePoint, GetMainPoint(Race.Citizen).GetTile());
		return tileDict;
	}

	public MainPointTile GetMainPoint(Race race)
	{
		MainPointTile tile = new MainPointTile(TileType.TradePoint);
		tile.SetMaterial(tradePointMaterial);
		tile.SetDomainSettings(tradePointDomainSets);

		return tile;
	}

	public int GetMainPointCount(Race race)
	{
		return tradePointCount;
	}
}
