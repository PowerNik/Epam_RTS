using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTileSettings
{
	public LayerTile groundTile = new LayerTile(TileType.GroundLayer, LayerType.Ground);
	public LayerTile waterTile = new LayerTile(TileType.WaterLayer, LayerType.Water);
	public LayerTile mountainTile = new LayerTile(TileType.MountainsLayer, LayerType.Mountain);

	public Dictionary<LayerType, LayerTile> GetLayerTileDictionary()
	{
		Dictionary<LayerType, LayerTile> tileDict = new Dictionary<LayerType, LayerTile>();

		tileDict.Add(groundTile.GetLayerType(), groundTile);
		tileDict.Add(waterTile.GetLayerType(), waterTile);
		tileDict.Add(mountainTile.GetLayerType(), mountainTile);

		return tileDict;
	}

	public LayerTile GetLayerTile(LayerType layerType)
	{
		return GetLayerTileDictionary()[layerType];
	}

	public GeneratorSettings GetGeneratorSettings(LayerType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetGeneratorSettings();
	}

	public MeshSettings GetMeshSettings(LayerType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetMeshSettings();
	}

	public LandscapeSettings GetLandscapeSettings(LayerType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetLandscapeSettings();
	}
}
