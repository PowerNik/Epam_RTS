using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTileSettings
{
	public FramingTile waterSideTile = new FramingTile(LayerType.Water);
	public FramingTile foothillTile = new FramingTile(LayerType.Mountain);

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
	public Dictionary<FramingTileType, LayerType> GetFramingTilePairs()
	{
		Dictionary<FramingTileType, LayerType> dict = new Dictionary<FramingTileType, LayerType>();

	/*	dict.Add(waterSideTile.tileType, waterSideTile.layerTileType);
		dict.Add(foothillTile.tileType, foothillTile.layerTileType);*/

		return dict;
	}
}
