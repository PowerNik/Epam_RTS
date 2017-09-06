using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerSettings", menuName = "MapSettings/LayerSettings", order = 4)]
public class LayerSettingsSO : ScriptableObject
{
	[SerializeField]
	private LayerSettings layerSettings = new LayerSettings();

	public LayerSettings GetLayerSettings()
	{
		return layerSettings;
	}
}
