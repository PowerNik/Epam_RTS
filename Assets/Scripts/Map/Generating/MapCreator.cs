using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoint { get; private set; }

	private LayerGenerator layerGen;

	private MapSizeSettings mapSizeSets;
	private BasePointSettings basePointSets;
	private MapLayers mapLayers;

	private int tileCountX;
	private int tileCountZ;

	/// <summary>
	/// Карта расположения слоев
	/// </summary>
	private MapLayerType[,] layerGrid;


	public MapCreator(MapSettingsSO mapSettings)
	{
		mapSizeSets = mapSettings.GetMapSizeSettings();
		basePointSets = mapSettings.GetBasePointSettings();
		mapLayers = mapSettings.GetMapLayers();

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;

		layerGen = new LayerGenerator(tileCountX, tileCountZ);
		Creating();
	}

	private void Creating()
	{
		CreateLayers();
		CreateBasePoints();
	}

	/// <summary>
	/// Разбивает карту на слои земли, гор, воды
	/// </summary>
	private void CreateLayers()
	{
		CreateLayerGround();
		SetLayer(MapLayerType.LayerWater, mapLayers.waterGenSets);
		SetLayer(MapLayerType.LayerMountain, mapLayers.mountainGenSets);
	}

	private void CreateLayerGround()
	{
		layerGrid = new MapLayerType[tileCountX, tileCountZ];

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				layerGrid[x, z] = MapLayerType.LayerGround;
			}
		}
	}

	/// <summary>
	/// Рандомно генерирует и устанавливает слой layerType поверх всех предыдущих
	/// </summary>
	/// <param name="layerType"></param>
	private void SetLayer(MapLayerType layerType, GeneratorSettings genSets)
	{
		int[,] grid = layerGen.Generate(genSets);

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (grid[x, z] == 1)
				{
					layerGrid[x, z] = layerType;
				}
			}
		}
	}

	public void CreateMeshes(GameObject map)
	{
		int[,] mas = GetLayerMap(MapLayerType.LayerMountain);
		MeshGenerator meshGen = map.transform.GetChild(0).GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, mapSizeSets.tileSize, 5f);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();

		int[,] mas1 = GetLayerMap(MapLayerType.LayerGround);
		MeshGenerator meshGen1 = map.transform.GetChild(1).GetComponent<MeshGenerator>();
		meshGen1.GenerateMesh(mas1, mapSizeSets.tileSize, 0.8f);
		meshGen1.gameObject.AddComponent<NavMeshSourceTag>();

		int[,] mas2 = GetLayerMap(MapLayerType.LayerWater);
		MeshGenerator meshGen2 = map.transform.GetChild(2).GetComponent<MeshGenerator>();
		meshGen2.GenerateMesh(mas2, mapSizeSets.tileSize, 0.5f);
		meshGen2.gameObject.AddComponent<NavMeshSourceTag>();
	}

	/// <summary>
	/// Возвращает карту расположения слоя layerType
	/// </summary>
	/// <returns></returns>
	private int[,] GetLayerMap(MapLayerType layerType)
	{
		int[,] mas = new int[tileCountX, tileCountZ];
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (layerGrid[x, z] == layerType)
				{
					mas[x, z] = 1;
				}
				else
				{
					mas[x, z] = 0;
				}
			}
		}

		return mas;
	}


	private void CreateBasePoints()
	{
		int[,] sectors = new int[basePointSets.sectorsAtX, basePointSets.sectorsAtZ];

		sectors = SectoringCitizenBasePoint(sectors);
		sectors = SectoringFermerBasePoints(sectors);

		PlaceCitizenBasePointOnMap(sectors);
		PlaceFermerBasePointsOnMap(sectors);
		SetBasePointsArea();
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
					CitizenBasePoint = new Vector3(x * sectorWidth + xCoord, 0, z * sectorLength + zCoord);
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

		FermerBasePoint = new Vector3[basePointSets.fermerBases.Length];
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
						FermerBasePoint[i] = new Vector3(x * sectorWidth + xCoord, 0, z * sectorLength + zCoord);

						sectors[x, z] = 0;
						isSetBase = true;
					}
				}
			}
		}
	}

	private void SetBasePointsArea()
	{
		SetAreaParams(basePointSets.citizenBaseSize / 2, CitizenBasePoint);

		float newX = CitizenBasePoint.x - mapSizeSets.width / 2;
		float newZ = CitizenBasePoint.z - mapSizeSets.length / 2;
		CitizenBasePoint = new Vector3(newX, 0, newZ);

		for (int i = 0; i < basePointSets.fermerBases.Length; i++)
		{
			SetAreaParams(basePointSets.fermerBases[i] / 2, FermerBasePoint[i]);

			newX = FermerBasePoint[i].x - mapSizeSets.width / 2;
			newZ = FermerBasePoint[i].z - mapSizeSets.length / 2;
			FermerBasePoint[i] = new Vector3(newX, 0, newZ);
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
