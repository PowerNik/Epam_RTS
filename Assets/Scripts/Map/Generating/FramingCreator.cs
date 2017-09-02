﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramingCreator
{
	private int tileCountX;
	private int tileCountZ;

	private TileGrid tileGrid;
	private Dictionary<TileType, FramingTile> framingTileDict;

	public FramingCreator(MapSizeSettings mapSizeSets, FramingTileSettings frameTileSets, ref TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;

		framingTileDict = frameTileSets.GetFramingTilePairs();
	}

	public void CreateLayerFraming()
	{
		foreach (var item in framingTileDict)
		{
			tileGrid.AddTile(item.Value.GetTile());
			SetFramingTilesAroundArea(item.Key, item.Value.GetTileType());
		}
	}

	/// <summary>
	/// Обрамляет область типа areaType тайлами типа framingTile
	/// </summary>
	/// <param name="areaType"></param>
	/// <param name="framingType"></param>
	private void SetFramingTilesAroundArea(TileType areaType, TileType framingType)
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
						tileGrid[x, z] = framingType;
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
}
