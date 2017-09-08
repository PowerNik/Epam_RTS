using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationCreator : MonoBehaviour
{
	private TileGrid tileGrid;
	private int decorGridCountX;
	private int decorGridCountZ;

	private DecorationSettings[] staticDecorSets;
	private DecorationSettings[] dynamicDecorSets;
	private DecorationSettings curentSets;
	private GameObject decorParent;

	public void SetTileGrid(ref TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;
		decorGridCountX = tileGrid.CountX;
		decorGridCountZ = tileGrid.CountZ;
	}

	public void SetDecorationSettings(DecorationSettings[] staticDecorSets, DecorationSettings[] dynamicDecorSets)
	{
		this.staticDecorSets = staticDecorSets;
		this.dynamicDecorSets = dynamicDecorSets;
	}

	public void CreateStaticDecorations()
	{
		decorParent = new GameObject();
		decorParent.name = "StaticDecorations";

		RandomGenerator.SetTileMapSize(decorGridCountX, decorGridCountZ);

		foreach (var decorSets in staticDecorSets)
		{
			curentSets = decorSets;
			CreateDecorations();
		}
	}

	private void CreateDecorations()
	{
		GameObject currentParent = new GameObject();
		currentParent.name = curentSets.GetTileHolder().ToString() + "_" + curentSets.name;
		currentParent.transform.parent = decorParent.transform;

		int[,] map = RandomGenerator.Generate(curentSets.GetGeneratorSettings());
		List<List<int[]>> areaList = CorrectAreas(ref map);

		GameObject[] decorMas = curentSets.GetDecorations();
		if (decorMas.Length == 0)
		{
			return;
		}
		int decorCount = decorMas.Length;
		int curDecor = 0;

		Random.InitState(curentSets.GetSeed().GetHashCode());
		float minScale = curentSets.minScale;
		float maxScale = curentSets.maxScale;

		for (int curArea = 0; curArea < areaList.Count; curArea++)
		{
			for (int curPoint = 0; curPoint < areaList[curArea].Count; curPoint++)
			{
				int[] point = areaList[curArea][curPoint];
				if (tileGrid[point[0], point[1]] == curentSets.GetTileHolder())
				{
					Transform tr = Instantiate(decorMas[curDecor % decorCount]).transform;
					curDecor++;

					tr.position = new Vector3(point[0] + 0.5f, 0, point[1] + 0.5f) * tileGrid.TileSize;
					tr.localScale *= Random.Range(minScale, maxScale);
					tr.localRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

					tr.parent = currentParent.transform;
				}
			}
		}
	}

	/// <summary>
	/// Удаляет маленькие области слоя layerType
	/// и ограничивает число оставшихся областей
	/// </summary>
	/// <param name="layerType"></param>
	private List<List<int[]>> CorrectAreas(ref int[,] layerMap)
	{
		var list = GetAllAreas(ref layerMap);

		list = RemoveSmallAreas(list, ref layerMap);
		list = LimitingAreaCount(list, ref layerMap);

		return list;
	}

	private List<List<int[]>> RemoveSmallAreas(List<List<int[]>> list, ref int[,] layerMap)
	{
		LandscapeSettings landscapeSets = curentSets.GetLandscapeSettings();

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
					layerMap[point[0], point[1]] = 0;
				}
				list.RemoveAt(i);
				i--;
			}
		}

		return list;
	}

	private List<List<int[]>> LimitingAreaCount(List<List<int[]>> list, ref int[,] layerMap)
	{
		Random.InitState(curentSets.GetSeed().GetHashCode());

		LandscapeSettings landscapeSets = curentSets.GetLandscapeSettings();
		if (landscapeSets.maxCount == -1)
		{
			return list;
		}

		while (list.Count > landscapeSets.maxCount)
		{
			int index = Random.Range(0, list.Count);

			foreach (int[] point in list[index])
			{
				layerMap[point[0], point[1]] = 0;
			}
			list.RemoveAt(index);
		}

		return list;
	}

	/// <summary>
	/// Список всех связанных областей слоя layerType
	/// </summary>
	/// <param name="layerType"></param>
	/// <returns></returns>
	private List<List<int[]>> GetAllAreas(ref int[,] layerMap)
	{
		var allAreasList = new List<List<int[]>>();

		for (int x = 0; x < decorGridCountX; x++)
		{
			for (int z = 0; z < decorGridCountZ; z++)
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

				if (x < 0 || decorGridCountX <= x || z < 0 || decorGridCountZ <= z)
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
}
