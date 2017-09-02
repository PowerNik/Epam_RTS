using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerator
{
	static private int callCount = 0;

	static private int tileCountX;
	static private int tileCountZ;

	static private int seedHash;
	static private int fillPercent;

	static private int surroundWallCount = 4;

	static private int[,] map;


	public static void SetTileMapSize(int tileCountX, int tileCountZ)
	{
		RandomGenerator.tileCountX = tileCountX;
		RandomGenerator.tileCountZ = tileCountZ;
	}

	private static void SetGeneratingParams(GeneratorSettings genSets)
	{
		if (genSets.isRandom)
		{
			seedHash = Time.time.ToString().GetHashCode() + callCount;
			callCount++;
		}
		else
		{
			seedHash = genSets.seed.GetHashCode() + callCount;
			callCount++;
		}

		fillPercent = genSets.fillPercent;
	}

	/// <summary>
	/// Генерирует случайно заполненный массив из 0 и 1.
	/// </summary>
	/// <param name="genSets"></param>
	/// <param name="border">Ширина границ массива, заполненных 1</param>
	/// <returns></returns>
	public static int[,] Generate(GeneratorSettings genSets, int border = 0)
	{
		if (genSets != null)
		{
			SetGeneratingParams(genSets);
		}

		map = new int[tileCountX, tileCountZ];
		RandomFillMap(border);

		for (int i = 0; i < genSets.smoothCount; i++)
		{
			SmoothMap();
		}

		return map;
	}

	private static void RandomFillMap(int border)
	{
		// Не костыль, а фича :)
		// Заполнение массива на 100% не является рандомной генерацией
		// Так что за вызов рандомогенератора не считается
		if(fillPercent == 100)
		{
			callCount--;
		}

		System.Random pseudoRandom = new System.Random(seedHash);

		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				// 0 <= x < b  <=>  -b <= x - b < 0
				// maxX - b <= x < maxX  <=>  maxX <= x + b < maxX + b
				bool isBorderX = x - border < 0 || tileCountX <= x + border;
				bool isBorderZ = z - border < 0 || tileCountZ <= z + border;

				if (isBorderX || isBorderZ)
				{
					map[x, z] = 1;
				}
				else
				{
					map[x, z] = (pseudoRandom.Next(0, 100) < fillPercent) ? 1 : 0;
				}
			}
		}
	}

	/// <summary>
	/// Соединяет близкие непроходимые/непроходимые области в одну большую
	/// </summary>
	private static void SmoothMap()
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				int neighbourWallTiles = GetSurroundWallCount(x, z);

				if (neighbourWallTiles > surroundWallCount)
				{
					map[x, z] = 1;
				}
				else
				{
					// Можно поставить <= или -1 справа
					if (neighbourWallTiles < surroundWallCount)
					{
						map[x, z] = 0;
					}
				}

			}
		}
	}

	/// <summary>
	/// Число стен вокруг клетки [gridX, gridY]
	/// </summary>
	private static int GetSurroundWallCount(int gridX, int gridZ)
	{
		int wallCount = 0;

		// Зона, в которой ищутся стены - [gridX -+ 1, gridY -+ 1]
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
		{
			for (int neighbourZ = gridZ - 1; neighbourZ <= gridZ + 1; neighbourZ++)
			{
				// Считаются точки только в соседних клетках
				if (neighbourX == gridX && neighbourZ == gridZ)
				{
					continue;
				}

				// Вне карты только непроходимые места
				if (neighbourX < 0 || neighbourX >= tileCountX || neighbourZ < 0 || neighbourZ >= tileCountZ)
				{
					wallCount++;
				}
				else
				{
					wallCount += map[neighbourX, neighbourZ];
				}
			}
		}

		return wallCount;
	}
}
