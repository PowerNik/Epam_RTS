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

	private Dictionary<LayerType, LayerTile> layerTileDict = 
		new Dictionary<LayerType, LayerTile>();

	private Dictionary<TileType, Tile> allTileDict =
	new Dictionary<TileType, Tile>();

	public GridManager(MapSettingsManagerSO mapSetsManager)
	{
		MapSizeSettings mapSizeSets = mapSetsManager.GetMapSizeSettings();

		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileSize = mapSizeSets.tileSize;

		tileGrid = new TileGrid(tileCountX, tileCountZ);

		layerTileDict = mapSetsManager.GetLayerTileSettings().GetLayerTileDictionary();
		foreach(var item in layerTileDict)
		{
			allTileDict.Add(item.Value.GetTileType(), item.Value.GetTile());
		}
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

	public void SetLayerMap(LayerType[,] map, LayerTileSettings layerTileSets)
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				LayerType layerType = map[x, z];
				LayerTile layerTile = layerTileSets.GetLayerTile(layerType);
				tileGrid[x, z] = layerTile.GetTileType();
			}
		}
	}
	
	public void SetAllFramingTiles(Dictionary<LayerType, FramingTile> framingTileDict)
	{
		foreach(var pair in framingTileDict)
		{
			SetFramingTilesAroundArea(pair.Key, pair.Value);
			allTileDict.Add(pair.Value.GetTileType(), pair.Value.GetTile());
		}
	}

	/// <summary>
	/// Обрамляет область типа areaType тайлами типа framingTile
	/// </summary>
	/// <param name="areaType"></param>
	/// <param name="framingTile"></param>
	private void SetFramingTilesAroundArea(LayerType areaType, FramingTile framingTile)
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
						tileGrid[x, z] = framingTile.GetTileType();
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
	private bool IsNearestTilesHasType(int curX, int curZ, LayerType type)
	{
		int[] dX = { 1, 0, -1, 0, 1, 1, -1, -1 };// Сдвиги к соседним клеткам
		int[] dZ = { 0, 1, 0, -1, 1, -1, 1, -1 };

		for(int i = 0; i < dX.Length; i++)
		{
			if(tileGrid[curX + dX[i], curZ + dZ[i]] == ((TileType)(int)(type)))
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

		return allTileDict[tileGrid[x, z]].IsAllowBuild(Race.Fermer);
	}
}

