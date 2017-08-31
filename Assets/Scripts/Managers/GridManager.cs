using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	private int tileCountX;
	private int tileCountZ;
	private float tileSize;

	private TileGrid tileGrid;
	private MapManager mapManager;

	void Start()
	{
		mapManager = GameManager.GetGameManager().MapManagerInstance;
		tileSize = mapManager.TileSize;
	}

	public void SetTileGrid(TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;

		tileCountX = tileGrid.countX;
		tileCountZ = tileGrid.countZ;
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

