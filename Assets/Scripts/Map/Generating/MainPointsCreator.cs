using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPointsCreator
{
	public Vector3 CitizenBasePoint
	{
		get
		{
			return mainPointPositions[MainPointType.Base][Race.Citizen][0];
		}
	}
	public Vector3[] FermerBasePoints
	{
		get
		{
			return mainPointPositions[MainPointType.Base][Race.Fermer];
		}
	}

	private Dictionary<MainPointType, Dictionary<Race, Vector3[]>> mainPointPositions =
		new Dictionary<MainPointType, Dictionary<Race, Vector3[]>>();

	private TileGrid tileGrid;

	private int tileCountX;
	private int tileCountZ;
	private float tileSize;
	private string seed;

	/// <summary>
	/// В одном регионе располагается только база, без ресурсов?
	/// </summary>
	private bool isUnique;

	private MainPointsSettingsSO mainPointsSets;
	private IMainPointSettings currentMainPointSets;

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
	}

	public void CreateMainPoints()
	{
		tileGrid.AddTile(mainPointsSets.GetNonDecorableTile());
		CreateRegionsAndSectors();

		CreateMainPoints(regions, MainPointType.Base);
		SetRegionDataOnSectors();

		CreateMainPoints(sectors, MainPointType.Extract);
		CreateMainPoints(sectors, MainPointType.Neutral);
		CreateMainPoints(sectors, MainPointType.Trade);
	}

	private void CreateRegionsAndSectors()
	{
		SectorSettings sectorSets = mainPointsSets.GetSectorsSettings();

		regions = new TileType[sectorSets.regionCountX, sectorSets.regionCountZ];

		int sectorCountX = sectorSets.sectorCountX * sectorSets.regionCountX;
		int sectorCountZ = sectorSets.sectorCountZ * sectorSets.regionCountZ;
		sectors = new TileType[sectorCountX, sectorCountZ];

		isUnique = sectorSets.isUnique;
	}

	private void CreateMainPoints(TileType[,] tileMas, MainPointType pointType)
	{
		currentMainPointSets = mainPointsSets.GetMainPointSettings(pointType);
		seed = currentMainPointSets.GetSeed();
		AddTilesToTileGrid(currentMainPointSets.GetTileDictionary());

		SectorGenerator.SetGeneratingParams(tileMas, seed, currentMainPointSets.GetIsCenter());

		GeneratePoints(Race.Citizen, true);
		if (pointType != MainPointType.Trade)
		{
			GeneratePoints(Race.Fermer, false);
		}

		tileMas = SectorGenerator.GetSectors();

		GenerateMainPointPosition(tileMas, pointType, Race.Citizen);
		if (pointType != MainPointType.Trade)
		{
			GenerateMainPointPosition(tileMas, pointType, Race.Fermer);
		}
	}

	private void AddTilesToTileGrid(Dictionary<TileType, Tile> dict)
	{
		foreach (var item in dict)
		{
			tileGrid.AddTile(item.Value);
		}
	}

	/// <summary>
	/// </summary>
	/// <param name="race"></param>
	/// <param name="getIsCenter">Взять ли значение GetIsCenter() из currentMainPointSets</param>
	private void GeneratePoints(Race race, bool getIsCenter)
	{
		TileType type = currentMainPointSets.GetMainPoint(race).GetTileType();
		int count = currentMainPointSets.GetMainPointCount(race);

		bool isCenter = false;
		if (getIsCenter)
		{
			isCenter = currentMainPointSets.GetIsCenter();
		}

		SectorGenerator.GeneratePoints(type, count, isCenter);
	}

	/// <summary>
	/// Помещает регионы расположения баз на более мелкое разбиение секторов
	/// </summary>
	private void SetRegionDataOnSectors()
	{
		int sectorCountX = sectors.GetLength(0);
		int sectorCountZ = sectors.GetLength(1);

		int secWidth = sectorCountX / regions.GetLength(0);
		int secLength = sectorCountZ / regions.GetLength(1);

		for (int x = 0; x < sectorCountX; x++)
		{
			for (int z = 0; z < sectorCountZ; z++)
			{
				sectors[x, z] = regions[x / secWidth, z / secLength];
			}
		}
	}

	private void GenerateMainPointPosition(TileType[,] tileMas, MainPointType pointType, Race race)
	{
		int seedHash = seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		// Базы размещаются по регионам
		int countX = tileMas.GetLength(0);
		int countZ = tileMas.GetLength(1);
		int width = tileGrid.CountX / countX;
		int length = tileGrid.CountZ / countZ;

		TileType tileType = currentMainPointSets.GetMainPoint(race).GetTileType();

		Vector3[] positionMas = new Vector3[currentMainPointSets.GetMainPointCount(race)];
		int currentPoint = 0;

		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
			{
				if (tileMas[x, z] == tileType)
				{
					int radius = CalculateRadius(race, width, length);
					float posX = GeneratePosition(radius, width, x, pseudoRandom);
					float posZ = GeneratePosition(radius, length, z, pseudoRandom);

					positionMas[currentPoint] = new Vector3(posX, 0, posZ);
					currentPoint++;
				}
			}
		}

		AddMainPointPositionsToDict(pointType, race, positionMas);
		SetMainPointsArea(tileMas, pointType, race);
	}

	private float GeneratePosition(int clearRadius, int width, int curX, System.Random pseudoRandom)
	{
		// Сдвиг относительно центра области (региона или сектора)
		int maxDX = width / 2 - clearRadius;
		int deltaX = width / 2 + pseudoRandom.Next(-maxDX, maxDX);
		float xPos = curX * width + deltaX;

		return xPos;
	}

	private int CalculateRadius(Race race, int width, int length)
	{
		int clearRadius = currentMainPointSets.GetMainPoint(race).GetDomainSettings().nonDecorableRadius;
		int minSize = Math.Min(width / 2, length / 2);
		clearRadius = Math.Min(clearRadius, minSize);

		return clearRadius;
	}

	private void AddMainPointPositionsToDict(MainPointType pointType, Race race, Vector3[] mas)
	{
		if (mainPointPositions.ContainsKey(pointType) == false)
		{
			var dict = new Dictionary<Race, Vector3[]>();
			dict.Add(race, mas);
			mainPointPositions.Add(pointType, dict);
		}
		else
		{
			mainPointPositions[pointType].Add(race, mas);
		}
	}

	private void SetMainPointsArea(TileType[,] tileMas, MainPointType pointType, Race race)
	{
		var dict = mainPointPositions[pointType];
		MainPointTile mainPoint = mainPointsSets.GetMainPointSettings(pointType).GetMainPoint(race);

		int width = tileGrid.CountX / tileMas.GetLength(0);
		int length = tileGrid.CountZ / tileMas.GetLength(1);
		int nonDecorRadius = CalculateRadius(race, width, length);

		for (int i = 0; i < dict[race].Length; i++)
		{
			SetNonDecorableArea(dict[race][i], nonDecorRadius);
			SetTileArea(dict[race][i], mainPoint.GetTileType(), mainPoint.GetDomainSettings().mainRadius);
			dict[race][i] *= tileSize;
		}
	}

	private void SetNonDecorableArea(Vector3 pos, int nonDecorRadius)
	{
		int posX = (int)pos.x;
		int posZ = (int)pos.z;
		int clearArea = (int)(nonDecorRadius / tileSize);

		for (int x = -clearArea; x < clearArea; x++)
		{
			for (int z = -clearArea; z < clearArea; z++)
			{
				if (0 <= posX + x && posX + x < tileGrid.CountX)
					if (0 <= posZ + z && posZ + z < tileGrid.CountZ)
					{
						tileGrid[posX + x, posZ + z] = TileType.NonDecorable;
					}
			}
		}
	}

	private void SetTileArea(Vector3 pos, TileType type, int mainRadius)
	{
		int posX = (int)pos.x;
		int posZ = (int)pos.z;
		int mainArea = (int)(mainRadius / tileSize);

		for (int x = -mainArea; x < mainArea; x++)
		{
			for (int z = -mainArea; z < mainArea; z++)
			{
				if (0 <= posX + x && posX + x < tileGrid.CountX)
					if (0 <= posZ + z && posZ + z < tileGrid.CountZ)
					{
						tileGrid[posX + x, posZ + z] = type;
					}
			}
		}
	}
}
