using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTile
{
	[SerializeField]
	private Tile tile;

	[SerializeField]
	private GeneratorSettings genSets;

	[SerializeField]
	private MeshSettings meshSets;

	[SerializeField]
	private LandscapeSettings landscapeSets;


	public LayerTile(TileType tileType, LayerType LayerType, Allows allows)
	{
		tile = new Tile(tileType, LayerType, allows);
	}

	public LayerType GetLayerType()
	{
		return tile.GetLayerType();
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

	public Material GetMaterial()
	{
		return tile.GetMaterial();
	}

	public Tile GetTile()
	{
		return tile;
	}

	public TileType GetTileType()
	{
		return tile.GetTileType();
	}
}
