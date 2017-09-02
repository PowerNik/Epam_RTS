using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTile
{
	private Tile tile;

	[SerializeField]
	private GeneratorSettings genSets;

	[SerializeField]
	private MeshSettings meshSets;

	[SerializeField]
	private LandscapeSettings landscapeSets;

	public void SetTileType(TileType tileType, TileType layerType = TileType.GroundLayer)
	{
		tile = new Tile(tileType, layerType);
	}

	public void SetSeed(string seed)
	{
		genSets.SetSeed(seed);
	}

	public GeneratorSettings GetGeneratorSettings()
	{
		return genSets;
	}

	public MeshSettings GetMeshSettings()
	{
		return meshSets;
	}

	public LandscapeSettings GetLandscapeSettings()
	{
		return landscapeSets;
	}

	public void SetMaterial(Material mat)
	{
		tile.SetDefaultMaterial(mat);
	}

	public Material GetMaterial()
	{
		return tile.GetMaterial();
	}

	public TileType GetTileType()
	{
		return tile.GetTileType();
	}

	public TileType GetLayerType()
	{
		return tile.GetLayerType();
	}

	public Tile GetTile()
	{
		return tile;
	}
}
