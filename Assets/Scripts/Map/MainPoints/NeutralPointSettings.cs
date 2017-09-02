using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeutralPointSettings
{
	[SerializeField]
	private string overseed = "";

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

	public MainPointTile GetNeutralPoint(Race race)
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

	public int GetNeutralPointCount(Race race)
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

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		Dictionary<TileType, Tile> tileDict = new Dictionary<TileType, Tile>();

		tileDict.Add(TileType.AggressiveNeuntralsPoint,
			new MainPointTile(TileType.AggressiveNeuntralsPoint).GetTile());
		tileDict.Add(TileType.PeacefulNeuntralsPoint, 
			new MainPointTile(TileType.PeacefulNeuntralsPoint).GetTile());

		return tileDict;
	}
}
