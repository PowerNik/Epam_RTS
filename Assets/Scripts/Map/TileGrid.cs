using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	/// <summary>
	/// X count
	/// </summary>
	public int Width { get; private set; }

	/// <summary>
	/// Z count
	/// </summary>
	public int Length { get; private set; }

	public TileType[,] Grid { get; private set; }

	public TileType this[int x, int z]
	{
		get
		{
			return Grid[x, z];
		}
		set
		{
			Grid[x, z] = value;
		}
	}

	private TileType[] tileTypes;
	private MapManager mapManager;


	public TileGrid(int width, int length, TileType defaultTileType = TileType.Ground)
	{
		Width = width;
		Length = length;

		ReceiveMapManager();
		CreateGrid(defaultTileType);
	}

	private void ReceiveMapManager()
	{
		mapManager = SceneManagerRTS.MapManager;
	}

	private void CreateGrid(TileType tileType)
	{
		ReceiveTileTypes();

		Grid = new TileType[Width, Length];

		for (int x = 0; x < Width; x++)
		{
			for (int z = 0; z < Length; z++)
			{
				Grid[x, z] = tileType;
			}
		}
	}

	private void ReceiveTileTypes()
	{
		tileTypes = mapManager.GetTileTypes();
	}

	public void SetTileType(int x, int z, TileType type)
	{
		Grid[x, z] = type;
	}
}
