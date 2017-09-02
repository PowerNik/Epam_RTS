using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
	private int tileCountX;
	private int tileCountZ;
	private float tileSize;

	public TileGrid tileGrid;

	public GridManager(MapSettingsManagerSO mapSetsManager)
	{
		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;

		tileGrid = new TileGrid(tileCountX, tileCountZ);
	}

	public Vector3 GetTilePos(Vector3 pos)
	{
		float x = pos.x - pos.x % tileSize + tileSize / 2f;
		if (pos.x < 0)
		{
			x -= tileSize;
		}

		float z = pos.z - pos.z % tileSize + tileSize / 2f;
		if (pos.z < 0)
		{
			z -= tileSize;
		}
		return new Vector3(x, pos.y, z);
	}

	public bool IsBuildableTile(Vector3 position, Race race)
	{
		int x = (int)((position.x - position.x % tileSize) / tileSize);
		int z = (int)((position.z - position.z % tileSize) / tileSize);

		if (x < 0 || tileCountX <= x || z < 0 || tileCountZ <= z)
		{
			return false;
		}

		return tileGrid.GetTile(x, z).IsAllowBuild(race);
	}
}

