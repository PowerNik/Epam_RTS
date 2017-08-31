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
	private MapGeneratorSettings genSets;

	private void Awake()
	{
		genSets = mapSettings.GetMapGeneratorSettings();

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

		mapCreator = new MapCreator(mapSettings, go);
		//gridManager = GameManager.Instance.GetComponent<GridManager>();
        gridManager = GameManager.GetGameManager().GetComponent<GridManager>();

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

    public Vector3[] getPlayerStart(Race race)
    {
        Vector3[] citizenArray = new Vector3[1];
        switch (race)
        {
            case Race.Citizen:
                citizenArray[0] = mapCreator.CitizenBasePoint;
                return citizenArray;
            case Race.Fermer:
                return mapCreator.FermerBasePoint;
            default:
                citizenArray[0] = Vector3.zero;
                return citizenArray;
        }
    }

	private void OnDrawGizmos()
	{
		if(mapCreator == null)
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
