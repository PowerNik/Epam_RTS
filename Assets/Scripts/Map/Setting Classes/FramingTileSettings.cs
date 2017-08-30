using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTileSettings
{
	public FramingTile waterSideTile = new FramingTile(FramingTileType.WaterSide, LayerTileType.Water);
	public FramingTile foothillTile = new FramingTile(FramingTileType.Foothill, LayerTileType.Mountain);

	public Dictionary<FramingTileType, FramingTile> GetFramingTileDictionary()
	{
		Dictionary<FramingTileType, FramingTile> tileDict = new Dictionary<FramingTileType, FramingTile>();

		tileDict.Add(FramingTileType.WaterSide, waterSideTile);
		tileDict.Add(FramingTileType.Foothill, foothillTile);

		return tileDict;
	}

	/// <summary>
	/// Возвращает пары (обрамляющий тип тайла, обрамляемый тип тайла)
	/// </summary>
	/// <returns></returns>
	public Dictionary<FramingTileType, LayerTileType> GetFramingTilePairs()
	{
		Dictionary<FramingTileType, LayerTileType> dict = new Dictionary<FramingTileType, LayerTileType>();

		dict.Add(waterSideTile.tileType, waterSideTile.layerTileType);
		dict.Add(foothillTile.tileType, foothillTile.layerTileType);

		return dict;
	}
}
