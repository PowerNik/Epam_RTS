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

	[SerializeField]
	private MapSettingsManagerSO mapSetsManager;

	private GridManager gridManager;
	private MapCreator mapCreator;
	private MapSizeSettings mapSizeSets;

	private BuildAreaSelecter buildAreaSelecter;

	private void Awake()
	{
		SetParams();
		CreateMap();

		buildAreaSelecter = gameObject.AddComponent<BuildAreaSelecter>();
	}

	private void SetParams()
	{
		mapSizeSets = mapSetsManager.GetMapSizeSettings();

		MapWidth = mapSizeSets.width;
		MapLength = mapSizeSets.length;
		TileSize = mapSizeSets.tileSize;
		TileCountX = mapSizeSets.TileCountX;
		TileCountZ = mapSizeSets.TileCountZ;
	}

	private void CreateMap()
	{
		mapCreator = new MapCreator(mapSetsManager);
		gridManager = new GridManager(mapSetsManager);
		gridManager.SetLayerMap(mapCreator.LayerGrid, mapSetsManager.GetLayerTileSettings());
		gridManager.SetAllFramingTiles(mapSetsManager.GetFramingTileSettings().GetFramingTilePairs());

		GameObject mapGO = new GameObject();
		mapGO.name = "Map";

		CreateNavMesh(mapGO);
		mapCreator.CreateMapMesh(mapGO);
	}

	private void CreateNavMesh(GameObject mapGO)
	{
		GameObject mapNavMesh = new GameObject();
		mapNavMesh.transform.parent = mapGO.transform;
		mapNavMesh.name = "LocalNavMeshBuilder";

		LocalNavMeshBuilder lnmb = mapNavMesh.AddComponent<LocalNavMeshBuilder>();
		lnmb.transform.position += new Vector3(MapWidth / 2, 0, MapLength / 2);
		lnmb.m_Size = new Vector3(MapWidth, 20, MapLength);
	}

	#region FOR TEST selecting buildArea
	private void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1))
			SelectArea();
		if (Input.GetKeyUp(KeyCode.Alpha1))
			DeselectArea();
	}

	public void SelectArea()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			IsBuildableArea(GetTilePos(hit.point) + Vector3.up * 0.3f, 3.1f, 3f, Race.Fermer);
		}
	}

	public void DeselectArea()
	{
		buildAreaSelecter.DeselectBuildArea();
	}
	#endregion

	public Vector3 GetTilePos(Vector3 position)
	{
		return gridManager.GetTilePos(position);
	}

	public bool IsBuildableTile(Vector3 position, Race race)
	{
		return gridManager.IsBuildableTile(position, race);
	}

	public bool IsBuildableArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		// TODO Nik Cнять заглушку
		// 01.09.17. Сейчас недопилена постройка для горожан
		// Поэтому стоит заглушка
		return buildAreaSelecter.SelectBuildArea(pos, areaSizeX, areaSizeZ, Race.Fermer);
	}

	public Vector3 GetCitizenBasePoint()
	{
		return mapCreator.CitizenBasePoint;
	}

	public Vector3[] GetFermerBasePoints()
	{
		return mapCreator.FermerBasePoints;
	}
}
