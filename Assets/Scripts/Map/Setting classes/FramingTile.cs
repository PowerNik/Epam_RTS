using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTile
{
	public FramingTile(FramingTileType tileType, LayerTileType layerTileType)
	{
		this.tileType = tileType;
		this.layerTileType = layerTileType;
	}

	[HideInInspector]
	public FramingTileType tileType = FramingTileType.WaterSide;

	[HideInInspector]
	public LayerTileType layerTileType = LayerTileType.Water;

	public Material material;

	public bool isAllowBuild = false;
	public bool isAllowMove = true;
	public bool isAllowFly = true;

	public float moveSpeed = 1;
}
