using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCreator
{
	private int tileCountX;
	private int tileCountZ;

	public LayerType[,] LayerGrid { get; private set; }
	private LayerSettings layerSets;

	public LayerCreator(MapSettingsManagerSO mapSetsManager)
	{
		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;

		RandomGenerator.SetTileMapSize(tileCountX, tileCountZ);

		layerSets = mapSetsManager.GetLayerSettings();
	}

	public void CreateLayers()
	{
		LayerGrid = new LayerType[tileCountX, tileCountZ];

		CreateLayer(LayerType.Ground);
		CreateLayer(LayerType.Water);

		int border = layerSets.mountainBorderWidth;
		CreateLayer(LayerType.Mountain, border);
	}

	public void CorrectLayers(LayerType[,] layerGrid)
	{
		LayerGrid = layerGrid;

		CorrectAreas(LayerType.Water);
		CorrectAreas(LayerType.Mountain);
	}

	private void CreateLayer(LayerType layerType, int border = 0)
	{
		GeneratorSettings genSets = layerSets.GetGeneratorSettings(layerType);
		int[,] grid = RandomGenerator.Generate(genSets, border);

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (grid[x, z] == 1)
				{
					LayerGrid[x, z] = layerType;
				}
			}
		}
	}

	/// <summary>
	/// Удаляет маленькие области слоя layerType
	/// и ограничивает число оставшихся областей
	/// </summary>
	/// <param name="layerType"></param>
	private void CorrectAreas(LayerType layerType)
	{
		var list = GetAllAreas(layerType);

		list = RemoveSmallAreas(list, layerType);
		LimitingAreaCount(list, layerType);
	}

	private List<List<int[]>> RemoveSmallAreas(List<List<int[]>> list, LayerType layerType)
	{
		AreaSettings areaSets = layerSets.GetAreaSettings(layerType);

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Count < areaSets.minTileSize)
			{
				foreach (int[] point in list[i])
				{
					LayerGrid[point[0], point[1]] = LayerType.Ground;
				}
				list.RemoveAt(i);
				i--;
			}
		}

		return list;
	}

	private void LimitingAreaCount(List<List<int[]>> list, LayerType layerType)
	{
		GeneratorSettings genSets = layerSets.GetGeneratorSettings(layerType);

		int seedHash = genSets.seed.GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		AreaSettings areaSets = layerSets.GetAreaSettings(layerType);

		while (list.Count > areaSets.maxAreaCount)
		{
			int index = pseudoRandom.Next(0, list.Count);

			foreach (int[] point in list[index])
			{
				LayerGrid[point[0], point[1]] = LayerType.Ground;
			}
			list.RemoveAt(index);
		}
	}

	/// <summary>
	/// Список всех связанных областей слоя layerType
	/// </summary>
	/// <param name="layerType"></param>
	/// <returns></returns>
	private List<List<int[]>> GetAllAreas(LayerType layerType)
	{
		int[,] layerMap = GetLayerMap(layerType);
		var allAreasList = new List<List<int[]>>();

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				// Равносильно LayerGrid[x, z] == layerType
				if (layerMap[x, z] == 1)
				{
					var areaList = GetArea(x, z, ref layerMap);
					allAreasList.Add(areaList);
				}
			}
		}

		return allAreasList;
	}

	/// <summary>
	/// Список координат тайлов, которые образуют связную область
	/// </summary>
	/// <param name="startX"></param>
	/// <param name="startZ"></param>
	/// <param name="layerMap"></param>
	/// <returns></returns>
	private List<int[]> GetArea(int startX, int startZ, ref int[,] layerMap)
	{
		var areaList = new List<int[]>();

		var curentPointsList = new List<int[]>();
		curentPointsList.Add(new int[] { startX, startZ });

		// Пометили точку как рассмотренную
		layerMap[startX, startZ] = 0;

		// Волновой алгоритм, он же поиск в ширину
		while (true)
		{
			areaList.AddRange(curentPointsList);

			var nextPointsList = StepBFS(curentPointsList, ref layerMap);
			if (nextPointsList.Count == 0)
			{
				break;
			}

			curentPointsList = nextPointsList;
		}

		return areaList;
	}

	/// <summary>
	/// Один шаг поиска в ширину
	/// </summary>
	/// <param name="curentPointsList"></param>
	/// <param name="layerMap"></param>
	/// <returns></returns>
	private List<int[]> StepBFS(List<int[]> curentPointsList, ref int[,] layerMap)
	{
		int[] dX = { 1, 0, -1, 0 };// Сдвиги к соседним клеткам
		int[] dZ = { 0, 1, 0, -1 };

		var nextPointsList = new List<int[]>();

		foreach (int[] item in curentPointsList)
		{
			for (int i = 0; i < dX.Length; i++)
			{
				int x = item[0] + dX[i];
				int z = item[1] + dZ[i];

				if (x < 0 || tileCountX <= x || z < 0 || tileCountZ <= z)
				{
					continue;
				}

				// Нашли нерассмотренную точку
				if (layerMap[x, z] == 1)
				{
					// Пометили ее как рассмотренную
					layerMap[x, z] = 0;
					nextPointsList.Add(new int[] { x, z });
				}
			}
		}

		return nextPointsList;
	}

	/// <summary>
	/// Возвращает карту расположения слоя layerType
	/// </summary>
	/// <returns>1 - тайл занят слоем layerType, 0 - другим слоем</returns>
	public int[,] GetLayerMap(LayerType layerType)
	{
		int[,] mas = new int[tileCountX, tileCountZ];
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				if (LayerGrid[x, z] == layerType)
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
