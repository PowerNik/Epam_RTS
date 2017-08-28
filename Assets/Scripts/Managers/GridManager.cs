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

	private Dictionary<BasicTileType, BasicTile> tileDict = new Dictionary<BasicTileType, BasicTile>();

	public GridManager(MapSizeSettings mapSizeSets)
	{
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileGrid = new TileGrid(tileCountX, tileCountZ);

		tileSize = mapSizeSets.tileSize;
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

	public void SetLayerMap(LayerType[,] map, LayerSettings layerSets)
	{
		for(int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				LayerType layerType = map[x, z];
				BasicTile tile = layerSets.GetLayerBasicTile(layerType);
				tileGrid[x, z] = tile.tileType;
			}
		}

		tileDict = layerSets.GetBasicTileDictionary();
		SetAllFramingTiles(layerSets.GetFramingTilePairs());
	}

	private void SetAllFramingTiles(Dictionary<BasicTileType, BasicTileType> framingTileDict)
	{
		foreach(var pair in framingTileDict)
		{
			SetFramingTilesAroundArea(pair.Key, pair.Value);
		}
	}

	/// <summary>
	/// Обрамляет область типа areaType тайлами типа framingTile
	/// </summary>
	/// <param name="areaType"></param>
	/// <param name="framingTile"></param>
	private void SetFramingTilesAroundArea(BasicTileType areaType, BasicTileType framingTile)
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				// Обрамление происходит только на земле
				if (tileGrid[x, z] == BasicTileType.Ground)
				{
					if (IsNearestTilesHasType(x, z, areaType))
					{
						tileGrid[x, z] = framingTile;
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
	private bool IsNearestTilesHasType(int curX, int curZ, BasicTileType type)
	{
		int[] dX = { 1, 0, -1, 0, 1, 1, -1, -1 };// Сдвиги к соседним клеткам
		int[] dZ = { 0, 1, 0, -1, 1, -1, 1, -1 };

		for(int i = 0; i < dX.Length; i++)
		{
			if(tileGrid[curX + dX[i], curZ + dZ[i]] == type)
			{
				return true;
			}
		}

		return false;
	}

	public bool IsBuildableTile(Vector3 position)
	{
		int x = (int)((position.x - position.x % tileSize) / tileSize);
		int z = (int)((position.z - position.z % tileSize) / tileSize);

		if(x < 0 || tileCountX <= x || z < 0 || tileCountZ <= z)
		{
			return false;
		}

		BasicTileType type = tileGrid[x, z];
		return tileDict[type].isAllowBuild;
	}
}

