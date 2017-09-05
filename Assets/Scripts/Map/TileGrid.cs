using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	public int CountX { get; private set; }

	public int CountZ { get; private set; }

	public float TileSize { get; private set; }

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
				throw new System.ArgumentException("Try add unknown tileType to TileGrid " + type);
			}

			grid[x, z] = type;
		}
	}

	public TileGrid(MapSizeSettings mapSizeSets)
	{
		CountX = mapSizeSets.TileCountX;
		CountZ = mapSizeSets.TileCountZ;
		TileSize = mapSizeSets.tileSize;

		dict = new Dictionary<TileType, Tile>();
		CreateGrid();
	}

	public TileGrid(int countX, int countZ, float tileSize)
	{
		CountX = countX;
		CountZ = countZ;
		TileSize = tileSize;

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
		grid = new TileType[CountX, CountZ];

		for (int x = 0; x < CountX; x++)
		{
			for (int z = 0; z < CountZ; z++)
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
		int[,] mas = new int[CountX, CountZ];
		if(tileType == TileType.NonDecorable) // Костыль
		{
			return mas;
		}

		for (int x = 0; x < CountX; x++)
		{
			for (int z = 0; z < CountZ; z++)
			{
				if (tileType != TileType.GroundLayer)
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
				else
				{
					//Костыль, чтобы NonDecorable и GroundLayer имели один меш
					if (grid[x, z] == tileType || grid[x, z] == TileType.NonDecorable)
					{
						mas[x, z] = 1;
					}
					else
					{
						mas[x, z] = 0;
					}
				}
			}
		}

		return mas;
	}
}
