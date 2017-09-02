using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradePointSettings
{
	[SerializeField]
	private string overseed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private int tradePointCount = 3;

	[SerializeField]
	private Material tradePointMaterial;

	[SerializeField]
	private DomainSettings tradePointDomainSets;

	public void SetSeed(string seed)
	{
		if (overseed == "")
		{
			overseed = seed;
		}
	}

	public string GetOverseed()
	{
		return overseed;
	}

	public bool GetIsCenter()
	{
		return isCenter;
	}

	public MainPointTile GetTradePoint(Race race)
	{
		MainPointTile tile = new MainPointTile(TileType.TradePoint);
		tile.SetMaterial(tradePointMaterial);
		tile.SetDomainSettings(tradePointDomainSets);

		return tile;
	}

	public int GetTradePointsCount(Race race)
	{
		return tradePointCount;
	}

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		Dictionary<TileType, Tile> tileDict = new Dictionary<TileType, Tile>();

		tileDict.Add(TileType.TradePoint, new MainPointTile(TileType.TradePoint).GetTile());

		return tileDict;
	}
}
