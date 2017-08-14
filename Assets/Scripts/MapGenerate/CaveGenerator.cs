using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGenerator : MonoBehaviour
{
	#region Fields

	[SerializeField] private int width = 200;
	public int Width { get { return width; } }

	[SerializeField] private int height = 200;
	public int Height { get { return height; } }

	[SerializeField]
	[Range(0.2f, 1)]
	private float squareSize = 1;
	public float SquareSize { get { return squareSize; } }

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

	private int curX, curZ;

	MeshCaveGenerator meshGen;
	private int[,] map;

	#endregion

	void Start()
	{
		meshGen = GetComponent<MeshCaveGenerator>();
		GenerateMap();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			GenerateMap();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			DestructMap();
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			ConstructMap();
		}
	}

	#region ChangeMap

	private void DestructMap()
	{
		CalculateMousePosition();
		ChangeMap(0);
	}

	private void ConstructMap()
	{
		CalculateMousePosition();
		ChangeMap(1);
	}

	private void CalculateMousePosition()
	{
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		curX = (int)(worldPoint.x / squareSize + width / 2);
		curZ = (int)(worldPoint.z / squareSize + height / 2);
	}
	/// <summary>
	/// </summary>
	/// <param name="changeValue">0 - удалить возвышенность, 1 - создать возвышенность</param>
	private void ChangeMap(int changeValue)
	{
		// Границу нельзя изменить
		if (0 < curX && curX < width - 1 && 0 < curZ && curZ < height - 1)
		{
			if (map[curX, curZ] != changeValue)
			{
				map[curX, curZ] = changeValue;
				meshGen.GenerateMesh(map, squareSize);
			}
		}
	}
	#endregion

	#region MapGenerating

	private void GenerateMap()
	{
		map = new int[width, height];
		RandomFillMap();

		for (int i = 0; i < smoothCount; i++)
		{
			SmoothMap();
		}

		meshGen.GenerateMesh(map, squareSize);
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

	#endregion
}
