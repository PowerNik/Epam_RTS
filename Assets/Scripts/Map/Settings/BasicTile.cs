using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicTile
{
	public BasicTile(BasicTileType type = BasicTileType.Ground)
	{
		tileType = type;
	}

	[HideInInspector]
	public BasicTileType tileType = BasicTileType.Ground;
	public Material material;

	public bool isAllowBuild = true;
	public bool isAllowMove = true;
	public bool isAllowFly = true;

	public float moveSpeed = 1;
}
