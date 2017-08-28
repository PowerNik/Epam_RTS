using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerSettings
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

	public BasicTile GetBasicTile(LayerType layerType)
	{
		var dict = basicTileSets.GetBasicTileDictionary();
		BasicTile res = dict[BasicTileType.Ground];

		switch (layerType)
		{
			case LayerType.Water:
				res = dict[BasicTileType.Water];
				break;

			case LayerType.Mountain:
				res = dict[BasicTileType.Mountain];
				break;
		}

		return res;
	}

	public BasicTile[] GetBasicTiles()
	{
		return basicTileSets.GetBasicTiles();
	}

	public GeneratorSettings GetGeneratorSettings(LayerType layerType)
	{
		GeneratorSettings res;
		switch (layerType)
		{
			case LayerType.Water:
				res = waterGenSets;
				break;

			case LayerType.Mountain:
				res = mountainGenSets;
				break;

			case LayerType.Ground:
			default:
				res = new GeneratorSettings();
				break;
		}

		return res;
	}

	public MeshSettings GetMeshSettings(LayerType layerType)
	{
		MeshSettings res;
		switch (layerType)
		{
			case LayerType.Water:
				res = waterMeshSets;
				break;

			case LayerType.Mountain:
				res = mountainMeshSets;
				break;

			case LayerType.Ground:
			default:
				res = groundMeshSets;
				break;
		}

		return res;
	}

	public AreaSettings GetAreaSettings(LayerType layerType)
	{
		AreaSettings res;
		switch (layerType)
		{
			case LayerType.Water:
				res = waterAreaSets;
				break;

			case LayerType.Mountain:
				res = mountainAreaSets;
				break;

			case LayerType.Ground:
			default:
				res = new AreaSettings();
				break;
		}

		return res;
	}
}
