using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaSelecter : MonoBehaviour
{
	public Material unbuildableTileMat;
	public GameObject selectedTilePrefab;

	private Transform buildArea;
	private Vector3 defaultPosition = new Vector3(0, -100, 0);

	private MapManager mapManager;
	private float tileSize;

	// Число тайлов под макет здания
	private int tileCountX;
	private int tileCountZ;

	void Start()
	{
		mapManager = GameManagerBeforeMerge.GetGameManager().MapManagerInstance;
		tileSize = mapManager.TileSize;

		buildArea = new GameObject().transform;
		buildArea.name = "BuildingArea";
		buildArea.position = defaultPosition;
	}

	public bool SelectBuildArea(Vector3 pos, float areaSizeX, float areaSizeZ, Race race)
	{
		if (buildArea.position == defaultPosition)
		{
			CreateTiles(areaSizeX, areaSizeZ);
			PlaceTilesOnBuildArea();
		}

		buildArea.position = pos;
		return BuildAreaRevision(race);
	}

	private void CreateTiles(float xSize, float zSize)
	{
		tileCountX = CalculateTileCount(xSize);
		tileCountZ = CalculateTileCount(zSize);

		for (int i = 0; i < tileCountX * tileCountZ; i++)
		{
			GameObject go = Instantiate(selectedTilePrefab);
			go.transform.parent = buildArea.transform;
			go.transform.localScale *= tileSize;
		}
	}

	private int CalculateTileCount(float size)
	{
		int count = (int)(size / tileSize);

		if (size % tileSize != 0)
		{
			count++;
		}

		if (count % 2 == 0)
		{
			count++;
		}

		return count;
	}

	private void PlaceTilesOnBuildArea()
	{
		for (int x = 0; x < tileCountX; x++)
		{
			for (int z = 0; z < tileCountZ; z++)
			{
				Vector3 parentPos = buildArea.position;
				Vector3 dX = (x - tileCountX / 2) * Vector3.left * tileSize;
				Vector3 dZ = (z - tileCountZ / 2) * Vector3.forward * tileSize;

				buildArea.GetChild(x * tileCountZ + z).transform.position = parentPos - dX + dZ;
			}
		}
	}

	private bool BuildAreaRevision(Race race)
	{
		bool isBuildableArea = true;

		// TODO Nik
		// Читать из mapManager массив тайлов/булов нужного размера
		for (int i = 0; i < buildArea.childCount; i++)
		{
			Renderer rend = buildArea.GetChild(i).gameObject.GetComponent<Renderer>();
			rend.sharedMaterial = selectedTilePrefab.GetComponent<Renderer>().sharedMaterial;

			if (!mapManager.IsBuildableTile(buildArea.GetChild(i).transform.position, race))
			{
				rend.sharedMaterial = unbuildableTileMat;
				isBuildableArea = false;
			}
		}

		return isBuildableArea;
	}

	public void DeselectBuildArea()
	{
		if (buildArea.transform.childCount > 0)
		{
			foreach (Transform child in buildArea.transform)
			{
				Destroy(child.gameObject);
			}
		}

		buildArea.position = defaultPosition;
	}
}
