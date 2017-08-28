using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Map/Map Settings", order = 0)]
public class MapSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettings mapSizeSetting;

	[Space(10)]
	[SerializeField]
	private LayerSettings layerSets;

	[Space(10)]
	[SerializeField]
	private BasePointSettings basePointSettings;

	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSetting;
	}

	public LayerSettings GetLayerSettings()
	{
		return layerSets;
	}

	public BasePointSettings GetBasePointSettings()
	{
		return basePointSettings;
	}
}
