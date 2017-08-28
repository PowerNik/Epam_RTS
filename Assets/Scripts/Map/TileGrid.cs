using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
	public int countX { get; private set; }

	public int countZ { get; private set; }

	public BasicTileType[,] Grid { get; private set; }

	public BasicTileType this[int x, int z]
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


	public TileGrid(int countX, int countZ)
	{
		this.countX = countX;
		this.countZ = countZ;

		CreateGrid();
	}

	private void CreateGrid()
	{
		Grid = new BasicTileType[countX, countZ];

		for (int x = 0; x < countX; x++)
		{
			for (int z = 0; z < countZ; z++)
			{
				Grid[x, z] = BasicTileType.Ground;
			}
		}
	}
}
