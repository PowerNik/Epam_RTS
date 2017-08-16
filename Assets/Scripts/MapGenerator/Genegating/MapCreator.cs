using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator
{
	private GameObject map;

	/// <summary>
	/// Карта расположения слоев
	/// </summary>
	private MapLayerType[,] layerGrid;
	private MapLayer[] layerData;

	private LayerGenerator layerGen;
	MapGeneratorSettings genSets;

	public TileGrid TileGrid { get; private set; }


	public MapCreator(MapGeneratorSettings genSets, MapLayer[] layerData, GameObject map)
	{
		this.layerData = layerData;
		this.map = map;
		this.genSets = genSets;

		Creating();
	}

	private void Creating()
	{
		layerGen = new LayerGenerator(genSets);
		TileGrid = new TileGrid(layerGen.Width, layerGen.Length);

		CreateLayers();
		SetTiles();

		CreateMeshes();
	}

	/// <summary>
	/// Разбивает карту на слои
	/// </summary>
	private void CreateLayers()
	{
		layerGrid = new MapLayerType[TileGrid.Width, TileGrid.Length];

		for (int x = 0; x < TileGrid.Width; x++)
		{
			for (int z = 0; z < TileGrid.Length; z++)
			{
				layerGrid[x, z] = MapLayerType.LayerGround;
			}
		}

		SetLayer(MapLayerType.LayerWater);
		SetLayer(MapLayerType.LayerMountain);
	}

	/// <summary>
	/// Рандомно устанавливает слой layerType поверх всех остальных
	/// </summary>
	/// <param name="layerType"></param>
	private void SetLayer(MapLayerType layerType)
	{
		int[,] grid = layerGen.Generate();

		for (int x = 0; x < TileGrid.Width; x++)
		{
			for (int z = 0; z < TileGrid.Length; z++)
			{
				if (grid[x, z] == 1)
				{
					layerGrid[x, z] = layerType;
				}
			}
		}
	}

	private void SetTiles()
	{
		//Тайловая карта пустая, поэтому "переносим" на нее тайлы первых трех РАЗНЫХ слоев
		int i = 0;
		for (; i < 3; i++)
		{
			for (int x = 0; x < TileGrid.Width; x++)
			{
				for (int z = 0; z < TileGrid.Length; z++)
				{
					if (layerGrid[x, z] == layerData[i].mapLayerType)
					{
						TileGrid[x, z] = layerData[i].tileType;
					}
				}
			}
		}

		//Остальные тайлы рандомно размещаются на своих слоях
		for (; i < layerData.Length; i++)
		{
			int[,] grid = layerGen.Generate();

			for (int x = 0; x < TileGrid.Width; x++)
			{
				for (int z = 0; z < TileGrid.Length; z++)
				{
					if (grid[x, z] == 1 && layerData[i].mapLayerType == layerGrid[x, z])
					{
						TileGrid[x, z] = layerData[i].tileType;
					}
				}
			}
		}
	}

	private void CreateMeshes()
	{
		int[,] mas = GetLayerMap(MapLayerType.LayerMountain);
		var meshGen = map.transform.GetChild(0).GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(mas, genSets.tileSize);

		var mas1 = GetLayerMap(MapLayerType.LayerGround);
		var meshGen1 = map.transform.GetChild(1).GetComponent<GroundGenerator>();
		meshGen1.GenerateMesh(mas, genSets.tileSize);

		var mas2 = GetLayerMap(MapLayerType.LayerGround);
		var meshGen2 = map.transform.GetChild(2).GetComponent<GroundGenerator>();
		meshGen2.GenerateMesh(mas, genSets.tileSize);
	}

	private int[,] GetLayerMap(MapLayerType layerType)
	{
		int[,] mas = new int[TileGrid.Width, TileGrid.Length];
		for (int x = 0; x < TileGrid.Width; x++)
		{
			for (int z = 0; z < TileGrid.Length; z++)
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
