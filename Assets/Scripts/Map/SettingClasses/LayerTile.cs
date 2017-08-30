using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTile
{
	public Tile tile;

	public LayerTile(LayerType type, Allows allows)
	{
		tile = new Tile(type, allows);
	}

	public LayerType GetLayerType()
	{
		return tile.GetLayerType();
	}

	public GeneratorSettings genSets;
	public MeshSettings meshSets;

	public LandscapeArea landscapeArea;
}
