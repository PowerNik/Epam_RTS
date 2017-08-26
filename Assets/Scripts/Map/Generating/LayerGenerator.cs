using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGenerator
{
	private int callCount = 0;

	private int tileCountX;
	private int tileCountZ;

	private string seed;
	private bool isRandom;
	private int fillPercent;

	private int surroundWallCount = 4;

	private int[,] map;


	public LayerGenerator(int tileCountX, int tileCountZ)
	{
		this.tileCountX = tileCountX;
		this.tileCountZ = tileCountZ;
	}

	private void SetGeneratingParams(GeneratorSettings genSets)
	{
		seed = genSets.seed;
		isRandom = genSets.isRandom;
		fillPercent = genSets.fillPercent;
	}

	/// <summary>
	/// Генерирует случайно заполненный массив из 0 и 1.
	/// </summary>
	/// <returns></returns>
	public int[,] Generate(GeneratorSettings genSets)
	{
		if (genSets != null)
		{
			SetGeneratingParams(genSets);
		}

		map = new int[tileCountX, tileCountZ];
		RandomFillMap();

		for (int i = 0; i < genSets.smoothCount; i++)
		{
			SmoothMap();
		}

		return map;
	}

	private void RandomFillMap()
	{
		if (isRandom)
		{
			seed = Time.time.ToString();
		}

		int seedHash = seed.GetHashCode() + callCount;
		callCount++;

		System.Random pseudoRandom = new System.Random(seedHash);

		for (int x = 0; x < tileCountX; x++)
		{
			for (int y = 0; y < tileCountZ; y++)
			{
				// Граница карты непроходима
				if (x == 0 || x == tileCountX - 1 || y == 0 || y == tileCountZ - 1)
				{
					//map[x, y] = 1;
				}
				else
				{
					map[x, y] = (pseudoRandom.Next(0, 100) < fillPercent) ? 1 : 0;
				}
			}
		}
	}

	/// <summary>
	/// Соединяет близкие непроходимые/непроходимые области в одну большую
	/// </summary>
	private void SmoothMap()
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int y = 0; y < tileCountZ; y++)
			{
				int neighbourWallTiles = GetSurroundWallCount(x, y);

				if (neighbourWallTiles > surroundWallCount)
				{
					map[x, y] = 1;
				}
				else
				{
					// Можно поставить <= или -1 справа
					if (neighbourWallTiles < surroundWallCount)
					{
						map[x, y] = 0;
					}
				}

			}
		}
	}

	/// <summary>
	/// Число стен вокруг клетки [gridX, gridY]
	/// </summary>
	private int GetSurroundWallCount(int gridX, int gridY)
	{
		int wallCount = 0;

		// Зона, в которой ищутся стены - [gridX -+ 1, gridY -+ 1]
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
		{
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
			{
				// Считаются точки только в соседних клетках
				if (neighbourX == gridX && neighbourY == gridY)
				{
					continue;
				}

				// Вне карты только непроходимые места
				if (neighbourX < 0 || neighbourX >= tileCountX || neighbourY < 0 || neighbourY >= tileCountZ)
				{
					wallCount++;
				}
				else
				{
					wallCount += map[neighbourX, neighbourY];
				}
			}
		}

		return wallCount;
	}
}
