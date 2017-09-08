using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainPointTile
{
	private Tile tile;

	[SerializeField]
	private DomainSettings domainSets;

	public MainPointTile(TileType tileType, TileType layerType = TileType.GroundLayer)
	{
		tile = new Tile(tileType, layerType);
	}

	public void SetDomainSettings(DomainSettings domainSets)
	{
		this.domainSets = domainSets;
	}

	public DomainSettings GetDomainSettings()
	{
		return domainSets;
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
