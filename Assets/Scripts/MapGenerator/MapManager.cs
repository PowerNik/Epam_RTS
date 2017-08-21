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
		if (genSets == null)
		{
			genSets = mapSettings.MapGeneratorSettings();
		}
		return genSets;
	}

	public MapLayer[] GetMapLayers()
	{
		return mapSettings.MapLayers();
	}

	#endregion


	[SerializeField]
	private GameObject prefabMap;

	private MapCreator mapCreator;
	private MapGeneratorSettings genSets;
	public TileGrid TileGrid { get; private set; }

	private void Awake()
	{
		SceneManagerRTS.MapManager = this;
	}

	private void Start()
	{
		GameObject go = Instantiate(prefabMap);

		go.AddComponent<LocalNavMeshBuilder>();
		LocalNavMeshBuilder lnmb = go.GetComponent<LocalNavMeshBuilder>();
		lnmb.m_Size = new Vector3(200, 200, 200);

		GetMapGeneratorSettings();
		mapCreator = new MapCreator(genSets, GetMapLayers(), go);

		TileGrid = mapCreator.TileGrid;
	}

	public Vector3 GetTilePos(Vector3 position)
	{
		float tileSize = genSets.tileSize;
		float x = position.x - position.x % tileSize + tileSize / 2f;
		if(position.x < 0)
		{
			x -= tileSize;
		}

		float z = position.z - position.z % tileSize + tileSize / 2f;
		if (position.z < 0)
		{
			z -= tileSize;
		}
		return new Vector3(x, position.y, z);
	}
}
