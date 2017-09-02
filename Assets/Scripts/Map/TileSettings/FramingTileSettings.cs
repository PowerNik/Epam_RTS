using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTileSettings
{
	public FramingTile waterSideTile = new FramingTile(TileType.FramingWaterSide, TileType.WaterLayer);
	public FramingTile foothillTile = new FramingTile(TileType.FramingFoothill, TileType.MountainsLayer);

	/// <summary>
	/// Возвращает пары (обрамляемый тип тайла, обрамляющий тайл)
	/// </summary>
	/// <returns></returns>
	public Dictionary<TileType, FramingTile> GetFramingTilePairs()
	{
		Dictionary<TileType, FramingTile> dict = new Dictionary<TileType, FramingTile>();

		dict.Add(waterSideTile.GetBandedLayerType(), waterSideTile);
		dict.Add(foothillTile.GetBandedLayerType(), foothillTile);

		return dict;
	}
}
