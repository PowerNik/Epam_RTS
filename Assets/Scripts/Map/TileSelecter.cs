using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelecter : MonoBehaviour
{
	public Material impassibleTileMat;
	public GameObject selectedTilePrefab;
	private Transform selectedArea;

	private MapManager mapManager;
	private float tileSize;

	// Число тайлов под макет здания
	private int selectedAreaCountX;
	private int selectAreaCountZ;

	void Start()
	{
		mapManager = GameManagerBeforeMerge.GetGameManager().MapManagerInstance;
		tileSize = mapManager.TileSize;

		selectedArea = new GameObject().transform;
		selectedArea.name = "SelectedArea";
	}

	private void Update()
	{
		TileSelect();
		AreaSelect(selectedArea.position, 5.1f, 3.9f);
	}

	public void AreaSelect(Vector3 pos, float areaSizeX, float areaSizeZ)
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			CreateSelectedArea(pos, areaSizeX, areaSizeZ);
			PlaceSelectedArea();
		}
		AreaRevision();

		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			AreaDeselect();
		}
	}

	private void TileSelect()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			selectedArea.position = mapManager.GetTilePos(hit.point) + Vector3.up * 0.3f;
		}
	}

	private void CreateSelectedArea(Vector3 pos, float xSize, float zSize)
	{
		selectedArea.position = pos;

		selectedAreaCountX = (int)(xSize / tileSize) + 1;
		selectAreaCountZ = (int)(zSize / tileSize) + 1;

		if (selectedAreaCountX % 2 == 0)
		{
			selectedArea.position -= Vector3.left * tileSize / 2f;
		}

		if (selectAreaCountZ % 2 == 0)
		{
			selectedArea.position += Vector3.forward * tileSize / 2f;
		}

		//GameObject[] tileArea = new GameObject[selectedAreaCountX * selectAreaCountZ];
		for (int i = 0; i < selectedAreaCountX * selectAreaCountZ; i++)
		{
			Instantiate(selectedTilePrefab).transform.parent = selectedArea.transform;
		}
	}

	private void PlaceSelectedArea()
	{
		for (int x = 0; x < selectedAreaCountX; x++)
		{
			for (int z = 0; z < selectAreaCountZ; z++)
			{
				Vector3 parentPos = selectedArea.position;
				Vector3 dX = x * Vector3.left * tileSize;
				Vector3 dZ = z * Vector3.forward * tileSize;

				selectedArea.GetChild(x * selectAreaCountZ + z).transform.position = parentPos - dX + dZ;
			}
		}
	}

	private void AreaRevision()
	{
		for (int i = 0; i < selectedArea.childCount; i++)
		{
			Renderer rend = selectedArea.GetChild(i).gameObject.GetComponent<Renderer>();
			rend.sharedMaterial = selectedTilePrefab.GetComponent<Renderer>().sharedMaterial;

			if (!mapManager.IsBuildableTile(selectedArea.GetChild(i).transform.position))
			{
				rend.sharedMaterial = impassibleTileMat;
			}
		}
	}

	private void AreaDeselect()
	{
		if (selectedArea.transform.childCount > 0)
		{
			foreach (Transform child in selectedArea.transform)
			{
				Destroy(child.gameObject);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (mapManager == null)
		{
			return;
		}

		float width = mapManager.MapWidth;
		float length = mapManager.MapLength;

		for (int x = 0; x < mapManager.TileCountX; x++)
		{
			for (int z = 0; z < mapManager.TileCountZ; z++)
			{
				float curZ = z * tileSize - length / 2;
				Gizmos.DrawLine(new Vector3(-width / 2, 0, curZ), new Vector3(width / 2, 0, curZ));
			}
			float curX = x * tileSize - width / 2;
			Gizmos.DrawLine(new Vector3(curX, 0, -length / 2), new Vector3(curX, 0, length / 2));
		}
	}
}
