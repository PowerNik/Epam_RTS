using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] private int width = 100;
	[SerializeField] private int height = 100;

	[Tooltip("Ключ для генерации карты")]
	[SerializeField]
	private string seed = "main";

	[Tooltip("Рандомно генерировать ключ?")]
	[SerializeField]
	private bool isRandomSeed = false;

	[Tooltip("Максимальный процент непроходимой местности")]
	[SerializeField]
	[Range(0, 100)]
	private int randomFillPercent = 5;

	private const int smoothCount = 5;
	private const int sorroundWallCount = 4;
	private const int gridStep = 1;

	private int[,] map;


	void Start()
	{
		GenerateMap();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			GenerateMap();
		}
	}

	void OnDrawGizmos()
	{
		if (map != null)
		{
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
					Vector3 pos = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
					Gizmos.DrawCube(pos, Vector3.one);
				}
			}
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
				//граница карты непроходима
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
	/// Соединяет непроходимые области
	/// </summary>
	private void SmoothMap()
	{
		for (int x = 1; x < width - 1; x++)
		{
			for (int y = 1; y < height - 1; y++)
			{
				int neighbourWallTiles = GetSorroundWallCount(x, y);

				if (neighbourWallTiles > sorroundWallCount)
				{
					map[x, y] = 1;
				}
				else
				{
					//TODO проверить для случая <=
					if (neighbourWallTiles < sorroundWallCount)
					{
						map[x, y] = 0;
					}
				}

			}
		}
	}

	private int GetSorroundWallCount(int gridX, int gridY)
	{
		int wallCount = 0;

		// Зона, в которой ищутся стены - [gridX -+ gridStep, gridY -+ gridStep]
		for (int neighbourX = gridX - gridStep; neighbourX <= gridX + gridStep; neighbourX++)
		{
			for (int neighbourY = gridY - gridStep; neighbourY <= gridY + gridStep; neighbourY++)
			{
				//вне карты только непроходимые места
				if (neighbourX < 0 || neighbourX >= width || neighbourY < 0 || neighbourY >= height)
				{
					wallCount++;
				}
				else
				{
					//TODO проверить || вместо &&
					if (neighbourX != gridX || neighbourY != gridY)
					{
						wallCount += map[neighbourX, neighbourY];
					}
				}
			}
		}

		return wallCount;
	}
}
