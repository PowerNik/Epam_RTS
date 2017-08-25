using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Map/Map Settings", order = 1)]
public class MapSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettings mapSizeSetting;

	[Space(10)]
	[SerializeField]
	private MapLayers mapLayers;

	[Space(10)]
	[SerializeField]
	private BasePointSettings basePointSettings;

	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSetting;
	}

	public MapLayers GetMapLayers()
	{
		return mapLayers;
	}

	public BasePointSettings GetBasePointSettings()
	{
		return basePointSettings;
	}
}
