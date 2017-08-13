using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] private int width = 200;
	[SerializeField] private int height = 200;

	[Tooltip("Ключ для генерации карты")]
	[SerializeField]
	private string seed = "main";

	[Tooltip("Рандомно генерировать ключ?")]
	[SerializeField]
	private bool isRandomSeed = false;

	[Tooltip("Максимальный процент непроходимой местности")]
	[SerializeField]
	[Range(0, 100)]
	private int randomFillPercent = 47;

	private int smoothCount = 5;
	private int surroundWallCount = 4;

	private int[,] map;


	void Start()
	{
		GenerateMap();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			GenerateMap();
		}
	}

	private void GenerateMap()
	{
		map = new int[width, height];
		RandomFillMap();

		for (int i = 0; i < smoothCount; i++)
		{
			SmoothMap();
		}

		MeshGenerator mesh = GetComponent<MeshGenerator>();
		mesh.GenerateMesh(map, 1);
	}

	private void RandomFillMap()
	{
		if (isRandomSeed)
		{
			seed = Time.time.ToString();
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				// Граница карты непроходима
				if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
				{
					map[x, y] = 1;
				}
				else
				{
					map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
				}
			}
		}
	}

	/// <summary>
	/// Соединяет близкие непроходимые/непроходимые области в одну большую
	/// </summary>
	private void SmoothMap()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
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
	/// <returns></returns>
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
				if (neighbourX < 0 || neighbourX >= width || neighbourY < 0 || neighbourY >= height)
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
