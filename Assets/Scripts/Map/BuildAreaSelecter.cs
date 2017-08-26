using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaSelecter : MonoBehaviour
{
	public Material unbuildableTileMat;
	public GameObject selectedTilePrefab;

	private Transform buildingArea;
	private Vector3 defaultPosition = new Vector3(0, -100, 0);

	private MapManager mapManager;
	private float tileSize;

	// Число тайлов под макет здания
	private int selectedAreaCountX;
	private int selectAreaCountZ;

	void Start()
	{
		mapManager = GameManagerBeforeMerge.GetGameManager().MapManagerInstance;
		tileSize = mapManager.TileSize;

		buildingArea = new GameObject().transform;
		buildingArea.name = "BuildingArea";
		buildingArea.position = defaultPosition;
	}

	public bool SelectBuildArea(Vector3 pos, float areaSizeX, float areaSizeZ)
	{
		if (buildingArea.position == defaultPosition)
		{
			CreateBuildArea(areaSizeX, areaSizeZ);
			PlaceBuildArea();
		}

		buildingArea.position = pos;
		return AreaRevision();
	}

	private void CreateBuildArea(float xSize, float zSize)
	{
		selectedAreaCountX = (int)(xSize / tileSize) + 1;
		selectAreaCountZ = (int)(zSize / tileSize) + 1;

		if (selectedAreaCountX % 2 == 0)
		{
			buildingArea.position -= Vector3.left * tileSize / 2f;
		}

		if (selectAreaCountZ % 2 == 0)
		{
			buildingArea.position += Vector3.forward * tileSize / 2f;
		}

		for (int i = 0; i < selectedAreaCountX * selectAreaCountZ; i++)
		{
			GameObject go = Instantiate(selectedTilePrefab);
			go.transform.parent = buildingArea.transform;
			go.transform.localScale *= tileSize;
		}
	}

	private void PlaceBuildArea()
	{
		for (int x = 0; x < selectedAreaCountX; x++)
		{
			for (int z = 0; z < selectAreaCountZ; z++)
			{
				Vector3 parentPos = buildingArea.position;
				Vector3 dX = x * Vector3.left * tileSize;
				Vector3 dZ = z * Vector3.forward * tileSize;

				buildingArea.GetChild(x * selectAreaCountZ + z).transform.position = parentPos - dX + dZ;
			}
		}
	}

	private bool AreaRevision()
	{
		bool isBuildableArea = true;

		for (int i = 0; i < buildingArea.childCount; i++)
		{
			Renderer rend = buildingArea.GetChild(i).gameObject.GetComponent<Renderer>();
			rend.sharedMaterial = selectedTilePrefab.GetComponent<Renderer>().sharedMaterial;

			if (!mapManager.IsBuildableTile(buildingArea.GetChild(i).transform.position))
			{
				rend.sharedMaterial = unbuildableTileMat;
				isBuildableArea = false;
			}
		}

		return isBuildableArea;
	}

	public void DeselectBuildArea()
	{
		if (buildingArea.transform.childCount > 0)
		{
			foreach (Transform child in buildingArea.transform)
			{
				Destroy(child.gameObject);
			}
		}

		buildingArea.position = defaultPosition;
	}
}
