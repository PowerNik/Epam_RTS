using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTile
{
	[SerializeField]
	private Tile tile;

	private LayerType bandedLayerType = LayerType.Water;

	public FramingTile(TileType tileType, LayerType bandedLayerType)
	{
		tile = new Tile(tileType, LayerType.Ground);
		this.bandedLayerType = bandedLayerType;
	}

	public LayerType GetLayerType()
	{
		return tile.GetLayerType();
	}

	/// <summary>
	/// Области какого слоя обрамляет данный тайл
	/// </summary>
	/// <returns></returns>
	public LayerType GetBandedLayerType()
	{
		return bandedLayerType;
	}

	public Tile GetTile()
	{
		return tile;
	}

	public TileType GetTileType()
	{
		return tile.GetTileType();
	}
}
