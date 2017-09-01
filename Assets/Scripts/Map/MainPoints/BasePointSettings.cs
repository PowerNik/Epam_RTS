using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePointSettings
{
	[SerializeField]
	private string overseed = "";

	[SerializeField]
	private bool isCenter = true;

	[Space(15)]
	[SerializeField]
	private Material citizenBasePointMaterial;

	[SerializeField]
	private DomainSettings citizenDomainSets;

	[Space(10)]
	[SerializeField]
	private Material fermerBasePointMaterial;

	[SerializeField]
	private DomainSettings[] fermerDomainSets;


	public string GetOverseed()
	{
		return overseed;
	}

	public bool GetIsCenter()
	{
		return isCenter;
	}

	public MainPointTile[] GetBasePoints(Race race)
	{
		if (race == Race.Citizen)
		{
			MainPointTile tile = new MainPointTile(TileType.CitizenBasePoint);
			tile.SetMaterial(citizenBasePointMaterial);
			tile.SetDomainSettings(citizenDomainSets);

			return new MainPointTile[] { tile };
		}
		else
		{
			MainPointTile[] tiles = new MainPointTile[fermerDomainSets.Length];
			for(int i = 0; i < tiles.Length; i++)
			{
				tiles[i] = new MainPointTile(TileType.FermersBasePoint);
				tiles[i].SetMaterial(fermerBasePointMaterial);
				tiles[i].SetDomainSettings(fermerDomainSets[i]);

			}

			return tiles;
		}
	}
}
