using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerTileSettings", menuName = "MapSettings/LayerTileSettings", order = 4)]
public class LayerTileSettingsSO : ScriptableObject
{
	[SerializeField]
	private LayerTileSettings layerTileSettings;

	public LayerTileSettings GetLayerTileSettings()
	{
		return layerTileSettings;
	}
}
