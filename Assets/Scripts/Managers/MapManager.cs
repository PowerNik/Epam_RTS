using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public int MapWidth { get; private set; }
	public int MapLength { get; private set; }

	public int TileCountX { get; private set; }
	public int TileCountZ { get; private set; }
	public float TileSize { get; private set; }

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


	[SerializeField]
	private MapSettingsSO mapSettings;

	[SerializeField]
	private GameObject prefabMap;

	private GridManager gridManager;
	private MapCreator mapCreator;
	private MapGeneratorSettings genSets;

	private void Awake()
	{
		//SceneManagerRTS.MapManager = this;
		genSets = mapSettings.MapGeneratorSettings();

		MapWidth = genSets.width;
		MapLength = genSets.length;
		TileSize = genSets.tileSize;
	}

	private void Start()
	{
		GameObject go = Instantiate(prefabMap);

		go.AddComponent<LocalNavMeshBuilder>();
		LocalNavMeshBuilder lnmb = go.GetComponent<LocalNavMeshBuilder>();
		lnmb.m_Size = new Vector3(200, 200, 200);

		mapCreator = new MapCreator(genSets, mapSettings.MapLayers(), go);
		gridManager = GameManager.Instance.GetComponent<GridManager>();

		TileGrid tileGrid = mapCreator.TileGrid;
		gridManager.SetTileGrid(tileGrid);

		TileCountX = tileGrid.countX;
		TileCountZ = tileGrid.countZ;
	}

	public Vector3 GetTilePos(Vector3 position)
	{
		return gridManager.GetTilePos(position);
	}
	public bool IsBuildableTile(Vector3 position)
	{
		return gridManager.IsBuildableTile(position);
	}

}
