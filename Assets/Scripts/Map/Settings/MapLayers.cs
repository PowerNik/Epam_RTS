using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Связывает тайлы и префабы со слоем, на котором они будут применяться
/// </summary>
[System.Serializable]
public class MapLayers
{
	public BasicTileSettingsSO basicTileSets;

	[Space(10)]
	public MeshSettings groundMeshSets;

	[Space(5)]
	public GeneratorSettings waterGenSets;
	public MeshSettings waterMeshSets;
	public AreaSettings waterAreaSets;

	[Space(5)]
	public GeneratorSettings mountainGenSets;
	public MeshSettings mountainMeshSets;
	public AreaSettings mountainAreaSets;
	public int mountainBorderWidth = 1;

	public BasicTile GetBasicTile(MapLayerType layerType)
	{
		var dict = basicTileSets.GetBasicTileDictionary();
		BasicTile res = dict[BasicTileType.Ground];

		switch (layerType)
		{
			case MapLayerType.Water:
				res = dict[BasicTileType.Water];
				break;

			case MapLayerType.Mountain:
				res = dict[BasicTileType.Mountain];
				break;
		}

		return res;
	}

	public BasicTile[] GetBasicTiles()
	{
		return basicTileSets.GetBasicTiles();
	}

	public GeneratorSettings GetGeneratorSettings(MapLayerType layerType)
	{
		GeneratorSettings res;
		switch (layerType)
		{
			case MapLayerType.Water:
				res = waterGenSets;
				break;

			case MapLayerType.Mountain:
				res = mountainGenSets;
				break;

			case MapLayerType.Ground:
			default:
				res = new GeneratorSettings();
				break;
		}

		return res;
	}

	public MeshSettings GetMeshSettings(MapLayerType layerType)
	{
		MeshSettings res;
		switch (layerType)
		{
			case MapLayerType.Water:
				res = waterMeshSets;
				break;

			case MapLayerType.Mountain:
				res = mountainMeshSets;
				break;

			case MapLayerType.Ground:
			default:
				res = groundMeshSets;
				break;
		}

		return res;
	}

	public AreaSettings GetAreaSettings(MapLayerType layerType)
	{
		AreaSettings res;
		switch (layerType)
		{
			case MapLayerType.Water:
				res = waterAreaSets;
				break;

			case MapLayerType.Mountain:
				res = mountainAreaSets;
				break;

			case MapLayerType.Ground:
			default:
				res = new AreaSettings();
				break;
		}

		return res;
	}
}
