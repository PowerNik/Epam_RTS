using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SectorGenerator
{
	static private int callCount = 0;

	static private TileType[,] sectors;
	static private int countX;
	static private int countZ;

	static bool isCentering = false;

	static private string seed = "";
	static private int seedHash;

	/// <summary>
	/// Список координат центральных точек
	/// </summary>
	static private List<KeyValuePair<int, int>> centerCoordXZ;

	static public TileType[,] GetSectors()
	{
		return sectors;
	}

	/// <summary>
	/// </summary>
	/// <param name="sectors"></param>
	/// <param name="seed"></param>
	/// <param name="isCentering">Есть ли центрирование одной расы</param>
	public static void SetGeneratingParams(TileType[,] sectors, string seed, bool isCentering)
	{
		SectorGenerator.isCentering = isCentering;
		SectorGenerator.sectors = sectors;
		countX = sectors.GetLength(0);
		countZ = sectors.GetLength(1);

		if (SectorGenerator.seed == seed)
		{
			callCount++;
			seedHash = seed.GetHashCode() + callCount;
		}
		else
		{
			SectorGenerator.seed = seed;
			seedHash = seed.GetHashCode();
		}
	}

	static public void GeneratePoints(TileType type, int pointCount, bool isPointsAtCenter)
	{
		System.Random pseudoRandom = new System.Random(seedHash);
		centerCoordXZ = CalculateCenterSectors();

		int xCoord;
		int zCoord;
		bool isCorrect = false;

		for (int i = 0; i < pointCount; i++)
		{
			while (true)
			{
				isCorrect = GenerateCoordinates(pseudoRandom, isPointsAtCenter, out xCoord, out zCoord);

				if (isCorrect && sectors[xCoord, zCoord] == TileType.None)
				{
					sectors[xCoord, zCoord] = type;
					break;
				}
			}
		}
	}

	static private bool GenerateCoordinates(System.Random pseudoRandom, bool isPointsAtCenter, 
		out int xCoord, out int zCoord)
	{
		if (isPointsAtCenter)
		{
			int centerIndex = pseudoRandom.Next(0, centerCoordXZ.Count);

			xCoord = centerCoordXZ[centerIndex].Key;
			zCoord = centerCoordXZ[centerIndex].Value;
		}
		else
		{
			// Больше рандомности :)
			int val = pseudoRandom.Next(0, countX * countZ * 10);
			val = val % (countX * countZ);

			xCoord = val / countZ;
			zCoord = val % countZ;

			// Если стоит центрирование,
			// то нецентральные точки не должны быть в центре
			if (isCentering)
			{
				foreach (var item in centerCoordXZ)
				{
					if (item.Key == xCoord && item.Value == zCoord)
					{
						return false;
					}
				}
			}
		}

		return true;
	}

	static private List<KeyValuePair<int, int>> CalculateCenterSectors()
	{
		// Диапазон индексов
		int[] xIndices = CalculateCenterArea(countX);
		int[] zIndices = CalculateCenterArea(countZ);

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

	static private int[] CalculateCenterArea(int sectorsCount)
	{
		if(sectorsCount == 2)
		{
			// Потому что из двух точек центр выбрать можно
			// всего двумя способами
			return new int[] { 1 };
		}

		// 0|1|2
		// 0|12|3
		// 0|123|4

		int cPoint = sectorsCount / 2; // индекс середины массива
		int cWidth = sectorsCount / 2; // ширина центральной области
		int cSector = sectorsCount % 2; // есть ли "неделимый" сектор

		List<int> list = new List<int>();
		for (int i = cPoint - cWidth / 2; i < cPoint + cWidth / 2 + cSector; i++)
		{
			list.Add(i);
		}

		return list.ToArray();
	}
}
