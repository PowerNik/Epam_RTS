using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FramingTileSettings
{
	[SerializeField]
	private Material waterSideMaterial;
	[SerializeField]
	private FramingTile waterSideTile;

	[Space(10)]
	[SerializeField]
	private Material foothillMaterial;
	[SerializeField]
	private FramingTile foothillTile;

	/// <summary>
	/// Возвращает пары (обрамляемый тип тайла, обрамляющий тайл)
	/// </summary>
	/// <returns></returns>
	public Dictionary<TileType, FramingTile> GetFramingTilePairs()
	{
		Dictionary<TileType, FramingTile> dict = new Dictionary<TileType, FramingTile>();

		waterSideTile.SetBandedTile(TileType.FramingWaterSide, TileType.WaterLayer);
		waterSideTile.SetMaterial(waterSideMaterial);
		dict.Add(waterSideTile.GetBandedLayerType(), waterSideTile);

		foothillTile.SetBandedTile(TileType.FramingFoothill, TileType.MountainLayer);
		foothillTile.SetMaterial(foothillMaterial);
		dict.Add(foothillTile.GetBandedLayerType(), foothillTile);

		return dict;
	}
}
