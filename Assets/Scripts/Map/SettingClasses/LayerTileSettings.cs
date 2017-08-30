using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LayerTileSettings
{
	public LayerTile groundTile = new LayerTile(LayerType.Ground, AllowsSettings.LayerGround);
	public LayerTile waterTile = new LayerTile(LayerType.Water, AllowsSettings.LayerNoGround);
	public LayerTile mountainTile = new LayerTile(LayerType.Mountain, AllowsSettings.LayerNoGround);

	public Dictionary<LayerType, LayerTile> GetLayerTileDictionary()
	{
		Dictionary<LayerType, LayerTile> tileDict = new Dictionary<LayerType, LayerTile>();

		tileDict.Add(groundTile.GetLayerType(), groundTile);
		tileDict.Add(waterTile.GetLayerType(), waterTile);
		tileDict.Add(mountainTile.GetLayerType(), mountainTile);

		return tileDict;
	}
}
