using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainPointTile
{
	[SerializeField]
	private Tile tile;

	[SerializeField]
	private DomainSettings domainSets;

	public MainPointTile(TileType tileType, LayerType LayerType = LayerType.Ground)
	{
		tile = new Tile(tileType, LayerType);
	}

	public void SetMaterial(Material mat)
	{
		tile.SetDefaultMaterial(mat);
	}

	public Material GetMaterial()
	{
		return tile.GetMaterial();
	}

	public void SetDomainSettings(DomainSettings domainSets)
	{
		this.domainSets = domainSets;
	}

	public DomainSettings GetDomainSettings()
	{
		return domainSets;
	}

	public Tile GetTile()
	{
		return tile;
	}

	public TileType GetTileType()
	{
		return tile.GetTileType();
	}

	public LayerType GetLayerType()
	{
		return tile.GetLayerType();
	}
}
