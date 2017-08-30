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

	private Dictionary<LayerTileType, LayerTile> layerTileDict = 
		new Dictionary<LayerTileType, LayerTile>();

	private Dictionary<FramingTileType, FramingTile> framingTileDict = 
		new Dictionary<FramingTileType, FramingTile>();

	public GridManager(MapSettingsManagerSO mapSetsManager)
	{
		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;

		tileGrid = new TileGrid(tileCountX, tileCountZ);

		layerTileDict = mapSetsManager.GetLayerTileSettings().GetLayerTileDictionary();
		framingTileDict = mapSetsManager.GetFramingTileSettings().GetFramingTileDictionary();
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
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				LayerType layerType = map[x, z];
				LayerTile tile = layerSets.GetLayerLayerTile(layerType);
				tileGrid[x, z] = tile.tileType;
			}
		}
	}
	
	public void SetAllFramingTiles(Dictionary<FramingTileType, LayerTileType> framingTileDict)
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
	private void SetFramingTilesAroundArea(FramingTileType framingTile, LayerTileType areaType)
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				// Обрамление происходит только на земле
				if (tileGrid[x, z] == LayerTileType.Ground)
				{
					if (IsNearestTilesHasType(x, z, areaType))
					{
						//TODO 1 tileGrid должен хранить все типы тайлов:
						// layerTileType, framingTileType, 
						//TODO 2 resourceTileType, citizenTileType
						//tileGrid[x, z] = framingTile;
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
	private bool IsNearestTilesHasType(int curX, int curZ, LayerTileType type)
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

		LayerTileType type = tileGrid[x, z];
		return layerTileDict[type].isAllowBuild;
	}
}

