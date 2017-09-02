using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	public int countX { get; private set; }

	public int countZ { get; private set; }

	private TileType[,] grid;
	private Dictionary<TileType, Tile> dict;

	public TileType this[int x, int z]
	{
		get
		{
			return grid[x, z];
		}
		set
		{
			TileType type = value;
			if (!dict.ContainsKey(type))
			{
				throw new System.ArgumentException("Try add unknown tileType to TileGrid");
			}

			grid[x, z] = type;
		}
	}

	public TileGrid(int countX, int countZ)
	{
		this.countX = countX;
		this.countZ = countZ;

		dict = new Dictionary<TileType, Tile>();
		CreateGrid();
	}

	public void AddTile(Tile tile)
	{
		if (!dict.ContainsKey(tile.GetTileType()))
		{
			dict.Add(tile.GetTileType(), tile);
		}
	}

	public Tile GetTile(int x, int z)
	{
		return dict[grid[x, z]];
	}

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		return dict;
	}

	private void CreateGrid()
	{
		grid = new TileType[countX, countZ];

		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
			{
				grid[x, z] = TileType.None;
			}
		}
	}

	/// <summary>
	/// Возвращает карту расположения тайла tileType
	/// </summary>
	/// <returns> 1 - тайл занят типом tileType, 0 - другим типом тайла</returns>
	public int[,] GetTileMap(TileType tileType)
	{
		int[,] mas = new int[countX, countZ];
		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
			{
				if (grid[x, z] == tileType)
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
