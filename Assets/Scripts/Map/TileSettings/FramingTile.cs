using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTile
{
	[SerializeField]
	private Tile tile;

	private TileType bandedLayerType = TileType.FramingWaterSide;

	public FramingTile(TileType tileType, TileType bandedLayerType)
	{
		tile = new Tile(tileType, TileType.GroundLayer);
		this.bandedLayerType = bandedLayerType;
	}

	public TileType GetLayerType()
	{
		return tile.GetLayerType();
	}

	/// <summary>
	/// Области какого слоя обрамляет данный тайл
	/// </summary>
	/// <returns></returns>
	public TileType GetBandedLayerType()
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
