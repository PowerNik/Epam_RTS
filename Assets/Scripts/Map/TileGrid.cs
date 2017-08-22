using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	/// <summary>
	/// X count
	/// </summary>
	public int countX { get; private set; }

	/// <summary>
	/// Z count
	/// </summary>
	public int countZ { get; private set; }

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


	public TileGrid(int countX, int countZ, TileType defaultTileType = TileType.Ground)
	{
		this.countX = countX;
		this.countZ = countZ;

		ReceiveMapManager();
		CreateGrid(defaultTileType);
	}

	private void ReceiveMapManager()
	{
        //TODO.Rename class
	    mapManager = GameManagerBeforeMerge.GetGameManager().MapManagerInstance;
	}

	private void CreateGrid(TileType tileType)
	{
		ReceiveTileTypes();

		Grid = new TileType[countX, countZ];

		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
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
