using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	public string TileName = "Ground";
	public TileType TileType = TileType.Ground;

	public Material material;

	public bool isAllowBuild = true;
	public bool isAllowMove = true;
	public bool isAllowFly = true;

	public float moveSpeed = 1;
}
