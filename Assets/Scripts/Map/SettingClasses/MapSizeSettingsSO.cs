using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapSizeSettings", menuName = "MapSettings/MapSizeSettings", order = 0)]
public class MapSizeSettingsSO : ScriptableObject
{
	[SerializeField]
	private MapSizeSettings mapSizeSettings;

	public MapSizeSettings GetMapSizeSettings()
	{
		return mapSizeSettings;
	}
}
