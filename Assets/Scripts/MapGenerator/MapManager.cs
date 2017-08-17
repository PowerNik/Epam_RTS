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
	private MapSettingsSO mapSettings;

	public MapGeneratorSettings GetMapGeneratorSettings()
	{
		return mapSettings.MapGeneratorSettings();
	}

	public MapLayer[] GetMapLayers()
	{
		return mapSettings.MapLayers();
	}

	#endregion


	[SerializeField]
	private GameObject prefabMap;

	private MapCreator mapCreator;

	public TileGrid TileGrid { get; private set; }

	private void Awake()
	{
		SceneManagerRTS.MapManager = this;
	}

	private void Start()
	{
		GameObject go = Instantiate(prefabMap);

		mapCreator = new MapCreator(GetMapGeneratorSettings(), GetMapLayers(), go);
		TileGrid = mapCreator.TileGrid;
	}
}
