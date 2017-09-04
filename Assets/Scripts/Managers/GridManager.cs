using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
	public TileGrid tileGrid;
	private float tileSize;

	public GridManager(MapSizeSettings mapSizeSets)
	{
		tileGrid = new TileGrid(mapSizeSets);
		tileSize = tileGrid.TileSize;
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

	public bool IsBuildableTile(Vector3 pos, Race race)
	{
		int x, z;
		if (CalculateTilePos(pos, out x, out z))
		{
			return tileGrid.GetTile(x, z).IsAllowBuild(race);
		}

		return false;
	}

	public bool IsExtractableTile(Vector3 pos, Race race)
	{
		int x, z;
		if (CalculateTilePos(pos, out x, out z))
		{
			return tileGrid.GetTile(x, z).IsAllowExtract(race);
		}

		return false;
	}

	/// <summary>
	/// Находится ли pos в пределах карты
	/// </summary>
	/// <returns></returns>
	private bool CalculateTilePos(Vector3 pos, out int x, out int z)
	{
		x = (int)((pos.x - pos.x % tileSize) / tileSize);
		z = (int)((pos.z - pos.z % tileSize) / tileSize);

		if (x < 0 || tileGrid.CountX <= x || z < 0 || tileGrid.CountZ <= z)
		{
			return false;
		}

		return true;
	}
}

