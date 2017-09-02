using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	private TileType tileType = TileType.GroundLayer;
	private TileType layerType = TileType.GroundLayer;

	[SerializeField]
	private Material defaultMaterial;
	private Material newMaterial;

	[SerializeField]
	private GameObject scenery;
	[SerializeField]
	private GameObject dynamic;

	private Allows allows;


	public Tile(TileType tileType, TileType layerType)
	{
		this.tileType = tileType;
		this.layerType = layerType;
		allows = AllowsSettings.GetAllow(tileType);
	}

	public TileType GetLayerType()
	{
		return layerType;
	}

	public TileType GetTileType()
	{
		return tileType;
	}

	public void SetDefaultMaterial(Material mat)
	{
		if(defaultMaterial == null)
		{
			defaultMaterial = mat;
		}
	}

	public void SetNewMaterial(Material mat)
	{
		if((allows.allowDecorate & AllowDecorateType.Texturing) == AllowDecorateType.Texturing)
		{
			newMaterial = mat;
		}
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

	public bool IsAllowBuild(Race race)
	{
		if (race == Race.Citizen)
		{
			return (allows.allowBuild & AllowBuildType.Citizen) == AllowBuildType.Citizen;
		}
		else
		{
			return (allows.allowBuild & AllowBuildType.Fermers) == AllowBuildType.Fermers;
		}
	}

	public bool IsAllowExtract(Race race)
	{
		if (race == Race.Citizen)
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

	//TODO Nik
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
