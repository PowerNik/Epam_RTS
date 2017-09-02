using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTileSettings
{
	public LayerTile groundTile = new LayerTile(TileType.GroundLayer, TileType.GroundLayer);
	public LayerTile waterTile = new LayerTile(TileType.WaterLayer, TileType.WaterLayer);
	public LayerTile mountainTile = new LayerTile(TileType.MountainsLayer, TileType.MountainsLayer);

	public Dictionary<TileType, LayerTile> GetLayerTileDictionary()
	{
		Dictionary<TileType, LayerTile> tileDict = new Dictionary<TileType, LayerTile>();

		tileDict.Add(groundTile.GetLayerType(), groundTile);
		tileDict.Add(waterTile.GetLayerType(), waterTile);
		tileDict.Add(mountainTile.GetLayerType(), mountainTile);

		return tileDict;
	}

	public Dictionary<TileType, Tile> GetTileDictionary()
	{
		Dictionary<TileType, Tile> tileDict = new Dictionary<TileType, Tile>();

		tileDict.Add(groundTile.GetTileType(), groundTile.GetTile());
		tileDict.Add(waterTile.GetTileType(), waterTile.GetTile());
		tileDict.Add(mountainTile.GetTileType(), mountainTile.GetTile());

		return tileDict;
	}

	public LayerTile GetLayerTile(TileType layerType)
	{
		return GetLayerTileDictionary()[layerType];
	}

	public GeneratorSettings GetGeneratorSettings(TileType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetGeneratorSettings();
	}

	public MeshSettings GetMeshSettings(TileType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetMeshSettings();
	}

	public LandscapeSettings GetLandscapeSettings(TileType layerType)
	{
		return GetLayerTileDictionary()[layerType].GetLandscapeSettings();
	}
}
