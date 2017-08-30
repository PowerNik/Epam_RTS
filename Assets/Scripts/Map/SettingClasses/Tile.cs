using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
	private LayerType layerType;

	[SerializeField]
	private Material defaultMaterial;
	[SerializeField]
	private GameObject decoration;
	[SerializeField]
	private GameObject obstacle;

	private bool isAllowTexturing = true;
	private bool isAllowScenery = true;
	private bool isAllowDynamic = true;

	private Allows allows;

	private Material newMaterial;


	public Tile(LayerType layerType, Allows allows)
	{
		this.layerType = layerType;
		this.allows = allows;

		isAllowTexturing = (allows.allowDecorate & AllowDecorateType.Texturing) == AllowDecorateType.Texturing;
		isAllowScenery = (allows.allowDecorate & AllowDecorateType.Scenery) == AllowDecorateType.Scenery;
		isAllowDynamic = (allows.allowDecorate & AllowDecorateType.Dynamic) == AllowDecorateType.Dynamic;
	}

	public LayerType GetLayerType()
	{
		return layerType;
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

	public bool IsAllowColoring() { return isAllowTexturing; }
	public bool IsAllowDecor() { return isAllowScenery; }
	public bool IsAllowObstacle() { return isAllowDynamic; }

	//TODO 
	//isBuildCitizen, isBuildFermers
	//isExtractCitizen, isExtractFermers

	public void ClearMaterial()
	{
		newMaterial = null;
	}

	public void ClearDecoration()
	{
		decoration = null;
	}

	public void ClearObstacle()
	{
		obstacle = null;
	}


	//TODO
	public static Tile operator +(Tile left, Tile right)
	{
		if (left.layerType != right.layerType)
		{
			return left;
		}

		if (left.isAllowScenery == true)
		{
			left.decoration = right.decoration;
		}

		if (left.isAllowDynamic == true)
		{
			left.obstacle = right.obstacle;
		}

		if (left.isAllowTexturing == true)
		{
			left.newMaterial = right.defaultMaterial;
		}

		return left;
	}
}
