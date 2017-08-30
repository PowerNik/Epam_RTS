using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTile
{
	public Tile tile;

	public FramingTile(LayerType layerTileType)
	{
		tile = new Tile(LayerType.Ground, AllowsSettings.Framing);
		this.layerTileType = layerTileType;
	}

	[HideInInspector]
	public LayerType layerTileType = LayerType.Water;

}
