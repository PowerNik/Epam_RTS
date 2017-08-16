using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MapSettings", menuName = "My Scriptable Objects/Map Settings", order = 1)]
public class MapSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapGeneratorSettings mapGenSet;

	[Space(15)]
	[SerializeField]
	private MapLayer[] mapLayers;

	public MapGeneratorSettings GetMapGeneratorSettings()
	{
		return mapGenSet;
	}

	public MapLayer[] GetMapLayers()
	{
		return mapLayers;
	}
}
