using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Связывает тайлы и префабы со слоем, на котором они будут применяться
/// </summary>
[System.Serializable]
public class MapLayers
{
	public TileType groundTileType = TileType.Ground;

	[Space(5)]
	public TileType waterTileType = TileType.Water;
	public GeneratorSettings waterGenSets;

	[Space(5)]
	public TileType mountainTileType = TileType.Mountain;
	public GeneratorSettings mountainGenSets;

	public TileType GetTileType(MapLayerType layerType)
	{
		TileType res = groundTileType;
		switch (layerType)
		{
			case MapLayerType.LayerGround:
				res = groundTileType;
				break;

			case MapLayerType.LayerWater:
				res = waterTileType;
				break;

			case MapLayerType.LayerMountain:
				res = mountainTileType;
				break;
		}

		return res;
	}
}
