using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerSettings", menuName = "MapSettings/LayerSettings", order = 2)]
public class LayerSettingsSO : ScriptableObject
{
	[SerializeField]
	private LayerSettings layerSettings;

	public LayerSettings GetLayerSettings()
	{
		return layerSettings;
	}
}
