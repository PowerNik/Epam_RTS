using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerTileSettings", menuName = "MapSettings/LayerTileSettings", order = 4)]
public class LayerTileSettingsSO : ScriptableObject
{
	[SerializeField]
	private LayerTileSettings layerTileSettings = new LayerTileSettings();

	public LayerTileSettings GetLayerTileSettings()
	{
		return layerTileSettings;
	}

	public Dictionary<LayerType, LayerTile> GetLayerTileDictionary()
	{
		return layerTileSettings.GetLayerTileDictionary();
	}
}
