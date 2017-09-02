using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecorationSettings
{
	public string name;
	public GameObject[] decorPrefabs;

	public GeneratorSettings genSets;
	public LandscapeSettings landscapeSets;

	public float minScale = 0.7f;
	public float maxScale = 1.5f;
}
