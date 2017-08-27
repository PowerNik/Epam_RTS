using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePointsGenerator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private BasePointSettings basePointSets;
	private MapLayerType[,] layerGrid;

	private int tileCountX;
	private int tileCountZ;
	private float tileSize;

	public BasePointsGenerator(MapSettingsSO mapSettings)
	{
		basePointSets = mapSettings.GetBasePointSettings();

		MapSizeSettings mapSizeSets = mapSettings.GetMapSizeSettings();
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;
	}

	public MapLayerType[,] CreateBasePoints(MapLayerType[,] layerGrid)
	{
		this.layerGrid = layerGrid;

		int[,] sectors = new int[basePointSets.sectorsAtX, basePointSets.sectorsAtZ];

		sectors = SectoringCitizenBasePoint(sectors);
		sectors = SectoringFermerBasePoints(sectors);

		PlaceCitizenBasePointOnMap(sectors);
		PlaceFermerBasePointsOnMap(sectors);
		SetBasePointsArea();

		return this.layerGrid;
	}

	private int[,] SectoringCitizenBasePoint(int[,] sectors)
	{
		int seedHash = basePointSets.seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int xCoord;
		int zCoord;

		if (basePointSets.isCitizenAtCenter)
		{
			List<KeyValuePair<int, int>> centerCoordXZ = CalculateCenterSectors();
			int centerIndex = pseudoRandom.Next(0, centerCoordXZ.Count);

			xCoord = centerCoordXZ[centerIndex].Key;
			zCoord = centerCoordXZ[centerIndex].Value;
		}
		else
		{
			xCoord = pseudoRandom.Next(0, basePointSets.sectorsAtX);
			zCoord = pseudoRandom.Next(0, basePointSets.sectorsAtZ);
		}

		sectors[xCoord, zCoord] = 1;
		return sectors;
	}

	private List<KeyValuePair<int, int>> CalculateCenterSectors()
	{
		// Диапазон номеров секторов для баз горожан
		int[] xIndices = CalculateCenter(basePointSets.sectorsAtX);
		int[] zIndices = CalculateCenter(basePointSets.sectorsAtZ);

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

	private int[,] SectoringFermerBasePoints(int[,] sectors)
	{
		int seedHash = basePointSets.seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int xCoord;
		int zCoord;

		for (int i = 0; i < basePointSets.fermerBases.Length; i++)
		{
			while (true)
			{
				int val = pseudoRandom.Next(0, basePointSets.sectorsAtX * basePointSets.sectorsAtZ * 10);
				val = val % (basePointSets.sectorsAtX * basePointSets.sectorsAtZ);

				xCoord = val / basePointSets.sectorsAtZ;
				zCoord = val % basePointSets.sectorsAtZ;

				if (sectors[xCoord, zCoord] == 0)
				{
					sectors[xCoord, zCoord] = 2;
					break;
				}
			}
		}
		return sectors;
	}

	private void PlaceCitizenBasePointOnMap(int[,] sectors)
	{
		int seedHash = basePointSets.seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int sectorWidth = tileCountX / basePointSets.sectorsAtX;
		int sectorLength = tileCountZ / basePointSets.sectorsAtZ;

		// Размещение в центре сектора + сдвиг
		int xCoord = sectorWidth / 2 + pseudoRandom.Next(-sectorWidth / 4, sectorWidth / 4);
		int zCoord = sectorLength / 2 + pseudoRandom.Next(-sectorLength / 4, sectorLength / 4);

		for (int x = 0; x < basePointSets.sectorsAtX; x++)
		{
			for (int z = 0; z < basePointSets.sectorsAtZ; z++)
			{
				if (sectors[x, z] == 1)
				{
					float xPos = x * sectorWidth + xCoord;
					float zPos = z * sectorLength + zCoord;
					CitizenBasePoint = new Vector3(xPos, 0, zPos);
				}
			}
		}
	}

	private void PlaceFermerBasePointsOnMap(int[,] sectors)
	{
		int seedHash = basePointSets.seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		int sectorWidth = tileCountX / basePointSets.sectorsAtX;
		int sectorLength = tileCountZ / basePointSets.sectorsAtZ;

		FermerBasePoints = new Vector3[basePointSets.fermerBases.Length];
		bool isSetBase = false;

		for (int i = 0; i < basePointSets.fermerBases.Length; i++)
		{
			isSetBase = false;
			for (int x = 0; x < basePointSets.sectorsAtX; x++)
			{
				for (int z = 0; z < basePointSets.sectorsAtZ; z++)
				{
					if (sectors[x, z] == 2 && isSetBase == false)
					{
						int xCoord = sectorWidth / 2 + pseudoRandom.Next(-sectorWidth / 4, sectorWidth / 4);
						int zCoord = sectorLength / 2 + pseudoRandom.Next(-sectorLength / 4, sectorLength / 4);

						float xPos = x * sectorWidth + xCoord;
						float zPos = z * sectorLength + zCoord;
						FermerBasePoints[i] = new Vector3(xPos, 0, zPos);

						sectors[x, z] = 0;
						isSetBase = true;
					}
				}
			}
		}
	}

	private void SetBasePointsArea()
	{
		SetAreaParams((int)(basePointSets.citizenBaseSize / tileSize), CitizenBasePoint);
		CitizenBasePoint *= tileSize;

		for (int i = 0; i < basePointSets.fermerBases.Length; i++)
		{
			SetAreaParams((int)(basePointSets.fermerBases[i] / tileSize), FermerBasePoints[i]);
			FermerBasePoints[i] *= tileSize;
		}
	}

	private void SetAreaParams(int areaSize, Vector3 pos)
	{
		int posX = (int)pos.x;
		int posZ = (int)pos.z;
		SetArea(areaSize, posX, posZ);
	}

	private void SetArea(int areaSize, int posX, int posZ)
	{
		for (int x = -areaSize; x < areaSize; x++)
		{
			for (int z = -areaSize; z < areaSize; z++)
			{
				if (0 < posX + x && posX + x < tileCountX - 1)
					if (0 < posZ + z && posZ + z < tileCountZ - 1)
						layerGrid[posX + x, posZ + z] = MapLayerType.LayerGround;
			}
		}
	}
}
