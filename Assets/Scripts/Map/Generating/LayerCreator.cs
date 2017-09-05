using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCreator
{
	private int layerCountX;
	private int layerCountZ;

	private TileGrid tileGrid;
	private TileGrid layerGrid;

	private LayerSettings layerSets;
	private int borderSize = 12;

	public LayerCreator(MapSizeSettings mapSizeSets, LayerSettings layerSets, ref TileGrid tileGrid)
	{
		layerCountX = mapSizeSets.TileCountX + 2 * borderSize;
		layerCountZ = mapSizeSets.TileCountZ + 2 * borderSize;

		RandomGenerator.SetTileMapSize(layerCountX, layerCountZ);
		layerGrid = new TileGrid(layerCountX, layerCountZ, mapSizeSets.tileSize);
		this.tileGrid = tileGrid;
		this.layerSets = layerSets;

		foreach (var item in layerSets.GetLayerTileDictionary())
		{
			tileGrid.AddTile(item.Value.GetTile());
		}

		foreach (var item in layerSets.GetLayerTileDictionary())
		{
			layerGrid.AddTile(item.Value.GetTile());
		}
	}

	public void CreateLayers()
	{
		CreateLayer(TileType.GroundLayer);
		CreateLayer(TileType.WaterLayer, borderSize);
		CreateLayer(TileType.MountainLayer, borderSize / 2);

		SetTileGridToLayer();

		CorrectLayers();
		SetBorder(TileType.WaterLayer);

		new FramingCreator(layerSets.GetFramingTilePairs(), ref layerGrid);
		SetLayerToTileGrid();
	}

	private void CorrectLayers()
	{
		CorrectAreas(TileType.WaterLayer);
		CorrectAreas(TileType.MountainLayer);
	}

	private void CreateLayer(TileType layerType, int border = 0)
	{
		GeneratorSettings genSets = layerSets.GetGeneratorSettings(layerType);
		int[,] grid = RandomGenerator.Generate(genSets, border);

		for (int x = 0; x < layerCountX; x++)
		{
			for (int z = 0; z < layerCountZ; z++)
			{
				if (grid[x, z] == 1)
				{
					layerGrid[x, z] = layerType;
				}
			}
		}
	}

	/// <summary>
	/// Удаляет маленькие области слоя layerType
	/// и ограничивает число оставшихся областей
	/// </summary>
	/// <param name="layerType"></param>
	private void CorrectAreas(TileType layerType)
	{
		var list = GetAllAreas(layerType);

		list = RemoveSmallAreas(list, layerType);
		LimitingAreaCount(list, layerType);
	}

	private List<List<int[]>> RemoveSmallAreas(List<List<int[]>> list, TileType layerType)
	{
		LandscapeSettings landscapeSets = layerSets.GetLandscapeSettings(layerType);

		if (landscapeSets.minSize == -1)
		{
			return list;
		}

		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Count < landscapeSets.minSize)
			{
				foreach (int[] point in list[i])
				{
					layerGrid[point[0], point[1]] = TileType.GroundLayer;
				}
				list.RemoveAt(i);
				i--;
			}
		}

		return list;
	}

	private void LimitingAreaCount(List<List<int[]>> list, TileType layerType)
	{
		GeneratorSettings genSets = layerSets.GetGeneratorSettings(layerType);

		int seedHash = genSets.GetSeed().GetHashCode();
		System.Random pseudoRandom = new System.Random(seedHash);

		LandscapeSettings landscapeSets = layerSets.GetLandscapeSettings(layerType);
		if (landscapeSets.maxCount == -1)
		{
			return;
		}

		while (list.Count > landscapeSets.maxCount)
		{
			int index = pseudoRandom.Next(0, list.Count);

			foreach (int[] point in list[index])
			{
				layerGrid[point[0], point[1]] = TileType.GroundLayer;
			}
			list.RemoveAt(index);
		}
	}

	/// <summary>
	/// Список всех связанных областей слоя layerType
	/// </summary>
	/// <param name="layerType"></param>
	/// <returns></returns>
	private List<List<int[]>> GetAllAreas(TileType layerType)
	{
		int[,] layerMap = layerGrid.GetTileMap(layerType);
		var allAreasList = new List<List<int[]>>();

		for (int x = 0; x < layerCountX; x++)
		{
			for (int z = 0; z < layerCountZ; z++)
			{
				// Равносильно layerGrid[x, z] == layerType
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

				if (x < 0 || layerCountX <= x || z < 0 || layerCountZ <= z)
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

	public int GetBorderSize()
	{
		return borderSize;
	}

	private void SetTileGridToLayer()
	{
		foreach (var item in tileGrid.GetTileDictionary())
		{
			layerGrid.AddTile(item.Value);
		}

		for (int x = 0; x < tileGrid.CountX; x++)
		{
			for (int z = 0; z < tileGrid.CountZ; z++)
			{
				if (tileGrid[x, z] != TileType.None)
				{
					layerGrid[x + borderSize, z + borderSize] = tileGrid[x, z];
				}
			}
		}
	}

	private void SetLayerToTileGrid()
	{
		foreach (var item in layerGrid.GetTileDictionary())
		{
			tileGrid.AddTile(item.Value);
		}

		for (int x = 0; x < tileGrid.CountX; x++)
		{
			for (int z = 0; z < tileGrid.CountZ; z++)
			{
				tileGrid[x, z] = layerGrid[x + borderSize, z + borderSize];
			}
		}
	}

	public TileGrid GetLayerGrid()
	{
		return layerGrid;
	}

	private void SetBorder(TileType borderType)
	{
		int maxX = layerCountX - 1;
		int maxZ = layerCountZ - 1;

		// "Заливка" земли в пределах границы водой
		for (int i = 0; i < borderSize; i++)
		{
			for (int z = 0; z < layerCountZ; z++)
			{
				if (layerGrid[i, z] == TileType.GroundLayer)
				{
					layerGrid[i, z] = borderType;
				}

				if (layerGrid[maxX - i, z] == TileType.GroundLayer)
				{
					layerGrid[maxX - i, z] = borderType;
				}
			}
		}

		for (int i = 0; i < borderSize; i++)
		{
			for (int x = 0; x < layerCountX; x++)
			{
				if (layerGrid[x, i] == TileType.GroundLayer)
				{
					layerGrid[x, i] = borderType;
				}

				if (layerGrid[x, maxZ - i] == TileType.GroundLayer)
				{
					layerGrid[x, maxZ - i] = borderType;
				}
			}
		}

		SetIrregularBorder(borderType);
	}

	private void SetIrregularBorder(TileType borderType)
	{
		int maxX = layerCountX - 1;
		int maxZ = layerCountZ - 1;

		//Обрамление границы неровностями
		System.Random pseudoRandom = new System.Random(layerSets.GetSeed().GetHashCode());
		int fillPercent = 50;

		for (int z = 0; z < layerCountZ; z++)
		{
			if (layerGrid[borderSize, z] == TileType.GroundLayer)
			{
				if (pseudoRandom.Next(0, 100) < fillPercent)
				{
					layerGrid[borderSize, z] = borderType;
					if (layerGrid[borderSize + 1, z] == TileType.GroundLayer)
					{
						if (pseudoRandom.Next(0, 100) < fillPercent / 3)
							layerGrid[borderSize + 1, z] = borderType;
					}
				}
			}

			if (layerGrid[maxX - borderSize, z] == TileType.GroundLayer)
			{
				if (pseudoRandom.Next(0, 100) < fillPercent)
				{
					layerGrid[maxX - borderSize, z] = borderType;
					if (layerGrid[maxX - (borderSize + 1), z] == TileType.GroundLayer)
					{
						if (pseudoRandom.Next(0, 100) < fillPercent / 3)
							layerGrid[maxX - (borderSize + 1), z] = borderType;
					}
				}
			}
		}

		for (int x = 0; x < layerCountX; x++)
		{
			if (layerGrid[x, borderSize] == TileType.GroundLayer)
			{
				if (pseudoRandom.Next(0, 100) < fillPercent)
				{
					layerGrid[x, borderSize] = borderType;
					if (layerGrid[x, borderSize + 1] == TileType.GroundLayer)
					{
						if (pseudoRandom.Next(0, 100) < fillPercent / 3)
							layerGrid[x, borderSize + 1] = borderType;
					}
				}
			}

			if (layerGrid[x, maxZ - borderSize] == TileType.GroundLayer)
			{
				if (pseudoRandom.Next(0, 100) < fillPercent)
				{
					layerGrid[x, maxZ - borderSize] = borderType;
					if (layerGrid[x, maxZ - (borderSize + 1)] == TileType.GroundLayer)
					{
						if (pseudoRandom.Next(0, 100) < fillPercent / 3)
							layerGrid[x, maxZ - (borderSize + 1)] = borderType;
					}
				}
			}

		}
	}
}
