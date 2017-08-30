using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Map/Map Settings", order = 20)]
public class MapSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettings mapSizeSetting;

	[Space(10)]
	[SerializeField]
	private LayerSettings layerSets;

	[Space(10)]
	[SerializeField]
	private BasePointsSettings basePointSettings;

	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSetting;
	}

	public LayerSettings GetLayerSettings()
	{
		return layerSets;
	}

	public BasePointsSettings GetBasePointSettings()
	{
		return basePointSettings;
	}
}
