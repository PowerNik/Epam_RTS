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

	public GridManager(MapSizeSettings mapSizeSets, Tile[] tileMas)
	{
		tileCountX = mapSizeSets.TileCountX;
		tileCountZ = mapSizeSets.TileCountZ;
		tileGrid = new TileGrid(tileCountX, tileCountZ);

		tileSize = mapSizeSets.tileSize;

		SetTiles(tileMas);
	}

	private void SetTiles(Tile[] tileMas)
	{
		for(int i = 0; i < tileMas.Length; i++)
		{
			tileDict.Add(tileMas[i].TileType, tileMas[i]);
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

	public void SetLayersMap(MapLayerType[,] map, MapLayers mapLayers)
	{
		for(int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				tileGrid[x, z] = mapLayers.GetTileType(map[x, z]);
			}
		}
	}

	public bool IsBuildableTile(Vector3 position)
	{
		int x = (int)((position.x - position.x % tileSize) / tileSize);
		int z = (int)((position.z - position.z % tileSize) / tileSize);

		x += tileCountX / 2;
		z += tileCountZ / 2;

		if(x < 0 || tileCountX <= x || z < 0 || tileCountZ <= z)
		{
			return false;
		}

		TileType type = tileGrid[x, z];
		return tileDict[type].isAllowBuild;
	}
}

