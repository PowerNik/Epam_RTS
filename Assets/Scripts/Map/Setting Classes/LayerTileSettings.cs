using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTileSettings
{
	public LayerTile groundTile = new LayerTile(LayerTileType.Ground);
	public LayerTile waterTile = new LayerTile(LayerTileType.Water);
	public LayerTile mountainTile = new LayerTile(LayerTileType.Mountain);

	public Dictionary<LayerTileType, LayerTile> GetLayerTileDictionary()
	{
		Dictionary<LayerTileType, LayerTile> tileDict = new Dictionary<LayerTileType, LayerTile>();

		tileDict.Add(LayerTileType.Ground, groundTile);
		tileDict.Add(LayerTileType.Water, waterTile);
		tileDict.Add(LayerTileType.Mountain, mountainTile);

		return tileDict;
	}
}
