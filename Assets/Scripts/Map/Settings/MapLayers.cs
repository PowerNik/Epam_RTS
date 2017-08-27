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
	public MeshSettings groundMeshSets;

	[Space(5)]
	public TileType waterTileType = TileType.Water;
	public GeneratorSettings waterGenSets;
	public MeshSettings waterMeshSets;

	[Space(5)]
	public TileType mountainTileType = TileType.Mountain;
	public GeneratorSettings mountainGenSets;
	public MeshSettings mountainMeshSets;

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

	public MeshSettings GetMeshSettings(MapLayerType layerType)
	{
		MeshSettings res = groundMeshSets;
		switch (layerType)
		{
			case MapLayerType.LayerGround:
				res = groundMeshSets;
				break;

			case MapLayerType.LayerWater:
				res = waterMeshSets;
				break;

			case MapLayerType.LayerMountain:
				res = mountainMeshSets;
				break;
		}

		return res;
	}

	public GeneratorSettings GetGeneratorSettings(MapLayerType layerType)
	{
		GeneratorSettings res = waterGenSets;
		switch (layerType)
		{
			case MapLayerType.LayerWater:
				res = waterGenSets;
				break;

			case MapLayerType.LayerMountain:
				res = mountainGenSets;
				break;
		}

		return res;
	}
}
