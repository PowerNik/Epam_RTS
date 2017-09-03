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
	private int regionCountX;
	private int regionCountZ;

	/// <summary>
	/// Каждый регион дополнительно разбивается на сектора.
	/// В одном секторе может быть только одна точка
	/// (ExtractPoint, TradePoint, NeutralPoint)
	/// </summary>
	private TileType[,] sectors;
	private int sectorCountX;
	private int sectorCountZ;

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
		CreateRegionsAndSectors();

		ChooseMainPointSettings(MainPointType.Base);
		SetRegionDataOnSectors();

		GenerateMainPointPosition(MainPointType.Base, Race.Citizen);
		GenerateMainPointPosition(MainPointType.Base, Race.Fermer);

		ChooseMainPointSettings(MainPointType.Extract);
		GenerateMainPointPosition(MainPointType.Extract, Race.Citizen);
		GenerateMainPointPosition(MainPointType.Extract, Race.Fermer);

		ChooseMainPointSettings(MainPointType.Neutral);
		GenerateMainPointPosition(MainPointType.Neutral, Race.Citizen);
		GenerateMainPointPosition(MainPointType.Neutral, Race.Fermer);

		ChooseMainPointSettings(MainPointType.Trade);
		GenerateMainPointPosition(MainPointType.Trade, Race.Citizen);
	}

	private void CreateRegionsAndSectors()
	{
		SectorSettings sectorSets = mainPointsSets.GetSectorsSettings();

		regionCountX = sectorSets.regionCountX;
		regionCountZ = sectorSets.regionCountZ;
		regions = new TileType[regionCountX, regionCountZ];

		sectorCountX = sectorSets.sectorCountX * regionCountX;
		sectorCountZ = sectorSets.sectorCountZ * regionCountZ;
		sectors = new TileType[sectorCountX, sectorCountZ];

		isUnique = sectorSets.isUnique;
	}

	private void ChooseMainPointSettings(MainPointType type)
	{
		currentMainPointSets = mainPointsSets.GetMainPointSettings(type);
		seed = currentMainPointSets.GetSeed();

		AddTilesToTileGrid(currentMainPointSets.GetTileDictionary());

		if (type == MainPointType.Base)
		{
			SectorGenerator.SetGeneratingParams(regions, seed, currentMainPointSets.GetIsCenter());
			CreatePoints(Race.Citizen, true);
			CreatePoints(Race.Fermer, false);
			regions = SectorGenerator.GetSectors();
		}
		else
		{
			SectorGenerator.SetGeneratingParams(sectors, seed, currentMainPointSets.GetIsCenter());
			CreatePoints(Race.Citizen, true);
			if (type != MainPointType.Trade)
			{
				CreatePoints(Race.Fermer, false);
			}
			sectors = SectorGenerator.GetSectors();
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
	private void CreatePoints(Race race, bool getIsCenter)
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
		for (int x = 0; x < sectorCountX; x++)
		{
			for (int z = 0; z < sectorCountZ; z++)
			{
				// TODO Nik isUnique
				// Заполнять сектора в зависимости от размеров баз?
				// Тогда прям рядом с базой может попасться ресурс, рынок или точка нейтралов
				int secWidth = sectorCountX / regionCountX;
				int secLength = sectorCountZ / regionCountZ;
				sectors[x, z] = regions[x / secWidth, z / secLength];
			}
		}
	}

	private void GenerateMainPointPosition(MainPointType pointType, Race race)
	{
		int seedHash = seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		// Базы размещаются по регионам
		int countX = regionCountX;
		int countZ = regionCountZ;
		TileType[,] tileMas = regions;

		// Другие главные точки - по секторам
		if (pointType != MainPointType.Base)
		{
			countX = sectorCountX;
			countZ = sectorCountZ;
			tileMas = sectors;
		}

		int width = tileGrid.CountX / countX;
		int length = tileGrid.CountZ / countZ;

		TileType tileType = currentMainPointSets.GetMainPoint(race).GetTileType();
		Vector3[] mas = new Vector3[currentMainPointSets.GetMainPointCount(race)];
		int currentPoint = 0;

		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
			{
				if (tileMas[x, z] == tileType)
				{
					tileMas[x, z] = TileType.None;

					// Сдвиги относительно центра области (региона или сектора)
					int deltaX = width / 2 + pseudoRandom.Next(-width / 4, width / 4);
					int deltaZ = length / 2 + pseudoRandom.Next(-length / 4, length / 4);

					float xPos = x * width + deltaX;
					float zPos = z * length + deltaZ;
					mas[currentPoint] = new Vector3(xPos, 0, zPos);
					currentPoint++;
				}
			}
		}

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

		SetBasePointsArea(pointType, race);
	}

	private void SetBasePointsArea(MainPointType pointType, Race race)
	{
		var dict = mainPointPositions[pointType];
		MainPointTile mainPoint = mainPointsSets.GetMainPointSettings(pointType).GetMainPoint(race);

		float areaSize = mainPoint.GetDomainSettings().mainRadius / tileSize;
		for (int i = 0; i < dict[race].Length; i++)
		{
			SetArea((int)areaSize, dict[race][i], mainPoint.GetTileType());
			dict[race][i] *= tileSize;
		}
	}

	private void SetArea(int areaSize, Vector3 pos, TileType type)
	{
		int posX = (int)pos.x;
		int posZ = (int)pos.z;

		for (int x = -areaSize; x < areaSize; x++)
		{
			for (int z = -areaSize; z < areaSize; z++)
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
