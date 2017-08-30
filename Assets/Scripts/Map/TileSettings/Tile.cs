using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	private TileType tileType;
	private LayerType layerType;

	[SerializeField]
	private Material defaultMaterial;
	[SerializeField]
	private GameObject scenery;
	[SerializeField]
	private GameObject dynamic;

	private Allows allows;

	private Material newMaterial;


	public Tile(TileType tileType, LayerType layerType, Allows allows)
	{
		this.tileType = tileType;
		this.layerType = layerType;
		this.allows = allows;
	}

	public LayerType GetLayerType()
	{
		return layerType;
	}

	public TileType GetTileType()
	{
		return tileType;
	}

	public Material GetMaterial()
	{
		if (newMaterial == null)
		{
			return defaultMaterial;
		}
		else
		{
			return newMaterial;
		}
	}

	public bool IsAllowTexturing()
	{
		return (allows.allowDecorate & AllowDecorateType.Texturing) == AllowDecorateType.Texturing;
	}

	public bool IsAllowScenery()
	{
		return (allows.allowDecorate & AllowDecorateType.Scenery) == AllowDecorateType.Scenery;
	}

	public bool IsAllowDynamic()
	{
		return (allows.allowDecorate & AllowDecorateType.Dynamic) == AllowDecorateType.Dynamic;
	}

	//TODO Nik
	public bool IsAllowBuild(bool isCitizen)
	{
		if (isCitizen)
		{
			return (allows.allowBuild & AllowBuildType.Citizen) == AllowBuildType.Citizen;
		}
		else
		{
			return (allows.allowBuild & AllowBuildType.Fermers) == AllowBuildType.Fermers;
		}
	}

	//TODO Nik
	public bool IsAllowExtract(bool isCitizen)
	{
		if (isCitizen)
		{
			return (allows.allowExtract & AllowExtractType.Citizen) == AllowExtractType.Citizen;
		}
		else
		{
			return (allows.allowExtract & AllowExtractType.Fermers) == AllowExtractType.Fermers;
		}
	}


	public void ClearMaterial()
	{
		newMaterial = null;
	}

	public void ClearScenery()
	{
		scenery = null;
	}

	public void ClearDynamic()
	{
		dynamic = null;
	}

	public static Tile operator +(Tile left, Tile right)
	{
		if (left.layerType != right.layerType)
		{
			return left;
		}

		if (left.IsAllowScenery() == true)
		{
			left.scenery = right.scenery;
			if(right.IsAllowScenery() == false)
			{
				left.allows.allowDecorate -= AllowDecorateType.Scenery;
			}
		}

		if (left.IsAllowDynamic() == true)
		{
			left.dynamic = right.dynamic;
			if (right.IsAllowDynamic() == false)
			{
				left.allows.allowDecorate -= AllowDecorateType.Dynamic;
			}
		}

		if (left.IsAllowTexturing() == true)
		{
			left.newMaterial = right.defaultMaterial;
			if (right.IsAllowTexturing() == false)
			{
				left.allows.allowDecorate -= AllowDecorateType.Texturing;
			}
		}

		left.allows.allowBuild = right.allows.allowBuild;
		left.allows.allowExtract = right.allows.allowExtract;

		return left;
	}
}
