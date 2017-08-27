using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	public Vector3 CitizenBasePoint { get; private set; }
	public Vector3[] FermerBasePoints { get; private set; }

	private LayerGenerator layerGen;
	private BasePointsGenerator basePointsGen;
	private MapLayers mapLayers;

	private Dictionary<TileType, Tile> tileDict;

	/// <summary>
	/// Карта расположения слоев
	/// </summary>
	private MapLayerType[,] layerGrid;

	private int tileCountX;
	private int tileCountZ;
	private float tileSize;


	public MapCreator(MapSettingsSO mapSettings, Dictionary<TileType, Tile> tileDict)
	{
		SetParams(mapSettings, tileDict);

		layerGen = new LayerGenerator(tileCountX, tileCountZ);
		basePointsGen = new BasePointsGenerator(mapSettings);
		MapCreating();
	}

	private void SetParams(MapSettingsSO mapSettings, Dictionary<TileType, Tile> tileDict)
	{
		mapLayers = mapSettings.GetMapLayers();

		MapSizeSettings mapSizeSets = mapSettings.GetMapSizeSettings();
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;

		this.tileDict = tileDict;
	}

	public MapLayerType[,] GetLayerGrid()
	{
		return layerGrid;
	}

	private void MapCreating()
	{
		CreateLayers();

		layerGrid = basePointsGen.CreateBasePoints(layerGrid);
		CitizenBasePoint = basePointsGen.CitizenBasePoint;
		FermerBasePoints = basePointsGen.FermerBasePoints;
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

	public void CreateMapMesh(GameObject map)
	{
		CreateMeshForLayer(map, MapLayerType.LayerGround);
		CreateMeshForLayer(map, MapLayerType.LayerWater);
		CreateMeshForLayer(map, MapLayerType.LayerMountain);
	}

	private void CreateMeshForLayer(GameObject map, MapLayerType layerType)
	{
		int[,] mas = GetLayerMap(layerType);
		MeshSettings meshSets = mapLayers.GetMeshSettings(layerType);

		GameObject layerGO = new GameObject();
		layerGO.transform.parent = map.transform;
		layerGO.AddComponent<MeshCollider>();
		layerGO.AddComponent<MeshFilter>();

		TileType tileType = mapLayers.GetTileType(layerType);
		Material mat = tileDict[tileType].material;
		layerGO.AddComponent<MeshRenderer>().material = mat;

		MeshGenerator meshGen = layerGO.AddComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, tileSize, meshSets);
		meshGen.gameObject.AddComponent<NavMeshSourceTag>();
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
}
