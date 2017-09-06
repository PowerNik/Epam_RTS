using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecorationSettings
{
	public string name;
	public TileType placeHolderTile;

	public Material[] decorMaterials;
	public GameObject[] decorPrefabs;

	public GeneratorSettings genSets;
	public LandscapeSettings landscapeSets;

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
}
