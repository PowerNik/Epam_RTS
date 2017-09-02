using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTile
{
	private Tile tile;

	private TileType bandedLayerType = TileType.FramingWaterSide;

	public void SetBandedTile(TileType tileType, TileType bandedLayerType)
	{
		tile = new Tile(tileType, TileType.GroundLayer);
		this.bandedLayerType = bandedLayerType;
	}

	public void SetMaterial(Material mat)
	{
		tile.SetDefaultMaterial(mat);
	}

	public Material GetMaterial()
	{
		return tile.GetMaterial();
	}

	public TileType GetTileType()
	{
		return tile.GetTileType();
	}

	public TileType GetLayerType()
	{
		return tile.GetLayerType();
	}

	public Tile GetTile()
	{
		return tile;
	}

	/// <summary>
	/// Области какого слоя обрамляет данный тайл
	/// </summary>
	/// <returns></returns>
	public TileType GetBandedLayerType()
	{
		return bandedLayerType;
	}
}
