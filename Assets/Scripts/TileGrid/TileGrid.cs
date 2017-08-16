using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	/// <summary>
	/// X count
	/// </summary>
	public int GridWidth { get; private set; }

	/// <summary>
	/// Z count
	/// </summary>
	public int GridLength { get; private set; }

	private TileType[,] grid;
	private Dictionary<TileType, Tile> tiles;

	public TileGrid(int width, int length, TileType defaultTileType = TileType.Ground)
	{
		GridWidth = width;
		GridLength = length;

		CreateGrid(defaultTileType);
	}

	private void CreateGrid(TileType defaultTileType)
	{
		ReceiveTiles();

		TileType tiletype = TileType.Ground;

		if(tiles.ContainsKey(defaultTileType))
		{
			tiletype = defaultTileType;
		}

		grid = new TileType[GridWidth, GridLength];

		for (int x = 0; x < GridWidth; x++)
		{
			for(int z = 0; z < GridLength; z++)
			{
				grid[x, z] = tiletype;
			}
		}
	}

	private void ReceiveTiles()
	{
		//TODO Reading from SceneManager
	}
}
