using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Decoration
{
	public string name;
	public GameObject[] decorPrefabs;

	public LayerType layer = LayerType.Ground;
	public GeneratorSettings genSets;
	public AreaSettings areaSets;

	public float minScale = 0.7f;
	public float maxScale = 1.5f;
}
