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

	public void SetAllFramingTiles(Dictionary<TileType, FramingTile> framingTileDict)
	{
		foreach (var pair in framingTileDict)
		{
			SetFramingTilesAroundArea(pair.Key, pair.Value);
		}
	}

	/// <summary>
	/// Обрамляет область типа areaType тайлами типа framingTile
	/// </summary>
	/// <param name="areaType"></param>
	/// <param name="framingTile"></param>
	private void SetFramingTilesAroundArea(TileType areaType, FramingTile framingTile)
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				// Обрамление происходит только на земле
				if (tileGrid[x, z] == TileType.GroundLayer)
				{
					if (IsNearestTilesHasType(x, z, areaType))
					{
						tileGrid.SetTile(x, z, framingTile.GetTile());
					}
				}
			}
		}
	}

	/// <summary>
	/// Есть ли вокруг тайла (curX, curZ) тайлы типа type
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	private bool IsNearestTilesHasType(int curX, int curZ, TileType type)
	{
		int[] dX = { 1, 0, -1, 0, 1, 1, -1, -1 };// Сдвиги к соседним клеткам
		int[] dZ = { 0, 1, 0, -1, 1, -1, 1, -1 };

		for (int i = 0; i < dX.Length; i++)
		{
			int x = curX + dX[i];
			int z = curZ + dZ[i];

			if (x < 0 || tileCountX <= x || z < 0 || tileCountZ <= z)
			{
				continue;
			}

			if (tileGrid[x, z] == type)
			{
				return true;
			}
		}

		return false;
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

