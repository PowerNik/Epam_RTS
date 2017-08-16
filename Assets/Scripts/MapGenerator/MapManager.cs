using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	#region TileSettings

	[SerializeField]
	private TileSettingsSO tileSettings;

	public TileType[] GetTileTypes()
	{
		return tileSettings.GetTileTypes();
	}

	public Tile[] GetTiles()
	{
		return tileSettings.GetAllSettings();
	}

	#endregion

	#region MapLayerSettings

	[SerializeField]
	private MapSettingsSO MapLayerSettings;

	public MapLayer[] GetMapLayers()
	{
		return MapLayerSettings.GetMapLayers();
	}

	#endregion
}
