﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExtractPointSettings
{
	[SerializeField]
	private string overseed = "";

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


	public string GetOverseed()
	{
		return overseed;
	}

	public bool GetIsCenter()
	{
		return isCenter;
	}

	public MainPointTile GetExtractPoint(Race race)
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
			MainPointTile tile = new MainPointTile(TileType.FermersBasePoint);
			tile.SetMaterial(fermerExtractPointMaterial);
			tile.SetDomainSettings(fermerDomainSets);

			return tile;
		}
	}

	public int GetExtractCount(Race race)
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