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
	private TileSettingsSO tileSettings;

	public TileType[] GetTileTypes()
	{
		return tileSettings.GetTileTypes();
	}

	public Tile[] GetTiles()
	{
		return tileSettings.GetAllSettings();
	}


	[SerializeField]
	private MapSettingsSO mapSettings;

	[SerializeField]
	private GameObject prefabMap;

	private GridManager gridManager;
	private MapCreator mapCreator;
	private MapSizeSettings mapSizeSets;

	private BuildAreaSelecter tileSelecter;

	private void Awake()
	{
		SetParams();
		CreateMap();

		tileSelecter = gameObject.AddComponent<BuildAreaSelecter>();
	}

	private void SetParams()
	{
		mapSizeSets = mapSettings.GetMapSizeSettings();

		MapWidth = mapSizeSets.width;
		MapLength = mapSizeSets.length;
		TileSize = mapSizeSets.tileSize;
		TileCountX = mapSizeSets.TileCountX;
		TileCountZ = mapSizeSets.TileCountZ;
	}

	private void CreateMap()
	{
		mapCreator = new MapCreator(mapSettings);
		gridManager = new GridManager(mapSizeSets);

		GameObject mapGO = Instantiate(prefabMap);

		mapGO.AddComponent<LocalNavMeshBuilder>();
		LocalNavMeshBuilder lnmb = mapGO.GetComponent<LocalNavMeshBuilder>();
		lnmb.m_Size = new Vector3(200, 200, 200);
		mapCreator.CreateMeshes(mapGO);
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
			IsBuildableArea(GetTilePos(hit.point), 5, 3);
		}
	}

	public void DeselectArea()
	{
		tileSelecter.DeselectBuildArea();
	}
	#endregion

	public Vector3 GetTilePos(Vector3 position)
	{
		return gridManager.GetTilePos(position);
	}

	public bool IsBuildableTile(Vector3 position)
	{
		return gridManager.IsBuildableTile(position);
	}

	public bool IsBuildableArea(Vector3 pos, float areaSizeX, float areaSizeZ)
	{
		return tileSelecter.SelectBuildArea(pos, areaSizeX, areaSizeZ);
	}

	private void OnDrawGizmos()
	{
		if (mapCreator == null)
		{
			return;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(mapCreator.CitizenBasePoint, 5);

		Gizmos.color = Color.green;
		Gizmos.DrawSphere(mapCreator.FermerBasePoint[0], 5);
		Gizmos.DrawSphere(mapCreator.FermerBasePoint[1], 5);
		Gizmos.DrawSphere(mapCreator.FermerBasePoint[2], 5);
	}
}
