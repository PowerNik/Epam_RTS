using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public int MapWidth { get { return mapSizeSets.width; } }
	public int MapLength { get { return mapSizeSets.length; } }

	[SerializeField]
	private MapSettingsManagerSO mapSetsManager;

	//TODO FOR TEST selecting
	private LayerMask layerToRay;

	private GridManager gridManager;
	private MapCreator mapCreator;
	private MapSizeSettings mapSizeSets;

	private BuildAreaSelecter buildAreaSelecter;

	private void Awake()
	{
		//TODO FOR TEST selecting
		layerToRay = GameObject.Find("MinimapCamera").GetComponent<MinimapCamera>().GetLayerToRay();

		mapSizeSets = mapSetsManager.GetMapSizeSettings();
		CreateMap();

		buildAreaSelecter = gameObject.AddComponent<BuildAreaSelecter>();
		buildAreaSelecter.SetGridManager(gridManager);
	}

	private void CreateMap()
	{
		gridManager = new GridManager(mapSetsManager.GetMapSizeSettings());
		mapCreator = new MapCreator(mapSetsManager, ref gridManager.tileGrid);

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

	#region FOR TEST selecting Area
	private void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1))
			SelectBuildArea(Race.Fermer);
		if (Input.GetKeyUp(KeyCode.Alpha1))
			DeselectArea();

		if (Input.GetKey(KeyCode.Alpha2))
			SelectBuildArea(Race.Citizen);
		if (Input.GetKeyUp(KeyCode.Alpha2))
			DeselectArea();

		if (Input.GetKey(KeyCode.Alpha3))
			SelectExtractArea(Race.Fermer);
		if (Input.GetKeyUp(KeyCode.Alpha3))
			DeselectArea();

		if (Input.GetKey(KeyCode.Alpha4))
			SelectExtractArea(Race.Citizen);
		if (Input.GetKeyUp(KeyCode.Alpha4))
			DeselectArea();
	}

	public void SelectBuildArea(Race race)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000, layerToRay))
		{
			IsBuildableArea(GetTilePos(hit.point) + Vector3.up * 1.3f, 3.1f, 3f, race);
		}
	}

	public void SelectExtractArea(Race race)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000, layerToRay))
		{
			IsExtractableArea(GetTilePos(hit.point) + Vector3.up * 1.3f, 2.5f, 3f, race);
		}
	}
	#endregion

	public Vector3 GetTilePos(Vector3 position)
	{
		return gridManager.GetTilePos(position);
	}

	public bool IsBuildableArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		return buildAreaSelecter.SelectBuildArea(pos, areaSizeX, areaSizeZ, race);
	}

	public bool IsExtractableArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		return buildAreaSelecter.SelectExtractArea(pos, areaSizeX, areaSizeZ, race);
	}

	public void DeselectArea()
	{
		buildAreaSelecter.DeselectArea();
	}

	#region GetMainPointPositions

	public Vector3 GetCitizenBasePoint()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Base, Race.Citizen)[0];
	}

	public Vector3[] GetFermerBasePoints()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Base, Race.Fermer);
	}

	public Vector3[] GetCitizenExtractPoitns()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Extract, Race.Citizen);
	}

	public Vector3[] GetFermerExtractPoitns()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Extract, Race.Fermer);
	}

	public Vector3[] GetTradePoitns()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Trade, Race.Citizen);
	}

	public Vector3[] GetAgressiveNeutralPoitns()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Neutral, Race.Citizen);
	}

	public Vector3[] GetPeacefulNeutralPoitns()
	{
		return mapCreator.mainPointsCreator.MainPointPositions(MainPointType.Neutral, Race.Fermer);
	}

	#endregion
}
