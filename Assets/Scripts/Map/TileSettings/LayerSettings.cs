using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerSettings
{
	[SerializeField]
	private string seed = "main";

	[Space(15)]
	[SerializeField]
	private Material groundMaterial;
	[SerializeField]
	private LayerTile groundTile;

	[Space(10)]
	[SerializeField]
	private Material waterMaterial;
	[SerializeField]
	private Material waterFramingMaterial;
	[SerializeField]
	private LayerTile waterTile;

	[Space(10)]
	[SerializeField]
	private Material mountainMaterial;
	[SerializeField]
	private Material mountainFramingMaterial;
	[SerializeField]
	private LayerTile mountainTile;

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

	public LayerTile GetLayerTile(TileType layerType)
	{
		return GetLayerTileDictionary()[layerType];
	}

	public Dictionary<TileType, LayerTile> GetLayerTileDictionary()
	{
		Dictionary<TileType, LayerTile> tileDict = new Dictionary<TileType, LayerTile>();

		groundTile.SetTileType(TileType.GroundLayer, TileType.GroundLayer);
		groundTile.SetMaterial(groundMaterial);
		groundTile.SetSeed(seed);
		tileDict.Add(groundTile.GetLayerType(), groundTile);

		waterTile.SetTileType(TileType.WaterLayer, TileType.WaterLayer);
		waterTile.SetMaterial(waterMaterial);
		waterTile.SetSeed(seed);
		tileDict.Add(waterTile.GetLayerType(), waterTile);

		mountainTile.SetTileType(TileType.MountainLayer, TileType.MountainLayer);
		mountainTile.SetMaterial(mountainMaterial);
		mountainTile.SetSeed(seed);
		tileDict.Add(mountainTile.GetLayerType(), mountainTile);

		return tileDict;
	}

	/// <summary>
	/// Возвращает пары (обрамляемый тип тайла, обрамляющий тайл)
	/// </summary>
	/// <returns></returns>
	public Dictionary<TileType, FramingTile> GetFramingTilePairs()
	{
		Dictionary<TileType, FramingTile> dict = new Dictionary<TileType, FramingTile>();

		FramingTile waterFramingTile = new FramingTile();
		waterFramingTile.SetBandedTile(TileType.WaterFraming, TileType.WaterLayer);
		waterFramingTile.SetMaterial(waterFramingMaterial);
		dict.Add(waterFramingTile.GetBandedLayerType(), waterFramingTile);

		FramingTile mountainFramingTile = new FramingTile();
		mountainFramingTile.SetBandedTile(TileType.MountainFraming, TileType.MountainLayer);
		mountainFramingTile.SetMaterial(mountainFramingMaterial);
		dict.Add(mountainFramingTile.GetBandedLayerType(), mountainFramingTile);

		return dict;
	}
}
