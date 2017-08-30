using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTile
{
	public LayerTile(LayerTileType type = LayerTileType.Ground)
	{
		tileType = type;
	}

	[HideInInspector]
	public LayerTileType tileType = LayerTileType.Ground;
	public Material material;

	public bool isAllowBuild = true;
	public bool isAllowMove = true;
	public bool isAllowFly = true;

	public float moveSpeed = 1;
}
