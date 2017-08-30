using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerSettings
{
	public LayerTileSettingsSO layerTileSets;

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

	public LayerTile GetLayerLayerTile(LayerType layerType)
	{
		var dict = layerTileSets.GetLayerTileDictionary();
		LayerTile res = dict[LayerType.Ground];

		switch (layerType)
		{
			case LayerType.Water:
				res = dict[LayerType.Water];
				break;

			case LayerType.Mountain:
				res = dict[LayerType.Mountain];
				break;
		}

		return res;
	}

	public GeneratorSettings GetGeneratorSettings(LayerType layerType)
	{
		GeneratorSettings res = new GeneratorSettings();
		switch (layerType)
		{
			case LayerType.Water:
				res = waterGenSets;
				break;

			case LayerType.Mountain:
				res = mountainGenSets;
				break;
		}

		return res;
	}

	public MeshSettings GetMeshSettings(LayerType layerType)
	{
		MeshSettings res = groundMeshSets;
		switch (layerType)
		{
			case LayerType.Water:
				res = waterMeshSets;
				break;

			case LayerType.Mountain:
				res = mountainMeshSets;
				break;
		}

		return res;
	}

	public AreaSettings GetAreaSettings(LayerType layerType)
	{
		AreaSettings res = new AreaSettings();
		switch (layerType)
		{
			case LayerType.Water:
				res = waterAreaSets;
				break;

			case LayerType.Mountain:
				res = mountainAreaSets;
				break;
		}

		return res;
	}
}
