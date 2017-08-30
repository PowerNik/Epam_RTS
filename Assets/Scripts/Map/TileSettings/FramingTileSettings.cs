using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTileSettings
{
	public FramingTile waterSideTile = new FramingTile(TileType.FramingWaterSide, LayerType.Water);
	public FramingTile foothillTile = new FramingTile(TileType.FramingFoothill, LayerType.Mountain);

	/// <summary>
	/// Возвращает пары (обрамляемый тип тайла, обрамляющий тайл)
	/// </summary>
	/// <returns></returns>
	public Dictionary<LayerType, FramingTile> GetFramingTilePairs()
	{
		Dictionary<LayerType, FramingTile> dict = new Dictionary<LayerType, FramingTile>();

		dict.Add(waterSideTile.GetBandedLayerType(), waterSideTile);
		dict.Add(foothillTile.GetBandedLayerType(), foothillTile);

		return dict;
	}
}
