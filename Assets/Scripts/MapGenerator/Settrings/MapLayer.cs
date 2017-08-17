using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Связывает тайлы и префабы со слоем, на котором они будут применяться
/// </summary>
[System.Serializable]
public class MapLayer
{
	public string name = "Ground";
	public MapLayerType mapLayerType = MapLayerType.LayerGround;
	public TileType tileType = TileType.Ground;
	
	public GameObject[] prefabs;
}
