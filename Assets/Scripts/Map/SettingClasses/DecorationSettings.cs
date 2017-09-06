using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecorationSettings
{
	public string name;
	[SerializeField]
	private TileType tileHolder;

	[SerializeField]
	private Material[] decorMaterials;
	[SerializeField]
	private GameObject[] decorPrefabs;

	[SerializeField]
	private GeneratorSettings genSets;
	[SerializeField]
	private LandscapeSettings landscapeSets;

	public float minScale = 0.7f;
	public float maxScale = 1.5f;

	public void SetMainSeed(string mainSeed)
	{
		genSets.SetMainSeed(mainSeed);
	}

	public string GetSeed()
	{
		return genSets.GetSeed();
	}


	public TileType GetTileHolder()
	{
		return tileHolder;
	}
	public Material[] GetMaterials()
	{
		return decorMaterials;
	}

	public GameObject[] GetDecorations()
	{
		return decorPrefabs;
	}

	public GeneratorSettings GetGeneratorSettings()
	{
		return genSets;
	}

	public LandscapeSettings GetLandscapeSettings()
	{
		return landscapeSets;
	}


}
