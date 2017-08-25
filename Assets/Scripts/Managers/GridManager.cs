using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
	private int tileCountX;
	private int tileCountZ;
	private float tileSize;

	private TileGrid tileGrid;

	private Dictionary<TileType, Tile> tileDict = new Dictionary<TileType, Tile>();

	public GridManager(MapSizeSettings mapSizeSets)
	{
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileGrid = new TileGrid(tileCountX, tileCountZ);

		tileSize = mapSizeSets.tileSize;
	}

	public Vector3 GetTilePos(Vector3 position)
	{
		float x = position.x - position.x % tileSize + tileSize / 2f;
		if (position.x < 0)
		{
			x -= tileSize;
		}

		float z = position.z - position.z % tileSize + tileSize / 2f;
		if (position.z < 0)
		{
			z -= tileSize;
		}
		return new Vector3(x, position.y, z);
	}

	public bool IsBuildableTile(Vector3 position)
	{
		int x = (int)((position.x - position.x % tileSize) / tileSize);
		int z = (int)((position.z - position.z % tileSize) / tileSize);

		x += tileCountX / 2;
		z += tileCountZ / 2;

		TileType type = tileGrid[x, z];
		if (type == TileType.Ground)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

