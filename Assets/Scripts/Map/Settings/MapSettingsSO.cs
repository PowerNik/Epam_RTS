using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapSettings", menuName = "Map/Map Settings", order = 1)]
public class MapSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapGeneratorSettings mapGenSet;

	[Space(15)]
	[SerializeField]
	private MapLayer[] mapLayers;

	[Space(15)]
	[SerializeField]
	private BasePointSettings basePointSettings;

	public MapGeneratorSettings MapGeneratorSettings()
	{
		return mapGenSet;
	}

	public MapLayer[] MapLayers()
	{
		return mapLayers;
	}

	public BasePointSettings BasePointSettings()
	{
		return basePointSettings;
	}
}
