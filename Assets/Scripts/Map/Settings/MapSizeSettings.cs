using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapSizeSettings{

	public int width = 200;
	public int length = 200;

	[Range(0.2f, 1)]
	public float tileSize = 1;

	public int TileCountX { get { return (int)(width / tileSize); } }
	public int TileCountZ { get { return (int)(length / tileSize); } }
}
