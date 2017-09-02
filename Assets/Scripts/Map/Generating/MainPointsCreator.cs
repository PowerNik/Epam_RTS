using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPointsCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private TileGrid tileGrid;

	private int tileCountX;
	private int tileCountZ;
	private float tileSize;
	private string seed;

	private MainPointsSettingsSO mainPointsSets;
	private BasePointSettings basePointSets;
	private SectorSettings regionSets;

	/// <summary>
	/// Первичное разбиение карты. В одном регионе может быть только одна база
	/// </summary>
	private TileType[,] regions;

	/// <summary>
	/// Каждый регион дополнительно разбивается на сектора.
	/// В одном секторе может быть только одна точка
	/// (ExtractPoint, TradePoint, NeutralPoint)
	/// </summary>
	private TileType[,] sectors;

	public MainPointsCreator(MapSizeSettings mapSizeSets, MainPointsSettingsSO mainPointsSets, ref TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;
		this.mainPointsSets = mainPointsSets;

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;

		seed = mainPointsSets.GetSeed();

		basePointSets = mainPointsSets.GetBasePointSettings();
		regionSets = mainPointsSets.GetRegionSettings();
		regions = new TileType[regionSets.countX, regionSets.countZ];
	}

	public void CreateMainPoints()
	{
		AddTilesToTileGrid(basePointSets.GetTileDictionary());
		CreateBasePoints();

	}

	private void AddTilesToTileGrid(Dictionary<TileType, Tile> dict)
	{
		foreach (var item in dict)
		{
			tileGrid.AddTile(item.Value);
		}
	}

	private void CreateBasePoints()
	{
		SectoringCitizenBasePoint();
		SectoringFermerBasePoints();

		PlaceCitizenBasePointOnMap();
		PlaceFermerBasePointsOnMap();
		SetBasePointsArea();
	}

	private void SectoringCitizenBasePoint()
	{
		int seedHash = (seed + basePointSets.GetOverseed()).GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int xCoord;
		int zCoord;

		if (basePointSets.GetIsCenter())
		{
			List<KeyValuePair<int, int>> centerCoordXZ = CalculateCenterSectors();
			int centerIndex = pseudoRandom.Next(0, centerCoordXZ.Count);

			xCoord = centerCoordXZ[centerIndex].Key;
			zCoord = centerCoordXZ[centerIndex].Value;
		}
		else
		{
			xCoord = pseudoRandom.Next(0, regionSets.countX);
			zCoord = pseudoRandom.Next(0, regionSets.countZ);
		}

		regions[xCoord, zCoord] = TileType.CitizenBasePoint;
	}

	private List<KeyValuePair<int, int>> CalculateCenterSectors()
	{
		// Диапазон номеров секторов для баз горожан
		int[] xIndices = CalculateCenter(regionSets.countX);
		int[] zIndices = CalculateCenter(regionSets.countZ);

		List<KeyValuePair<int, int>> coordValuePairs = new List<KeyValuePair<int, int>>();
		for (int x = 0; x < xIndices.Length; x++)
		{
			for (int z = 0; z < zIndices.Length; z++)
			{
				coordValuePairs.Add(new KeyValuePair<int, int>(xIndices[x], zIndices[z]));
			}
		}

		return coordValuePairs;
	}

	private int[] CalculateCenter(int sectorsCount)
	{
		// Если число секторов нечетно, то середина - один сектор 
		// Иначе середина - два сектора
		int[] res;
		if (sectorsCount % 2 == 1)
		{
			res = new int[] { (sectorsCount - 1) / 2 };
		}
		else
		{
			int x1 = (sectorsCount - 2) / 2;
			int x2 = x1 + 1;
			res = new int[] { x1, x2 };
		}

		return res;
	}

	private void SectoringFermerBasePoints()
	{
		int seedHash = (seed + basePointSets.GetOverseed()).GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int xCoord;
		int zCoord;

		for (int i = 0; i < basePointSets.GetBasePoints(Race.Fermer).Length; i++)
		{
			while (true)
			{
				int val = pseudoRandom.Next(0, regionSets.countX * regionSets.countZ * 10);
				val = val % (regionSets.countX * regionSets.countZ);

				xCoord = val / regionSets.countZ;
				zCoord = val % regionSets.countZ;

				if (regions[xCoord, zCoord] != TileType.CitizenBasePoint)
				{
					regions[xCoord, zCoord] = TileType.FermersBasePoint;
					break;
				}
			}
		}
	}

	private void PlaceCitizenBasePointOnMap()
	{
		int seedHash = (seed + basePointSets.GetOverseed()).GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int sectorWidth = tileGrid.CountX / regionSets.countX;
		int sectorLength = tileGrid.CountZ / regionSets.countZ;

		// Размещение в центре сектора + сдвиг
		int xCoord = sectorWidth / 2 + pseudoRandom.Next(-sectorWidth / 4, sectorWidth / 4);
		int zCoord = sectorLength / 2 + pseudoRandom.Next(-sectorLength / 4, sectorLength / 4);

		for (int x = 0; x < regionSets.countX; x++)
		{
			for (int z = 0; z < regionSets.countZ; z++)
			{
				if (regions[x, z] == TileType.CitizenBasePoint)
				{
					float xPos = x * sectorWidth + xCoord;
					float zPos = z * sectorLength + zCoord;
					CitizenBasePoint = new Vector3(xPos, 0, zPos);
				}
			}
		}
	}

	private void PlaceFermerBasePointsOnMap()
	{
		int seedHash = (seed + basePointSets.GetOverseed()).GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int sectorWidth = tileGrid.CountX / regionSets.countX;
		int sectorLength = tileGrid.CountZ / regionSets.countZ;

		FermerBasePoints = new Vector3[basePointSets.GetBasePoints(Race.Fermer).Length];
		bool isSetBase = false;

		for (int i = 0; i < FermerBasePoints.Length; i++)
		{
			isSetBase = false;
			for (int x = 0; x < regionSets.countX; x++)
			{
				for (int z = 0; z < regionSets.countZ; z++)
				{
					if (regions[x, z] == TileType.FermersBasePoint && isSetBase == false)
					{
						int xCoord = sectorWidth / 2 + pseudoRandom.Next(-sectorWidth / 4, sectorWidth / 4);
						int zCoord = sectorLength / 2 + pseudoRandom.Next(-sectorLength / 4, sectorLength / 4);

						float xPos = x * sectorWidth + xCoord;
						float zPos = z * sectorLength + zCoord;
						FermerBasePoints[i] = new Vector3(xPos, 0, zPos);

						regions[x, z] = TileType.None;
						isSetBase = true;
					}
				}
			}
		}
	}

	private void SetBasePointsArea()
	{
		SetAreaParams((int)(basePointSets.GetBasePoints(Race.Citizen)[0].GetDomainSettings().mainRadius / tileSize), 
			CitizenBasePoint, TileType.CitizenBasePoint);
		CitizenBasePoint *= tileSize;

		for (int i = 0; i < basePointSets.GetBasePoints(Race.Fermer).Length; i++)
		{
			SetAreaParams((int)(basePointSets.GetBasePoints(Race.Fermer)[i].GetDomainSettings().mainRadius / tileSize), 
				FermerBasePoints[i], TileType.FermersBasePoint);
			FermerBasePoints[i] *= tileSize;
		}
	}

	private void SetAreaParams(int areaSize, Vector3 pos, TileType type)
	{
		int posX = (int)pos.x;
		int posZ = (int)pos.z;
		SetArea(areaSize, posX, posZ, type);
	}

	private void SetArea(int areaSize, int posX, int posZ, TileType type)
	{
		for (int x = -areaSize; x < areaSize; x++)
		{
			for (int z = -areaSize; z < areaSize; z++)
			{
				if (0 < posX + x && posX + x < tileGrid.CountX - 1)
					if (0 < posZ + z && posZ + z < tileGrid.CountZ - 1)
					{
						tileGrid[posX + x, posZ + z] = type;
					}
			}
		}
	}
}
